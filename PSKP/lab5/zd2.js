const express = require('express');
const bodyParser = require('body-parser');
const Sequelize = require('sequelize');
const redis = require('redis');
const jwt = require('jsonwebtoken');
const fs = require('fs');
const cookieParser = require('cookie-parser');

const app = express();

const redisClient = redis.createClient();
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(cookieParser());
app.use((req, res, next) => {
	if (req.cookies.accessToken) {
		jwt.verify(req.cookies.accessToken, access, (err, user) => {
			if (err) next();
			else if (user) {
				req.user = user;
				next();
			}
		});
	} else next();
});

const sequelize = new Sequelize('lab22', 'student', 'fitfit', {
	host: '192.168.140.101',
	dialect: 'mssql',
	dialectOptions: {
		options: {
			encrypt: true, // If encryption is used
			trustServerCertificate: true,
			validateBulkLoadParameters: true, // Add this line
		},
		requestTimeout: 30000, // Add this line (adjust the value based on your needs)
	},
});

const Model = Sequelize.Model;
class Users extends Model {}

Users.init(
	{
		id: { type: Sequelize.INTEGER, autoIncrement: true, primaryKey: true },
		name: { type: Sequelize.STRING, allowNull: false },
		password: { type: Sequelize.STRING, allowNull: false },
	},
	{
		sequelize,
		Users: 'USERS',
		tableName: 'USERS',
		createdAt: false,
		updatedAt: false,
	}
);

const access = 'accessToken';
const refresh = 'refreshToken';
let id = 20;

app.get('/login', (req, res) => {
	res.sendFile(__dirname + '/login.html');
});

app.post('/login', async (req, res, next) => {
	const { name, password } = req.body;
	const us = await Users.findOne({
		where: {
			name: name,
			password: password
		},
	});
	if (us) {
		const accessToken = jwt.sign({ id: us.id, name: us.name }, access, {
			expiresIn: 10 * 60,
		});
		const refreshToken = jwt.sign(
			{ id: us.id, name: us.name },
			refresh,
			{
				expiresIn: 24 * 60 * 60,
			}
		);
		res.cookie('accessToken', accessToken, {
			httpOnly: true,
			sameSite: 'strict',
		});
		res.cookie('refreshToken', refreshToken, {
			httpOnly: true,
			sameSite: 'strict',
		});
		res.redirect('/resource');
	} else {
		res.redirect('/login');
	}
});

app.get('/refresh-token', async (req, res) => {

	if (req.cookies.refreshToken) {
		jwt.verify(req.cookies.refreshToken, refresh, async (err, user) => {
			if (err) console.log(err.message);
			else if (user) {
				const userId = user.id;
				const refreshToken = req.cookies.refreshToken;
				const result2 =         await  redisClient.get(`key${refreshToken}`);

				if("value" + userId ===  result2){
					return res.status(401).send('Refresh token in black list');
				}

				const candidate = await Users.findOne({
					where: {
						id: userId,
					},
				});
				const newAccessToken = jwt.sign(
					{ id: candidate.id, name: candidate.name },
					access,
					{ expiresIn: 10 * 60 }
				);

				// Генерация нового refresh token
				const newRefreshToken = jwt.sign(
					{ id: candidate.id, name: candidate.name },
					refresh,
					{ expiresIn: 24 * 60 * 60 }
				);

				console.log('NEW REFRESH ' + newRefreshToken);
				res.clearCookie('accessToken');
				res.clearCookie('refreshToken');
				res.cookie('accessToken', newAccessToken, {
					httpOnly: true,
					sameSite: 'strict',
				});
				res.cookie('refreshToken', newRefreshToken, {
					httpOnly: true,
					sameSite: 'strict',
				});

				await  redisClient.set(`key${refreshToken}`, `value${userId}`);
				const keys = await redisClient.keys(`key${refreshToken}*`);

				// Iterate through each key and retrieve its corresponding value
				const results = await Promise.all(keys.map(async key => {
					const value = await redisClient.get(key);
					return { key, value };
				}));

				const result =         await  redisClient.get(`key${refreshToken}`);

				console.log('refresh token in balck list', result);

/*
				id++;
*/

				// Запись в файл
				const filePath = 'C:/instit/kurs3_2/PSKP/lab5/Black.txt';
				const data = `${userId}:${refreshToken}\n`;

				fs.appendFile(filePath, data, (err) => {
					if (err) {
						console.error('Error writing to Black.txt:', err);
					} else {
						console.log('Token added to Black.txt:', data);
					}
				});

				res.redirect('/resource');

/*
				await redisClient.exists(`key${userId}:value${refreshToken}`, async (err, reply) => {
					if (reply === 1) {
						return res.status(401).send('Refresh token is blacklisted');
					}

					await redisClient.exists(`${userId}`, async (err, reply) => {
						if (reply === 1) {
							console.log('Exists');
							await redisClient.del(`${userId}`, function (err, result) {
								console.log('DELETE ' + result);
							});
						}
					});

					await redisClient.get(`${userId}`, (err, result) => console.log('get ', result));


				});
*/
			} else {
				res.status(401).send('Authorize');
			}
		});
	} else {
		res.status(401).send('Authorize');
	}
});

app.get('/resource', async (req, res) => {
	if (req.user) {
		const userId = req.user.id;
		const refreshToken = req.cookies.refreshToken;
		try {
			const result2 = await redisClient.get(`key${refreshToken}`);
			if (`value${userId}` === result2) {
				return res.status(401).send('Refresh token is in the blacklist');
			}
		} catch (error) {
			console.error('Error accessing Redis:', error);
			return res.status(500).send('Internal Server Error');
		}

		res.status(200).send(`resource userId:${userId} name:${req.user.name}`);
	} else {
		res.status(401).send('you not autorize');
	}
	/*if (req.user)
		res
			.status(200)
			.send(`RESOURCE userId:${req.user.id} name:${req.user.name}`);
	else res.status(401).send('Unauthorized');*/
});

app.get('/logout', (req, res) => {
	res.clearCookie('accessToken');
	res.clearCookie('refreshToken');

	res.redirect('/login');
});

app.get('/register', (req, res) => {
	res.sendFile(__dirname + '/reg.html');
});

app.post('/register', async (req, res) => {
	const us = await Users.findOne({
		where: {
			name: req.body.name,
		},
	});
	if (us) res.redirect('/register');
	else {
		await Users.create({
			name: req.body.name,
			password: req.body.password,
		});
		res.redirect('/login');
	}
});

app.all('*', (req, res) => {
	res.status(404);
	throw new Error('Page not found');
});

app.use(function (err, req, res, next) {
	res.send(err.message);
});

sequelize.sync().then(() => {
	app.listen(3000, () => {
		redisClient.connect().then(async () => {
			console.log('connect Redis');
		}).catch((err) => {
			console.log('connection error Redis:', err);
		});
		sequelize
			.authenticate()
			.then(() => console.log('connection success'));

	});
});

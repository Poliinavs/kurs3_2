const path = require('path')
const jwt = require('jsonwebtoken')
const db = require('../db/db')

const LoginService = {
    getLogin: (req, res, next) => {
        res.sendFile(path.join(__dirname, '../static/login.html'))
    },
    getRegister: (req, res, next) => {
        res.sendFile(path.join(__dirname, '../static/login.html'))
    },
    
    postLogin: async (req, res, next) => {
        const usr = await db.models.Users.findOne({
            where: {
                name: req.body.username,
                password: req.body.password
            }
        })
        if (usr) {
            const accessToken = jwt.sign({id: usr.id, name: usr.name, role: usr.role}, accessKey, {expiresIn: 30 * 60})
            const refreshToken = jwt.sign({id: usr.id, name: usr.name, role: usr.role}, refreshKey, {expiresIn: 24 * 60 * 60})
            res.cookie('accessToken', accessToken, {
                httpOnly: true,
                sameSite: 'strict'
            })
            res.cookie('refreshToken', refreshToken, {
                httpOnly: true,
                sameSite: 'strict'
            })
            res.sendFile(path.join(__dirname, '../static/main.html'))
        } else {
            res.redirect('/login')
        }
    },

    postRegister: async (req, res, next) => {
        const { username, email, password } = req.body;
        const role = 'user';

        try {
            await global.sequelize.query(`
            INSERT INTO users(name, email, password, role) VALUES('${username}', '${email}', '${password}', '${role}')
        `);
            res.redirect('/login');
        } catch (error) {
            // Handle any errors appropriately
            console.error('Error regist:', error);
            res.status(500).send('error registr');
        }
    },

    getLogout: (req, res, next) => {
        res.clearCookie('accessToken')
        res.clearCookie('refreshToken')
        res.redirect('/login')
    }

}

module.exports = LoginService
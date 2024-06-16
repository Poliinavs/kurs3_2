import bodyParser from 'body-parser';
import express from 'express';
import fs from 'fs';
import swaggerUi from 'swagger-ui-express';
import data from './tel.json' assert { type: 'json' };
import swaggerDocument from './swagger.js';
const telJsonPath = './tel.json';


const options = {
    explorer: true,
};

const app = express();

app.use(bodyParser.json());

function readTelJson() {
    try {
        const telJsonData = fs.readFileSync(telJsonPath, 'utf8');
        return JSON.parse(telJsonData);
    } catch (err) {
        console.error('Error reading tel.json:', err);
        return [];
    }
}


function commit(data) {
    fs.writeFile('./tel.json', JSON.stringify(data, null, '  '), err => {
        if (err) {
            throw err;
        }
    });
}

app.use(
    '/swagger',
    swaggerUi.serve,
    swaggerUi.setup(swaggerDocument, options)
);

console.log(' http://localhost:3000/swagger')

app.get('/TS', (req, res) => {
    const data = readTelJson();
    res.send(data);
});
app.post('/TS', (req, res) => {
    const lastId = data[data.length - 1].id;
    const { name, number } = req.body;
    if (name && number && number.length > 6) {
        const obj = {
            id: lastId + 1,
            name: name,
            number: number,
        };
        data.push(obj);
        commit(data);
        res.send(obj);
    } else {
        res.status(300);
        res.send('bed request');
    }
});
app.put('/TS', (req, res) => {
    const { name, number } = req.body;
    if (name && number && number.length > 6) {
        const obj = data.find(el => el.name === name);
        if(obj == null){
            res.status(404);
            res.send('Not found such user');
        }
        obj.number = number;
        commit(data);
        res.send(obj);
    } else {
        res.status(300);
        res.send('bed request');
    }
});
app.delete('/TS', (req, res) => {
    const { name } = req.body;
    if (name ) {
        const obj = data.find(el => el.name === name);
        if(obj == null){
            res.status(404);
            res.send('Not found such user');
        }
        const newData = data.filter(el => el.id != obj.id);
        commit(newData);
        res.send(obj);
    } else {
        res.status(300);
        res.send('bed request');
    }
});

app.listen(3000);
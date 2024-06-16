import bodyParser from 'body-parser';
import express from 'express';
import fs from 'fs';
import path from 'path';
import formidable from 'formidable';

import axios from 'axios';

import { createClient } from 'webdav';

const __dirname = path.resolve();

const app = express();
const { urlencoded, json } = bodyParser;

app.use(urlencoded({ extended: false }));
app.use(json());
app.use(function (err, req, res, next) {
    res.send(err.message);
});

/*
app.post('/md/:name', async (req, res) => {
    const dirName = req.params.name;

    try {
        const response = await axios({
            method: 'PUT',
            url: `https://webdav.yandex.ru/${dirName}`,
            headers: {
                Authorization: 'OAuth y0_AgAAAABBWOsBAAuelgAAAAEB9FKfAABXAIlIcbxCar-cyhfe8Yq5pxHYZA'
            }
        });

        console.log('Response:', response.data); // Выводим данные ответа для отладки

        if (response.status === 201) {
            res.status(201).send('Directory created');
        } else {
            res.status(response.status).send(response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
        res.status(error.response.status).send('An error occurred while processing your request');
    }
});*/


const client = createClient('https://webdav.yandex.ru', {
    username: 'paulinaavsyukevitch',
    password: 'wciepmdhjvolhmnm',
});

app.post('/md/:name', async (req, res) => {
    const dirName = req.params.name;
    if (await client.exists(`/${dirName}`)) {
        res.status(408);
        res.send('This directory exists');
    } else {
        await client.createDirectory(`/${dirName}`);
        res.send('Directory created');
    }
});

app.post('/rd/:name', async (req, res) => {
    const dirName = req.params.name;
    if (await client.exists(`/${dirName}`)) {
        await client.deleteFile(`/${dirName}`);
        res.send('Directory deleted');
    } else {
        res.status(404);
        res.send('Directory is not found');
    }
});

const form = formidable();

app.post('/up/:file', async (req, res) => {

    const file = req.params.file;
    if (file.includes('.')) {
        if (fs.existsSync(__dirname + `/${file}`)) {
            fs.createReadStream(__dirname + `/${file}`).pipe(
                client.createWriteStream(`/${file}`)
            );
            res.send('Upload file');
        } else {
            res.status(404);
            res.send('File not found');
        }
    } else {
        res.status(404);
        res.send('Is not a file');
    }


  /*  form.parse(req, (err, fields, files) => {
        if (err) {
            console.error('Error parsing form data:', err);
            return res.status(500).send('Error parsing form data');
        }

        // Проверяем, был ли передан файл
        if (!files || !files.file) {
            return res.status(400).send('No file uploaded');
        }

        const file = files.file;
        client
            .createReadStream(`/${file[Object.keys(file)[0]].filepath}`)
            .pipe(fs.createWriteStream(`./${file[Object.keys(file)[0]].filepath}`));
console.log(file[Object.keys(file)[0]].filepath)
        // Создаем поток для записи файла на сервере
        const writeStream = fs.createWriteStream(`${__dirname}/${file[Object.keys(file)[0]].name}`);

        // Перенаправляем содержимое файла в поток записи
        fs.createReadStream(file[Object.keys(file)[0]].filepath).pipe(writeStream);

        // Обработчик события завершения записи файла
        writeStream.on('finish', () => {

            res.send('Upload file'); // Отправляем ответ о успешной загрузке
        });

        // Обработчик события ошибки при записи файла
        writeStream.on('error', (error) => {
            console.error('Error writing file:', error);
            return res.status(500).send('Error writing file');
        });
    });*/
});

app.post('/down/:file', async (req, res) => {
    const file = req.params.file;
    if (file.includes('.')) {
        if (await client.exists(`/${file}`)) {
           /* client
                .createReadStream(`/${file}`)
                .pipe(fs.createWriteStream(`./${file}`));
            res.send('Download file');*/

            const downloadFilePath = 'C:\\instit\\kurs3_2\\PSKP\\lab10\\downl' + file;
            const readStream = client.createReadStream(file);
            const writeStream = fs.createWriteStream(downloadFilePath);
            readStream.pipe(writeStream);
            res.status(200).send('Download file');


        } else {
            res.status(404);
            res.send('File is not exists');
        }
    } else {
        res.status(404);
        res.send('Is not a file');
    }
});

app.post('/del/:file', async (req, res) => {
    const file = req.params.file;
    if (file.includes('.')) {
        if (await client.exists(`/${file}`)) {
            await client.deleteFile(file);
            res.send('File deleted');
        } else {
            res.status(404);
            res.send('File not found');
        }
    } else {
        res.send('Is not a file');
    }
});

app.post('/copy/:oldName/:newName', async (req, res) => {
    const oldName = req.params.oldName;
    const newName = req.params.newName;
    if (await client.exists(`/${oldName}`)) {
        await client.copyFile(`/${oldName}`, `/${newName}`);
        res.send('File copied');
    } else {
        res.status(404);
        res.send('File not found');
    }

});

app.post('/move/:oldName/:newName', async (req, res) => {
    const oldName = req.params.oldName;
    const newName = req.params.newName;
    if (await client.exists(`/${oldName}`)) {
        await client.moveFile(`/${oldName}`, `/${newName}`);
        res.send('File moved');
    } else {
        res.status(404);
        res.send('File not found');
    }

});

app.listen(3000);
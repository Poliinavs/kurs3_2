const redis = require('redis');

const client = redis.createClient();
client.connect().then(() => {
    console.log('connect Redis');
    client.quit().then(() => {
        console.log('connect close Redis');
    }).catch((err) => {
        console.log('connection error Redis:', err);
    });
}).catch((err) => {
    console.log('connection error Redis:', err);
});
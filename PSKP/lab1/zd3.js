const redis = require('redis');
const { promisify } = require('util');

const client = redis.createClient();

async function testIncr() {
    for (let i = 1; i <= 10000; i++) {

        await client.incr('incr');
    }
}

async function testDecr() {
    for (let i = 1; i <= 10000; i++) {
        await client.decr('incr');
    }
}

async function runTests() {
    try {
        // Устанавливаем начальное значение счетчика
        await client.set('incr', 0);

        console.time('INCR');
        await testIncr();
        console.timeEnd('INCR');

        console.time('DECR');
        await testDecr();
        console.timeEnd('DECR');
    } catch (error) {
        console.error('Error during tests:', error);
    } finally {
        client.quit().then(() => {
            console.log('Connection closed');
        }).catch((err) => {
            console.log('Error closing connection:', err);
        });
    }
}

// Подключаемся к Redis
client.connect().then(async () => {
    console.log('connect Redis');
    await runTests();
}).catch((err) => {
    console.log('connection error Redis:', err);
});

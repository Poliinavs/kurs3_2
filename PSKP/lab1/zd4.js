const redis = require('redis');
const { promisify } = require('util');

const client = redis.createClient();

async function testHSet() {
    for (let i = 1; i <= 10000; i++) {
        await client.hSet(i.toString(),`id:${i}`,`${JSON.stringify({val:`val-${i}`})}`);
    }
}

async function testHGet() {
    for (let i = 1; i <= 10000; i++) {
        await client.hGet(i.toString(), `id:${i}`);
    }
}

async function runTests() {
    try {
        console.time('HSET');
        await testHSet();
        console.timeEnd('HSET');

        console.time('HGET');
        await testHGet();
        console.timeEnd('HGET');
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


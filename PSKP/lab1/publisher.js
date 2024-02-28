const redis = require('redis');
const publisher = redis.createClient();
(async () => {
    await publisher.connect();
    await publisher.publish('myChanel', JSON.stringify('Hello'));
})();
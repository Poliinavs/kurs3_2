const redis = require('redis');

(async () => {

    const subscriber = redis.createClient();

    await subscriber.connect();

    await subscriber.subscribe('myChanel', (message) => {
        console.log(message);
    });

})();
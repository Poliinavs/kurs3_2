const request = require('request');
const TelegramBot = require('node-telegram-bot-api');
const bot = new TelegramBot(token, { polling: true });

const axios = require('axios');
const prismaClient = require('./prisma/Clients')



bot.onText('', async (msg) => {
    const chatId = msg.chat.id;


    if (msg.text.toLowerCase() === 'привет') {
        bot.sendSticker(chatId, stickerFileId).catch(error => {
            console.error(error); // В случае возникновения ошибки выведем её в консоль
        });
    }
    if (msg.text.toLowerCase() === '/weather'){
        const apiKey = 'c7f6ee8d23ce6d1074e9b71bd025dbcf';
        const cityName = 'Italy';

        try {
            const response = await axios.get(apiUrl);

            const weatherData = response.data;
            const weatherDescription = weatherData.weather[0].description;
            const temperature = weatherData.main.temp;
            const humidity = weatherData.main.humidity;
            const pressure = weatherData.main.pressure;
            const windSpeed = weatherData.wind.speed;


            console.log(`Погода в ${cityName}:`);
            console.log(`Температура: ${temperature}`);
            console.log(`Влажность: ${humidity}`);
            console.log(`Давление: ${pressure}`);
            console.log(`Скорость ветра: ${windSpeed} `);

            const message = `
            Погода ${cityName}:
            Скорость ветра: ${windSpeed} 
            Температура: ${temperature}
            Влажность: ${humidity}
            Давление: ${pressure} 
        `;

            bot.sendMessage(chatId, message);

        } catch (error) {
            console.error('error', error.message);
        }
    }



    if(msg.text.toLowerCase() === '/subscribe'){
        prismaClient.subscribers.create({
            data:{
                chat_id: chatId
            }
        }).then(() => {
            bot.sendMessage(chatId, "you subscribe to bot");
            sendFact(chatId);
        })
            .catch((error) => {
                if (error.code === 'P2002') {
                    bot.sendMessage(chatId, "error subscribe");
                } else {
                    bot.sendMessage(chatId, "error subscribe");
                }
            })
    }

    if(msg.text.toLowerCase() === '/unsubscribe'){
        prismaClient.subscribers.delete({
            where: {
                chat_id: chatId
            }
        })
            .then(() => {
                bot.sendMessage(chatId, "you unsubscribe");
            })
            .catch((error) => {
                console.error('Ошибка при отписке:', error);
                bot.sendMessage(chatId, "error while unsubscribe");
            })
    }

  if (msg.text.toLowerCase() === '/joke') {

    request.get({
      url: 'https://api.api-ninjas.com/v1/jokes?limit=1',
      headers: {
        'X-Api-Key': WheatherApiKey
      },
    }, function(error, response, body) {
      if(error) {
        bot.sendMessage(chatId, 'error');
        return console.error('Request failed:', error);
      } else if(response.statusCode != 200) {
        bot.sendMessage(chatId, 'error get joke ' + response.statusCode);
        return console.error('Error:', response.statusCode, body.toString('utf8'));
      } else {
        
        try {
          const joke = JSON.parse(body);
          let jokeMessage = joke.map(joke => joke.joke).join('nn');
          bot.sendMessage(chatId, jokeMessage);
        } catch (parseError) {
          bot.sendMessage(chatId, 'error handling joke');
          return console.error('Parse failed:', parseError);
        }
      }
    });
  };

    if(msg.text.toLowerCase() === '/cat'){

        axios.get('https://api.thecatapi.com/v1/images/search', {
            headers: {
                'x-api-key': CatImgAPI_KEY
            }
        })
            .then(response => {
                const imageUrl = response.data[0].url;
                bot.sendPhoto(chatId, imageUrl);
            })
            .catch(error => {
                bot.sendMessage(chatId, 'error get cat img');
                console.error(error);
            });
    }
    
  });

const cron = require('node-cron');

cron.schedule('0 0 * * *', () => {

    prismaClient.subscribers.findMany()
        .then((subscribers) => {
            subscribers.forEach((subscriber) => {
                sendFact(subscriber.chat_id);
            });
        })
        .catch((error) => {
            console.error('error get subscribers', error);
        })
  });
  
function sendFact(chatId) {
   
    request.get({
      url: 'https://api.api-ninjas.com/v1/facts?limit=1',
      headers: {
        'X-Api-Key': WheatherApiKey
      },
    }, function(error, response, body) {
      if(error) {
        bot.sendMessage(chatId, 'error facts');
        return console.error('Request failed:', error);
      } else if(response.statusCode != 200) {
        bot.sendMessage(chatId, 'can t get facts' + response.statusCode);
        return console.error('Error:', response.statusCode, body.toString('utf8'));
      } else {
        try {
          const facts = JSON.parse(body);
          let factsMessage = facts.map(fact => fact.fact).join('nn');
          bot.sendMessage(chatId, factsMessage);
        } catch (parseError) {
          bot.sendMessage(chatId, 'error handling facts');
        }
      }
    });
  }


bot.on('message', (msg) => {
    const chatId = msg.chat.id;
    if(msg.text === null){
        return;
    }
    bot.sendMessage(chatId, msg.text); // Echo the same message back
});








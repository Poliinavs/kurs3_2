const express = require('express')
const bodyParser = require('body-parser')
const jwt = require('jsonwebtoken')
global.accessKey = 'secracces'
global.refreshKey = 'secrRef'

const cookieParser = require('cookie-parser')
let loginController = require('./controllers/loginController')
let mainController = require('./controllers/mainController')

const app = express()
app.use(cookieParser('lbbb'))
app.use(bodyParser.json())
app.use(bodyParser.urlencoded({extended: true}))





app.use((req, res, next) => {
    if (req.cookies.accessToken) {
        jwt.verify(req.cookies.accessToken, accessKey, (err, user) => {
            if (err) next()
            else if(user) {
                req.user = user
                next()
            }
        })
    }
    else next()
})

app.use(express.static('static'))
app.use(loginController)
app.use(mainController)
app.use((err, req, res, next) => {
    res.send(`${err}`)
})

app.listen(3000)
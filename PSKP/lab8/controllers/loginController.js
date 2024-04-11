const express = require('express')
const AuthController = require('../Services/loginService')

let rout = express.Router()

rout.get('/login', AuthController.getLogin)
rout.get('/register', AuthController.getRegister)
rout.get('/logout', AuthController.getLogout)
rout.post('/login', AuthController.postLogin)
rout.post('/register', AuthController.postRegister)

module.exports = rout
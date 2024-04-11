const Sequelize = require('sequelize')
const redis = require('redis')

global.sequelize = new Sequelize('LabAuthNode', 'student', 'fitfit', {
	host: '192.168.140.101',
	dialect: 'mssql',
	dialectOptions: {
		options: {
			encrypt: true, // Если используется шифрование
			trustServerCertificate: true,
		},
	},
});


const {Users, Repos, Commits} = require('./models')

module.exports = {
    models: {Users, Repos, Commits}
}
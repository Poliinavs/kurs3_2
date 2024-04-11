const { defineRulesFor } = require("../util/abilitites")
const db = require('../db/db')
const path = require('path')

const ApiController = {

    getUserAbilities: (req, res, next) => {
        res.json(defineRulesFor(req.user))
    },
    getUsersList: async (req, res, next) => {
        try {
            if (req.user.role =='user')
            throw new Error('you need admin permission')
            let list = await db.models.Users.findAll()
            list = list.map(user => {
                const { password, ...userWithoutPassword } = user.toJSON(); // Разбираем объект пользователя, исключая поле password
                return userWithoutPassword;
            });
            res.json(list)        
        } catch(err) {
            next(err)
        }
    },
    getUserInfo: async (req, res, next) => {
        try {
            if(req.user.role == 'user' && req.params.id != req.user.id) 
            throw new Error('you can get only personal info')
            let info = await db.models.Users.findOne({
                where: {id: req.params.id}
            })
            res.json({id: info.id, username: info.name})
        } catch(err) {
            next(err)
        }
    },

    getReposList: async (req, res, next) => {
        try {
            let list = await db.models.Repos.findAll()
            res.json(list)
        } catch(err) {
            next(err)
        }
    },
    getRepoInfo: async (req, res, next) => {
        try {
            let info = await db.models.Repos.findOne({
                where: {id: req.params.id}
            })
            res.json({id: info.id, name: info.name, authorId: info.authorId})
        } catch(err) {
            next(err)
        }
    },

    getReposCommits: async (req, res, next) => {
        try {
            let list = await db.models.Commits.findAll({
                where: {repoId: req.params.id}
            })
            res.json(list)
        } catch(err) {
            next(err)
        }
    },
    getCommitInfo: async (req, res, next) => {
        try {
            let info = await db.models.Commits.findOne({
                where: {repoId: req.params.id, id: req.params.commitId}
            })
            res.json(info)
        } catch(err) {
            next(err)
        }
    },


    addRepo: async (req, res, next) => {
        try {
            let result = await global.sequelize.query(
            `insert into repos(name, authorId) values ('${req.body.reponame}', '${req.user.id}')`)
            res.sendFile(path.join(__dirname, '../static/main.html'))
        } catch(err) {
            next(err)
        }
    },
    editRepo: async (req, res, next) => {
        try {
            let check = await db.models.Repos.findOne({where: {id: req.params.id, authorId: req.user.id}})
            if(!check && (req.user.role != 'admin')) throw new Error('you can edit only you repos')
            let result = await db.models.Repos.update({name: req.body.reponame}, {where: {id: req.params.id}})
            res.sendFile(path.join(__dirname, '../static/main.html'))
        } catch(err) {
            next(err)
        }
    },
    deleteRepo: async (req, res, next) => {
        try {
            let check = await db.models.Repos.findOne({where: {id: req.params.id}})
            if(!check && (req.user.role != 'admin')) throw new Error('you have not permission')
            let result = await db.models.Repos.destroy({where: {id: req.params.id}})
            res.sendFile(path.join(__dirname, '../static/main.html'))
        } catch(err) {
            next(err)
        }
    },
    addCommit: async (req, res, next) => {
        try {
            let check = await db.models.Repos.findOne({where: {id: req.params.id, authorId: req.user.id}})
            if(!check && (req.user.role != 'admin')) throw new Error('you can commit only on you repos')
            let result = await global.sequelize.query(
            `insert into commits(repoId, message) values (${req.params.id}, '${req.body.commitmessage}')`)
            res.sendFile(path.join(__dirname, '../static/main.html'))
        } catch(err) {
            next(err)
        }
    },
    editCommit: async (req, res, next) => {
        try {
            let check = await db.models.Repos.findOne({where: {id: req.params.id, authorId: req.user.id}})
            if(!check && (req.user.role != 'admin'))
                throw new Error('you can edit only you repos')
            await db.models.Commits.update({message: req.body.commitmessage}, {where: {id: req.params.commitId, repoId: req.params.id}})
            res.sendFile(path.join(__dirname, '../static/main.html'))
        } catch(err) {
            next(err)
        }
    },
    deleteCommit: async (req, res, next) => {
        try {
            let check = await db.models.Repos.findOne({where: {id: req.params.id}})
            console.log(!check && (req.user.role != 'admin'))
            if(!check && (req.user.role != 'admin'))
                throw new Error('you have not permission')
            await db.models.Commits.destroy({where: {id: req.params.commitId}})
            res.sendFile(path.join(__dirname, '../static/main.html'))
        } catch(err) {
            next(err)
        }
    }
}

module.exports = ApiController
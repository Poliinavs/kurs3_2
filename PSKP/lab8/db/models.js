const Sequelize = require('sequelize')

const Model = Sequelize.Model;

class Users extends Model{}
class Repos extends Model{}
class Commits extends Model{}
Users.init (
    {
        id:	{type: Sequelize.INTEGER, primaryKey:true, autoIncrementIdentity: true},
        name:{type: Sequelize.STRING, allowNull: false},
        password:	{type: Sequelize.STRING, allowNull: false},
        role: {type: Sequelize.STRING, validate: {isIn:[['user', 'admin']]}}
    },
    {sequelize, modelName:'Users', tableName:'users', timestamps: false}
)
Repos.init (
    {
        id:	{type: Sequelize.INTEGER, primaryKey:true, autoIncrementIdentity: true},
        name:{type: Sequelize.STRING, allowNull: false},
        authorId: {type: Sequelize.INTEGER, allowNull: false, references: {model: 'Users', key: 'id'}}
    },
    {sequelize, modelName:'Repos', tableName:'repos', timestamps: false}
)
Commits.init (
    {
        id:	{type: Sequelize.INTEGER, primaryKey:true, autoIncrementIdentity: true},
        repoId: {type: Sequelize.INTEGER, allowNull: false, references: {model: 'Repos', key: 'id'}},
        message: {type: Sequelize.STRING, allowNull: false}
    },
    {sequelize, modelName:'Commits', tableName:'commits', timestamps: false}
)

Users.hasMany(Repos, {as:'users_repos', foreignKey:'authorId', sourceKey:'id'})
Repos.hasMany(Commits, {as:'repos_commits', foreignKey:'repoId', sourceKey:'id'})

module.exports = { Users, Repos, Commits }
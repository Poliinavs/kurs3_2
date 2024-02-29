const { Model, Sequelize } = require("sequelize");

const sequelize = new Sequelize('lab22', 'student', 'fitfit', {
    host: '192.168.140.101',
    dialect: 'mssql',
    dialectOptions: {
        options: {
            encrypt: true, // Если используется шифрование
            trustServerCertificate: true,
        },
    },
});

class Faculty extends Model {}
class Pulpit extends Model {}
class Teacher extends Model {}
class Subject extends Model {}
class Auditorium_type extends Model {}
class Auditorium extends Model {}

Faculty.init(
    {
        FACULTY: {type: Sequelize.STRING, allowNull:false, primaryKey:true},
        FACULTY_NAME:{type: Sequelize.STRING, allowNull:false}
    },{
        sequelize, modelName:"Faculty", tableName:"FACULTY", timestamps:false
    }
);
Pulpit.init(
    {
        PULPIT: {type: Sequelize.STRING, allowNull:false, primaryKey:true},
        PULPIT_NAME:{type: Sequelize.STRING, allowNull:false},
        FACULTY: {type: Sequelize.STRING,allowNull:false,
            references: {model: Faculty, key: "FACULTY"}}
    },{
        sequelize, modelName:"Pulpit", tableName:"PULPIT", timestamps:false
    }
);
Teacher.init(
    {
        TEACHER: {type: Sequelize.STRING, allowNull:false, primaryKey:true},
        TEACHER_NAME:{type: Sequelize.STRING, allowNull:false},
        PULPIT: {type: Sequelize.STRING,allowNull:false,
            references: {model: Pulpit, key: "PULPIT"}}
    },{
        sequelize, modelName:"Teacher", tableName:"TEACHER", timestamps:false
    }
);
Subject.init(
    {
        SUBJECT: {type: Sequelize.STRING, allowNull:false, primaryKey:true},
        SUBJECT_NAME:{type: Sequelize.STRING, allowNull:false},
        PULPIT: {type: Sequelize.STRING,allowNull:false,
            references: {model: Pulpit, key: "PULPIT"}}
    },{
        sequelize, modelName:"Subject", tableName:"SUBJECT", timestamps:false
    }
);
Auditorium_type.init(
    {
        AUDITORIUM_TYPE: {type: Sequelize.STRING, allowNull:false, primaryKey:true},
        AUDITORIUM_TYPENAME:{type: Sequelize.STRING, allowNull:false},
    },{
        sequelize, modelName:"Auditorium_type", tableName:"AUDITORIUM_TYPE", timestamps:false
    }
);
Auditorium.init(
    {
        AUDITORIUM: {type: Sequelize.STRING, allowNull:false, primaryKey:true},
        AUDITORIUM_NAME:{type: Sequelize.STRING, allowNull:false},
        AUDITORIUM_CAPACITY: {type: Sequelize.INTEGER, allowNull:false},
        AUDITORIUM_TYPE:{type: Sequelize.STRING, allowNull:false,
            references: {model: Auditorium_type, key: "AUDITORIUM_TYPE"}},
    },
    {
        sequelize: sequelize, modelName:"Auditorium", tableName:"AUDITORIUM", timestamps:false
    }
);

module.exports = {
    sequelize,
    Faculty,
    Pulpit,
    Teacher,
    Subject,
    Auditorium_type,
    Auditorium
};
const {Sequelize,Model}=require("sequelize");
const Op=Sequelize.Op;



const http=require("http");
const url=require("url");
const fs=require("fs");
const {    sequelize, Faculty, Pulpit, Subject, Teacher, Auditorium_type, Auditorium} = require("./TablesInit");

Faculty.hasMany(Pulpit, {
    foreignKey: 'FACULTY',
    onDelete: 'CASCADE'
});
Pulpit.hasMany(Subject, {
    foreignKey: 'PULPIT',
    onDelete: 'CASCADE'
});
Pulpit.hasMany(Teacher,{
    foreignKey:'PULPIT',
    onDelete: 'CASCADE'
})
Auditorium_type.hasMany(Auditorium,{
    foreignKey:'AUDITORIUM_TYPE',
    onDelete: 'CASCADE'
})

Auditorium.addScope('capacityRange', {
    where: {
        AUDITORIUM_CAPACITY: {
            [Sequelize.Op.between]: [10, 60]
        }
    }
});

Auditorium.scope('capacityRange').findAll()
    .then(auditoriums => {
        console.log(auditoriums);
    })
    .catch(error => {
        console.error(error);
    });

Faculty.addHook("beforeCreate", (instance,options)=>{
    console.log("///////////faculty beforeCreate/////////////");
})

Faculty.addHook("afterCreate", (instance,options)=>{
    console.log("////////////faculty afterCreate///////////");
})

Faculty.addHook("beforeDestroy", (instance, options) => {
    console.log("///////////faculty beforeDestroy/////////////");
});



sequelize.authenticate()
    .then(()=>{
        console.log("connected");
        http.createServer((req,resp)=>{
            let codeDelete=new RegExp("[A-Za-zА-Яа-я-]{0,6}",'g');

            const urlPath = decodeURIComponent(url.parse(req.url).pathname);
            const pathSegments = urlPath.split('/');
            //-------------------GET---------------------------------------------------------------------------
            if (req.method=="GET"){
                if (url.parse(req.url).pathname=="/"){
                    resp.writeHead(200,{"Content-Type":"text/html"});
                    let html=fs.readFileSync("zd1.html");
                    resp.end(html);
                }
                else if (url.parse(req.url).pathname=="/api/faculties"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    Faculty.findAll()
                        .then(table=>{resp.end(JSON.stringify(table));})
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else if (url.parse(req.url).pathname=="/api/pulpits"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    Pulpit.findAll()
                        .then(table=>{resp.end(JSON.stringify(table));})
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else if (url.parse(req.url).pathname=="/api/pulpits"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    Pulpit.findAll()
                        .then(table=>{resp.end(JSON.stringify(table));})
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else if (url.parse(req.url).pathname=="/api/subjects"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    Subject.findAll()
                        .then(table=>{resp.end(JSON.stringify(table));})
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else if (url.parse(req.url).pathname=="/api/teachers"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    Teacher.findAll()
                        .then(table=>{resp.end(JSON.stringify(table));})
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else if (url.parse(req.url).pathname=="/api/auditoriumstypes"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    Auditorium_type.findAll()
                        .then(table=>{resp.end(JSON.stringify(table));})
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else if (url.parse(req.url).pathname=="/api/auditoriums"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    Auditorium.findAll()
                        .then(table=>{resp.end(JSON.stringify(table));})
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else if (pathSegments.length === 5 &&
                    pathSegments[1] === 'api' &&
                    pathSegments[2] === 'faculties'){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
                    Faculty.findAll({
                        where:{
                            FACULTY: code
                        },
                        include: [
                            {model: Pulpit, required:true,
                                include:[
                                    {model: Subject, required:true}
                                ]
                            }
                        ]
                    })
                        .then(table=>{resp.end(JSON.stringify(table));})
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else
                if (pathSegments.length === 5 &&
                    pathSegments[1] === 'api' &&
                    pathSegments[2] === 'auditoriumtypes'){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
                    Auditorium_type.findAll({
                        where:{
                            AUDITORIUM_TYPE: code
                        },
                        include: [
                            {model: Auditorium, required:true}
                        ]
                    })
                        .then(table=>{resp.end(JSON.stringify(table));})
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
            }
            else
            if (req.method=="POST"){
                if (url.parse(req.url).pathname=="/api/faculties"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let body = ' ';
                    req.on('data', chunk => {
                        body = chunk.toString();
                        body = JSON.parse(body);
                    });
                    req.on('end',  () => {
                        Faculty.create({
                            FACULTY: body.FACULTY,
                            FACULTY_NAME: body.FACULTY_NAME
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    })
                }
                else
                if (url.parse(req.url).pathname=="/api/pulpits"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let body = ' ';
                    req.on('data', chunk => {
                        body = chunk.toString();
                        body = JSON.parse(body);
                    });
                    req.on('end',  () => {
                        Pulpit.create({
                            PULPIT: body.PULPIT,
                            PULPIT_NAME: body.PULPIT_NAME,
                            FACULTY: body.FACULTY
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    })
                }
                else
                if (url.parse(req.url).pathname=="/api/subjects"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let body = ' ';
                    req.on('data', chunk => {
                        body = chunk.toString();
                        body = JSON.parse(body);
                    });
                    req.on('end',  () => {
                        Subject.create({
                            SUBJECT: body.SUBJECT,
                            SUBJECT_NAME: body.SUBJECT_NAME,
                            PULPIT: body.PULPIT
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    })
                }
                else
                if (url.parse(req.url).pathname=="/api/teachers"){
                    let body = '';
                    req.on('data', (chunk) => {
                        body += chunk;
                    });
                    req.on('end', async () => {
                        try {
                            const jsonData = JSON.parse(body);
                            console.log(jsonData);
                            await Teacher.create(jsonData);
                            resp.statusCode = 200;
                            resp.end('Данные получены и сохранены');
                        } catch (error) {
                            resp.statusCode = 500;
                            resp.end(error.message);
                        }
                    });
                }
                else
                if (url.parse(req.url).pathname=="/api/auditoriumstypes"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let body = ' ';
                    req.on('data', chunk => {
                        body = chunk.toString();
                        body = JSON.parse(body);
                    });
                    req.on('end',  () => {
                        Auditorium_type.create({
                            AUDITORIUM_TYPE: body.AUDITORIUM_TYPE,
                            AUDITORIUM_TYPENAME: body.AUDITORIUM_TYPENAME
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    })
                }
                else
                if (url.parse(req.url).pathname=="/api/auditoriums"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let body = ' ';
                    req.on('data', chunk => {
                        body = chunk.toString();
                        body = JSON.parse(body);
                    });
                    req.on('end',  () => {
                        Auditorium.create({
                            AUDITORIUM: body.AUDITORIUM,
                            AUDITORIUM_CAPACITY: body.AUDITORIUM_CAPACITY,
                            AUDITORIUM_NAME: body.AUDITORIUM_NAME,
                            AUDITORIUM_TYPE: body.AUDITORIUM_TYPE
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    })
                }
            }
            else
            if (req.method=="PUT"){
                if (url.parse(req.url).pathname=="/api/faculties"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let body = ' ';
                    req.on('data', chunk => {
                        body = chunk.toString();
                        body = JSON.parse(body);
                    });
                    req.on('end',  () => {
                        Faculty.findAll({
                            where: {FACULTY: body.FACULTY}
                        })
                            .then(table=>{
                                console.log(table.length)
                                if (table.length==0){
                                    resp.end(JSON.stringify({"error":"not found"}))
                                }
                                else {
                                    resp.end(JSON.stringify(table));
                                    Faculty.update({
                                            FACULTY_NAME: body.FACULTY_NAME
                                        },
                                        {
                                            where: {FACULTY: body.FACULTY}
                                        }
                                    );
                                }
                            })
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    })
                }
                else
                if (url.parse(req.url).pathname=="/api/pulpits"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let body = ' ';
                    req.on('data', chunk => {
                        body = chunk.toString();
                        body = JSON.parse(body);
                    });
                    req.on('end',  () => {

                        Pulpit.findAll({
                            where: {PULPIT: body.PULPIT}
                        })
                            .then(table=>{
                                console.log(table.length)
                                if (table.length==0){
                                    resp.end(JSON.stringify({"error":"not found"}))
                                }
                                else {
                                    resp.end(JSON.stringify(table));
                                    Pulpit.update({
                                            PULPIT_NAME: body.PULPIT_NAME,
                                            FACULTY: body.FACULTY
                                        },
                                        {
                                            where: {PULPIT: body.PULPIT}
                                        })
                                }
                            })
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    })
                }
                else
                if (url.parse(req.url).pathname=="/api/subjects"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let body = ' ';
                    req.on('data', chunk => {
                        body = chunk.toString();
                        body = JSON.parse(body);
                    });
                    req.on('end',  () => {

                        Subject.findAll({
                            where: {SUBJECT: body.SUBJECT}
                        })
                            .then(table=>{
                                console.log(table.length)
                                if (table.length==0){
                                    resp.end(JSON.stringify({"error":"not found"}))
                                }
                                else {
                                    resp.end(JSON.stringify(table));
                                    Subject.update({
                                            SUBJECT_NAME: body.SUBJECT_NAME,
                                            PULPIT: body.PULPIT
                                        },
                                        {
                                            where: {SUBJECT: body.SUBJECT}
                                        })
                                }
                            })
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    })
                }
                else
                if (url.parse(req.url).pathname=="/api/teachers"){
                    let body = '';
                    req.on('data', (chunk) => {
                        body += chunk;
                    });
                    req.on('end', async () => {
                        try {
                            const jsonData = JSON.parse(body);
                            console.log(jsonData);
                            await Teacher.update(jsonData, { where: { TEACHER: jsonData.TEACHER } })
                            resp.statusCode = 200;
                            resp.end('Данные получены и Обновлены');
                        } catch (error) {
                            resp.statusCode = 500;
                            resp.end(error.message);
                        }
                    });
                }
                else
                if (url.parse(req.url).pathname=="/api/auditoriumstypes"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let body = ' ';
                    req.on('data', chunk => {
                        body = chunk.toString();
                        body = JSON.parse(body);
                    });
                    req.on('end',  () => {

                        Auditorium_type.findAll({
                            where: {AUDITORIUM_TYPE: body.AUDITORIUM_TYPE}
                        })
                            .then(table=>{
                                console.log(table.length)
                                if (table.length==0){
                                    resp.end(JSON.stringify({"error":"not found"}))
                                }
                                else {
                                    resp.end(JSON.stringify(table));
                                    Auditorium_type.update({
                                            AUDITORIUM_TYPENAME: body.AUDITORIUM_TYPENAME
                                        },
                                        {
                                            where: {AUDITORIUM_TYPE: body.AUDITORIUM_TYPE}
                                        })
                                }
                            })
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    })
                }
                else
                if (url.parse(req.url).pathname=="/api/auditoriums"){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let body = ' ';
                    req.on('data', chunk => {
                        body = chunk.toString();
                        body = JSON.parse(body);
                    });
                    req.on('end',  () => {

                        Auditorium.findAll({
                            where: {AUDITORIUM: body.AUDITORIUM}
                        })
                            .then(table=>{
                                console.log(table.length)
                                if (table.length==0){
                                    resp.end(JSON.stringify({"error":"not found"}))
                                }
                                else {
                                    resp.end(JSON.stringify(table));
                                    Auditorium.update({
                                            AUDITORIUM_CAPACITY: body.AUDITORIUM_CAPACITY,
                                            AUDITORIUM_NAME: body.AUDITORIUM_NAME,
                                            AUDITORIUM_TYPE: body.AUDITORIUM_TYPE
                                        },
                                        {
                                            where: {AUDITORIUM: body.AUDITORIUM}
                                        })
                                }
                            })
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    })
                }
            }
            else
            if (req.method=="DELETE"){
                if (url.parse(req.url).pathname.split("/")[2]=="faculties" &&
                    codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
                    Faculty.findAll({
                        where: {FACULTY: code}
                    })
                        .then(table=>{
                            console.log(table.length)
                            if (table.length==0){
                                resp.end(JSON.stringify({"error":"not found"}))
                            }
                            else {
                                resp.end(JSON.stringify(table));
                                Faculty.destroy({
                                    where: {FACULTY: code}
                                })
                            }
                        })
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else
                if (url.parse(req.url).pathname.split("/")[2]=="pulpits" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);

                    Pulpit.findAll({
                        where: {PULPIT: code}
                    })
                        .then(table=>{
                            console.log(table.length)
                            if (table.length==0){
                                resp.end(JSON.stringify({"error":"not found"}))
                            }
                            else {
                                resp.end(JSON.stringify(table));
                                Pulpit.destroy({
                                    where: {PULPIT: code}
                                })
                            }
                        })
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})

                }
                else
                if (url.parse(req.url).pathname.split("/")[2]=="teachers" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
                    Teacher.findAll({
                        where: {TEACHER: code}
                    })
                        .then(table=>{
                            console.log(table.length)
                            if (table.length==0){
                                resp.end(JSON.stringify({"error":"not found"}))
                            }
                            else {
                                resp.end(JSON.stringify(table));
                                Teacher.destroy({
                                    where: {TEACHER: code}
                                })
                            }
                        })
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else
                if (url.parse(req.url).pathname.split("/")[2]=="subjects" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
                    Subject.findAll({
                        where: {SUBJECT: code}
                    })
                        .then(table=>{
                            console.log(table.length)
                            if (table.length==0){
                                resp.end(JSON.stringify({"error":"not found"}))
                            }
                            else {
                                resp.end(JSON.stringify(table));
                                Subject.destroy({
                                    where: {SUBJECT: code}
                                })
                            }
                        })
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else
                if (url.parse(req.url).pathname.split("/")[2]=="auditoriumstypes" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
                    Auditorium_type.findAll({
                        where: {AUDITORIUM_TYPE: code}
                    })
                        .then(table=>{
                            console.log(table.length)
                            if (table.length==0){
                                resp.end(JSON.stringify({"error":"not found"}))
                            }
                            else {
                                resp.end(JSON.stringify(table));
                                Auditorium_type.destroy({
                                    where: {AUDITORIUM_TYPE: code}
                                })
                            }
                        })
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
                else
                if (url.parse(req.url).pathname.split("/")[2]=="auditoriums" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
                    resp.writeHead(200,{"Content-Type":"application/json"});
                    let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
                    Auditorium.findAll({
                        where: {AUDITORIUM: code}
                    })
                        .then(table=>{
                            console.log(table.length)
                            if (table.length==0){
                                resp.end(JSON.stringify({"error":"not found"}))
                            }
                            else {
                                resp.end(JSON.stringify(table));
                                Auditorium.destroy({
                                    where: {AUDITORIUM: code}
                                })
                            }
                        })
                        .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                }
            }
        }).listen(3000);
    })

    .then(() => {
        return sequelize.transaction({isolationLevel: Sequelize.Transaction.ISOLATION_LEVELS.READ_UNCOMMITTED})
            .then(t => {
                return Auditorium.update(
                    {AUDITORIUM_CAPACITY: 0},
                    {where: {AUDITORIUM_CAPACITY: {[Op.gte]: 0}},
                        transaction: t
                    })
                    .then((r) => {
                        setTimeout(() => {
                            return t.rollback()
                        }, 10000);
                    })
                    .catch((e) => {
                        console.log(e.message);
                        return t.rollback();
                    });
            })
    })
    .catch(err=>{console.log("error",err);});

console.log("http://localhost:3000/api/subjects")
console.log("http://localhost:3000/api/faculties/ИДиП/subjects")
console.log("http://localhost:3000/api/auditoriumtypes/ЛК/auditoriums")
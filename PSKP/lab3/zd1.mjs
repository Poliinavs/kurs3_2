import { PrismaClient } from '@prisma/client';
const prisma = new PrismaClient({ log: ['query'] });
import http from "http";
import url from "url";
import fs from "fs";

http.createServer(async (req,resp)=>{
    const requestPath = decodeURI(url.parse(req.url).pathname);
    const pathParts = requestPath.split('/');
    let codeDelete=new RegExp("[A-Za-zА-Яа-я-]{0,6}",'g');
    // --------------GET---------------------------------
    if(req.method=="GET"){
        if (url.parse(req.url).pathname=="/"){
            resp.writeHead(200,{"Content-Type":"text/html"});
            resp.end(fs.readFileSync("zd1.html"));
        } else
        if ( pathParts.length === 5 &&
            pathParts[1] === 'api' &&
            pathParts[2] === 'faculties' &&
            pathParts[4] === 'subjects'){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
            prisma.FACULTY.findMany({
                where:{
                    FACULTY: code
                },
                select:{
                    FACULTY: true,
                    PULPIT_PULPIT_FACULTYToFACULTY: {
                        select:{
                            PULPIT: true,
                            SUBJECT_SUBJECT_PULPITToPULPIT: {
                                select: {
                                    SUBJECT: true
                                }
                            }
                        }}
                }
            })
                .then(data=>{console.log(data);resp.end(JSON.stringify(data))})
                .catch(e=>resp.end(JSON.stringify(e)))
        } else
        if ( pathParts.length === 5 &&
            pathParts[1] === 'api' &&
            pathParts[2] === 'auditoriumtypes' &&
            pathParts[4] === 'auditoriums'){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
            prisma.AUDITORIUM_TYPE.findMany({
                where:{
                    AUDITORIUM_TYPE: code
                },
                select:{
                    AUDITORIUM_TYPE: true,
                    AUDITORIUM_AUDITORIUM_AUDITORIUM_TYPEToAUDITORIUM_TYPE: {
                        select:{
                            AUDITORIUM: true
                        }}
                }
            })
                .then(data=>{
                    console.log(data);
                    resp.end(JSON.stringify(data))})
                .catch(e=>resp.end(JSON.stringify(e)))
        } else
        if (url.parse(req.url).pathname=="/api/auditoriumsWithComp1"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            prisma.AUDITORIUM.findMany({
                where:{
                    AUDITORIUM_TYPE: "ЛБ-К",
                    AUDITORIUM:{
                        contains: "-1"
                    }
                }
            })
                .then(data=>{console.log(data);resp.end(JSON.stringify(data))})
                .catch(e=>resp.end(JSON.stringify(e)))
        } else
        if (url.parse(req.url).pathname=="/api/puplitsWithoutTeachers"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            prisma.PULPIT.findMany({
                where:{
                    TEACHER_TEACHER_PULPITToPULPIT: {
                        none: {}
                    }
                },
            })
                .then(data=>{console.log(data);resp.end(JSON.stringify(data))})
                .catch(e=>resp.end(JSON.stringify(e)))
        } else
        if (url.parse(req.url).pathname=="/api/pulpitsWithVladimir"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            prisma.PULPIT.findMany({
                where:{
                    TEACHER_TEACHER_PULPITToPULPIT: {
                        some: {
                            TEACHER_NAME:{
                                contains: "Владимир"
                            }
                        }
                    }
                },
            })
                .then(data=>{console.log(data);resp.end(JSON.stringify(data))})
                .catch(e=>resp.end(JSON.stringify(e)))
        } else
        if (url.parse(req.url).pathname=="/api/auditoriumsSameCount"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            prisma.AUDITORIUM.groupBy({
                by: [
                    "AUDITORIUM_TYPE",
                    "AUDITORIUM_CAPACITY"
                ],
                _count: true
            })
                .then(data=>{console.log(data);resp.end(JSON.stringify(data))})
                .catch(e=>resp.end(JSON.stringify(e)))
        } else
        if (url.parse(req.url).pathname.split("/")[2]=="fluent" &&  codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
            prisma.AUDITORIUM_TYPE.findUnique({
                where: {AUDITORIUM_TYPE: code}
            }).AUDITORIUM_AUDITORIUM_AUDITORIUM_TYPEToAUDITORIUM_TYPE()
                .then(data=>{console.log(data);resp.end(JSON.stringify(data))})
                .catch(e=>resp.end(JSON.stringify(e)))
        } else
        if (url.parse(req.url).pathname.match('html/[0-9]{0,9}') !== null) {
            let code=decodeURI(url.parse(req.url).pathname.split("/")[2]);
            if (code == 1) {
                prisma.PULPIT.findMany({
                    take: 10,
                    include: {
                        _count: {
                            select: {
                                TEACHER_TEACHER_PULPITToPULPIT: true,
                            },
                        },
                    },
                })
                    .then(result => {
                        resp.end(JSON.stringify(result));
                    });
            } else {
                prisma.PULPIT.findMany({
                    take: 10,
                    skip: 10 * code - 10,
                    include: {
                        _count: {
                            select: {
                                TEACHER_TEACHER_PULPITToPULPIT: true,
                            },
                        },
                    },
                })
                    .then(result => {
                        resp.end(JSON.stringify(result));
                    });
            }

        } else
        if (url.parse(req.url).pathname.includes("api")!==false){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let value = table(decodeURIComponent(url.parse(req.url).pathname.split("/")[2]));
           await value.findMany()
                .then(data=>resp.end(JSON.stringify(data)))
                .catch(e=>resp.end(JSON.stringify(e)))
        }
    } else
        // --------------POST-------------------------------------
    if (req.method=="POST"){
        if (url.parse(req.url).pathname=="/api/faculties"){
            resp.writeHead(200, { "Content-Type": "application/json" });
            let body = '';

            req.on('data', chunk => {
                body += chunk.toString();
            });

            req.on('end', async () => {
                try {
                    body = JSON.parse(body);

                    const facultyData = {
                        FACULTY: body.FACULTY,
                        FACULTY_NAME: body.FACULTY_NAME
                    };

                    if (body.PULPIT_PULPIT_FACULTYToFACULTY !== undefined) {
                        facultyData.PULPIT_PULPIT_FACULTYToFACULTY = {
                            createMany: {
                                data: body.PULPIT_PULPIT_FACULTYToFACULTY.map(pulpit => ({
                                    PULPIT: pulpit.PULPIT,
                                    PULPIT_NAME: pulpit.PULPIT_NAME
                                }))
                            }
                        };
                    }

                    const result = await prisma.FACULTY.upsert({
                        where: { FACULTY: body.FACULTY },
                        create: facultyData,
                        update: facultyData,
                        include: {
                            PULPIT_PULPIT_FACULTYToFACULTY: true
                        }
                    });

                    resp.end(JSON.stringify(result));
                } catch (error) {
                    console.error(error);

                }
            });

        } else
        if (url.parse(req.url).pathname=="/api/pulpits"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.PULPIT.create({
                    data:{
                        PULPIT: body.PULPIT,
                        PULPIT_NAME: body.PULPIT_NAME,
                        FACULTY_PULPIT_FACULTYToFACULTY:{
                            connectOrCreate: {
                                where:{
                                    FACULTY: body.FACULTY
                                },
                                create:{
                                    FACULTY: body.FACULTY,
                                    FACULTY_NAME: body.FACULTY_NAME
                                }
                            }
                        }
                    }
                })
                    .then(table=>{resp.end(JSON.stringify(table));})
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})

            })
        } else
        if (url.parse(req.url).pathname=="/api/subjects"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.SUBJECT.create({
                    data:{
                        SUBJECT: body.SUBJECT,
                        SUBJECT_NAME: body.SUBJECT_NAME,
                        PULPIT_SUBJECT_PULPITToPULPIT:{
                            connect: {
                                PULPIT: body.PULPIT
                            }
                        }
                    }
                })
                    .then(table=>{resp.end(JSON.stringify(table));})
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
            })
        } else
        if (url.parse(req.url).pathname=="/api/teachers"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.TEACHER.create({
                    data:{
                        TEACHER: body.TEACHER,
                        TEACHER_NAME: body.TEACHER_NAME,
                        PULPIT_TEACHER_PULPITToPULPIT:{
                            connect: {
                                PULPIT: body.PULPIT
                            }
                        }
                    }
                })
                    .then(table=>{resp.end(JSON.stringify(table));})
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
            })
        } else
        if (url.parse(req.url).pathname=="/api/auditoriumstypes"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.AUDITORIUM_TYPE.create({
                    data:{
                        AUDITORIUM_TYPE: body.AUDITORIUM_TYPE,
                        AUDITORIUM_TYPENAME: body.AUDITORIUM_TYPENAME
                    }
                })
                    .then(table=>{resp.end(JSON.stringify(table));})
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
            })
        } else
        if (url.parse(req.url).pathname=="/api/auditoriums"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.AUDITORIUM.create({
                    data:{
                        AUDITORIUM: body.AUDITORIUM,
                        AUDITORIUM_NAME: body.AUDITORIUM_NAME,
                        AUDITORIUM_CAPACITY: body.AUDITORIUM_CAPACITY,
                        AUDITORIUM_TYPE_AUDITORIUM_AUDITORIUM_TYPEToAUDITORIUM_TYPE:{
                            connect: {
                                AUDITORIUM_TYPE: body.AUDITORIUM_TYPE
                            }
                        }
                    }
                })
                    .then(table=>{resp.end(JSON.stringify(table));})
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
            })
        }
    } else
        // ------------PUT-----------------------------------
    if (req.method=="PUT"){
        if (url.parse(req.url).pathname=="/api/faculties"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.FACULTY.findMany({
                    where:{
                        FACULTY: body.FACULTY
                    }
                })
                    .then(table=>{
                        if (table.length==0){
                            resp.end(JSON.stringify({"error":"not found"}))
                        }
                        else {
                            prisma.FACULTY.update({
                                where: {
                                    FACULTY: body.FACULTY
                                },
                                data:{
                                    FACULTY_NAME: body.FACULTY_NAME
                                }
                            })
                                .then(table=>{resp.end(JSON.stringify(table));})
                                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                        }
                    })
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})

                // .then(table=>{resp.end(JSON.stringify(table));})
                // .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
            });
        } else
        if (url.parse(req.url).pathname=="/api/pulpits"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.PULPIT.findMany({
                    where:{
                        PULPIT: body.PULPIT
                    }
                })
                    .then(table=>{
                        if (table.length==0){
                            resp.end(JSON.stringify({"error":"not found"}))
                        }
                        else {
                            prisma.PULPIT.update({
                                where: {
                                    PULPIT: body.PULPIT
                                },
                                data:{
                                    PULPIT_NAME: body.PULPIT_NAME,
                                    FACULTY: body.FACULTY
                                }
                            })
                                .then(table=>{resp.end(JSON.stringify(table));})
                                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                        }
                    })
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
            });
        } else
        if (url.parse(req.url).pathname=="/api/subjects"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.SUBJECT.findMany({
                    where:{
                        SUBJECT: body.SUBJECT
                    }
                })
                    .then(table=>{
                        if (table.length==0){
                            resp.end(JSON.stringify({"error":"not found"}))
                        }
                        else {
                            prisma.SUBJECT.update({
                                where:{
                                    SUBJECT: body.SUBJECT
                                },
                                data:{
                                    SUBJECT_NAME: body.SUBJECT_NAME,
                                    PULPIT: body.PULPIT
                                }
                            })
                                .then(table=>{resp.end(JSON.stringify(table));})
                                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                        }
                    })
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
            });
        } else
        if (url.parse(req.url).pathname=="/api/teachers"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.TEACHER.findMany({
                    where:{
                        TEACHER: body.TEACHER
                    }
                })
                    .then(table=>{
                        if (table.length==0){
                            resp.end(JSON.stringify({"error":"not found"}))
                        }
                        else {
                            prisma.TEACHER.update({
                                where:{
                                    TEACHER: body.TEACHER
                                },
                                data:{
                                    TEACHER_NAME: body.TEACHER_NAME,
                                    PULPIT: body.PULPIT
                                }
                            })
                                .then(table=>{resp.end(JSON.stringify(table));})
                                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                        }
                    })
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
            });
        } else
        if (url.parse(req.url).pathname=="/api/auditoriumstypes"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.AUDITORIUM_TYPE.findMany({
                    where:{
                        AUDITORIUM_TYPE: body.AUDITORIUM_TYPE
                    }
                })
                    .then(table=>{
                        if (table.length==0){
                            resp.end(JSON.stringify({"error":"not found"}))
                        }
                        else {
                            prisma.AUDITORIUM_TYPE.update({
                                where:{
                                    AUDITORIUM_TYPE: body.AUDITORIUM_TYPE
                                },
                                data:{
                                    AUDITORIUM_TYPENAME: body.AUDITORIUM_TYPENAME
                                }
                            })
                                .then(table=>{resp.end(JSON.stringify(table));})
                                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                        }
                    })
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
            });
        } else
        if (url.parse(req.url).pathname=="/api/auditoriums"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let body = ' ';
            req.on('data', chunk => {
                body = chunk.toString();
                body = JSON.parse(body);
            });
            req.on('end',  () => {
                prisma.AUDITORIUM.findMany({
                    where:{
                        AUDITORIUM: body.AUDITORIUM
                    }
                })
                    .then(table=>{
                        if (table.length==0){
                            resp.end(JSON.stringify({"error":"not found"}))
                        }
                        else {
                            prisma.AUDITORIUM.update({
                                where:{
                                    AUDITORIUM: body.AUDITORIUM
                                },
                                data:{
                                    AUDITORIUM_NAME: body.AUDITORIUM_NAME,
                                    AUDITORIUM_CAPACITY: body.AUDITORIUM_CAPACITY,
                                    AUDITORIUM_TYPE: body.AUDITORIUM_TYPE
                                }
                            })
                                .then(table=>{resp.end(JSON.stringify(table));})
                                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                        }
                    })
                    .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
            });
        } else
        if(url.parse(req.url).pathname=="/transaction"){
            resp.writeHead(200,{"Content-Type":"application/json"});
            prisma.$transaction(
                (t)=>{
                    prisma.AUDITORIUM.updateMany({
                        data: {AUDITORIUM_CAPACITY:{increment: 100}}

                    });
                    t.$queryRaw`Rollback transaction`;
                    // .then(table=>{resp.end(JSON.stringify(table));})
                    // .catch(err=>resp.end(JSON.stringify(err)))
                },
                {isolationLevel: Prisma.TransactionIsolationLevel.Serializable}
            )
                .then(()=>{console.log("rollback");resp.end(JSON.stringify({"transaction":"rollback"}));})
                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
        }
    } else
    if (req.method=="DELETE"){
        if (url.parse(req.url).pathname.split("/")[2]=="faculties" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
            prisma.FACULTY.findUnique({
                where: {FACULTY: code}
            })
                .then(table=>{
                    if (table==null){
                        resp.end(JSON.stringify({"error":"not found"}))
                    }
                    else {
                        prisma.FACULTY.delete({
                            where: {FACULTY: code}
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    }
                })
                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
        }  else
        if (url.parse(req.url).pathname.split("/")[2]=="pulpits" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
            prisma.PULPIT.findUnique({
                where: {PULPIT: code}
            })
                .then(table=>{
                    if (table==null){
                        resp.end(JSON.stringify({"error":"not found"}))
                    }
                    else {
                        prisma.PULPIT.delete({
                            where: {PULPIT: code}
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    }
                })
                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
        } else
        if (url.parse(req.url).pathname.split("/")[2]=="subjects" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
            prisma.SUBJECT.findUnique({
                where: {SUBJECT: code}
            })
                .then(table=>{
                    if (table==null){
                        resp.end(JSON.stringify({"error":"not found"}))
                    }
                    else {
                        prisma.SUBJECT.delete({
                            where: {SUBJECT: code}
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    }
                })
                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
        }  else
        if (url.parse(req.url).pathname.split("/")[2]=="teachers" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
            prisma.TEACHER.findUnique({
                where: {TEACHER: code}
            })
                .then(table=>{
                    if (table==null){
                        resp.end(JSON.stringify({"error":"not found"}))
                    }
                    else {
                        prisma.TEACHER.delete({
                            where: {TEACHER: code}
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    }
                })
                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
        } else
        if (url.parse(req.url).pathname.split("/")[2]=="auditoriumstypes" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
            prisma.AUDITORIUM_TYPE.findUnique({
                where: {AUDITORIUM_TYPE: code}
            })
                .then(table=>{
                    if (table==null){
                        resp.end(JSON.stringify({"error":"not found"}))
                    }
                    else {
                        prisma.AUDITORIUM_TYPE.delete({
                            where: {AUDITORIUM_TYPE: code}
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    }
                })
                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
        }  else
        if (url.parse(req.url).pathname.split("/")[2]=="auditoriums" && codeDelete.test(decodeURI(url.parse(req.url).pathname.split("/")[3]))){
            resp.writeHead(200,{"Content-Type":"application/json"});
            let code=decodeURI(url.parse(req.url).pathname.split("/")[3]);
            prisma.AUDITORIUM.findUnique({
                where: {AUDITORIUM: code}
            })
                .then(table=>{
                    if (table==null){
                        resp.end(JSON.stringify({"error":"not found"}))
                    }
                    else {
                        prisma.AUDITORIUM.delete({
                            where: {AUDITORIUM: code}
                        })
                            .then(table=>{resp.end(JSON.stringify(table));})
                            .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
                    }
                })
                .catch(err=>{console.log(err); resp.end(JSON.stringify(err));})
        }
    }
}).listen(3000);

const table=(value)=>{
    switch (value){
        case "faculties": return prisma.FACULTY;
        case "pulpits": return prisma.PULPIT;
        case "subjects": return prisma.SUBJECT;
        case "teachers": return prisma.TEACHER;
        case "auditoriumstypes": return prisma.AUDITORIUM_TYPE;
        case "auditoriums": return prisma.AUDITORIUM;
        default: break;
    }
}
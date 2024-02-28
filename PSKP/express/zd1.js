const express=require("express")
const bodyParser=require("body-parser")
const crudFunctions=require("./Functions/CrudFunctions")
const url=require("url")

const hbs = require("express-handlebars").create({
    extname: ".hbs",
    helpers: {
        back: () => {
            return '<a href="/"">back</a>';
        },
    },
});

let app=express()

app.engine(".hbs", hbs.engine);
app.set("view engine", ".hbs");
app.use(bodyParser.urlencoded({extended: false}))
app.use("/static", express.static("public"));

app.get("/",(req,resp)=>{
    resp.render("getAll.hbs",{
        list: crudFunctions.getAll(),
        title: "Phone book",
        click: true
    })
})
app.get("/add",(req,resp)=>{
    resp.render("post.hbs",{
        list: crudFunctions.getAll(),
        title: "Phone book",
        click: false
    })
})
app.get("/update",(req,resp)=>{
    var url_parts = url.parse(req.url, true);
    var query = url_parts.query;
    let result = crudFunctions.getAll();
    let send;
    for (var i = 0; i < result.length; i++) {
        if (result[i]["FIO"] == query["FIO"]) {
            send = result[i];
            break;
        }
    }
    resp.render("update.hbs",{
        list: crudFunctions.getAll(),
        title: "Phone book",
        FIO: send["FIO"],
        number: send["number"],
        click: false
    })
})
app.get("/delete",(req,resp)=>{
    resp.render("update.hbs",{
        list: crudFunctions.getAll(),
        title: "Phone book",
    })
})

app.post("/add",(req,resp)=>{
    crudFunctions.add(req.body)
    resp.redirect(303,"/")
})
app.post("/update",(req,resp)=>{
    console.log(req.body)
    crudFunctions.update(req.body)
    resp.redirect(303,"/")
})
app.post("/delete/:FIO",(req,resp)=>{
    crudFunctions.delete(req.params)
    resp.redirect(303,"/")
})

app.listen(3000)
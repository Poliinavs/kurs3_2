const app = require("express")();
const passport = require("passport");
const BasicStrategy = require("passport-http").BasicStrategy;
const session = require("express-session")({
    resave: false,
    saveUnitialized: false,
    secret: "leralera",
});

let users = require(__dirname + "/users.json");
app.use(session);
app.use(passport.initialize());
app.use(passport.session());


let getCredential=(user)=>{
    let u=users.find((e)=>e.user.toUpperCase()===user.toUpperCase())
    console.log(u)
    return u
}

let verPassword=(pass1,pass2)=>{return pass1===pass2}


passport.use(new BasicStrategy((user,password,done)=>{
    console.log("passport.use",user,password)
    let cr=getCredential(user)
    if (!cr){
        console.log("incorrect username")
        return done(null,false,{message:"incorrect username"})
    } 
    else if (!verPassword(cr.password,password)) {
        console.log("incorrect password")
        return done(null,false,{message:"incorrect password"})
    }
    else {
        console.log("success")
        return done(null,user, {message: "success"})
    }
}))

passport.serializeUser((user,done)=>{
    console.log("serialize",user)
    done(null,user)
})

passport.deserializeUser((user,done)=>{
    console.log("deserialize",user)
    done(null,user)
})


app.get("/login", (req,resp,next)=>{
    console.log("login")
    console.log(req.session.logout)
    if (req.session.logout && req.headers["authorization"]){
        console.log(req.headers["authorization"])
        req.session.logout=false
        delete req.headers["authorization"]
    }
    next()
}, passport.authenticate("basic"), (req,resp,next)=>{
    if (req.session.logout==undefined) req.session.logout=false
    next()
}
).get("/login",(req,resp,next)=>{
    resp.end("login")
})


app.get("/logout",(req,resp)=>{
    console.log("logout")
    req.session.logout=true
    delete req.headers["authorization"]

    resp.redirect("/login")
})

app.get("/resource",(req,resp,next)=>{
    if (req.session.logout === false && req.headers["authorization"])
        resp.end("Resource")
    else resp.redirect("/login")
})+

app.use((err,req,resp,next)=>{
    resp.status(404).send(err)
})

app.listen(3000)
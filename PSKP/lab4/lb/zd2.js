const app = require("express")();
const passport = require("passport");
const DigestStrategy = require("passport-http").DigestStrategy;
const session = require("express-session")({
    resave: false,
    saveUninitialized: false, // Corrected typo here
    secret: "SomeSecret",
});

let users = require(__dirname + "/users.json");
const express = require("express");
app.use(session);
app.use(passport.initialize());
app.use(passport.session());


let getCredential=(user)=>{
    let u=users.find((e)=>e.user.toUpperCase()===user.toUpperCase())
    console.log(u)
    return u
}

passport.use(new DigestStrategy({qop: 'auth'},(user,done)=>{
    // console.log("passport.use",user,password)
    let cr=getCredential(user)
    if (!cr){
        console.log("incorrect username")
        return done(null,false)
    }
    else {
        return done(null, cr.user,cr.password)
    }
    },(params,done)=>{
        done(null,true)
}))

passport.serializeUser((user,done)=>{
    done(null,user)
})

passport.deserializeUser((user,done)=>{
    done(null,user)
})

app.get("/login", (req,resp,next)=>{
    if (req.session.logout && req.headers["authorization"]){
        console.log(req.headers["authorization"])
        req.session.logout=false
        delete req.headers["authorization"]
    }
    next()
}, passport.authenticate("digest", {session: false}), (req,resp,next)=>{
    if (req.session.logout==undefined)
        req.session.logout=false
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
})

app.use((err,req,resp,next)=>{
    resp.status(404).send(err)
})

app.listen(3000)
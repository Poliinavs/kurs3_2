const express = require("express");
const app = express();
const bodyParser = require("body-parser");
const session = require("express-session");
const passport = require("passport");
const localStrategy = require("passport-local").Strategy;
const users = require("./users.json");

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(session({
    secret: "SomeSecret",
    resave: true,
    saveUninitialized: false
}));
app.use(passport.initialize());
app.use(passport.session());

passport.serializeUser((user, done) => done(null, user));
passport.deserializeUser((user, done) => done(null, user));

passport.use(
    new localStrategy((username, password, done) => {
        const user = users.find(u => u.username === username && u.password === password);
        if (user) {
            return done(null, user);
        }
        return done(null, false, { message: "Wrong username or password" });
    })
);

app.get("/login", (req, resp) => {
    resp.send("<form method='post' action='/login'>" +
        "<input type='text' name='username' required>" +
        "<input type='password' name='password' required>" +
        "<button type='submit'>Login</button>" +
        "</form>");
});

app.post("/login",
    passport.authenticate("local", {
        successRedirect: "/resource",
        failureRedirect: "/login"
    })
);

app.get("/resource", (req, resp) => {
    if (req.isAuthenticated()) {
        resp.send(`resource page<br/>Username: ${req.user.username}`);
    } else {
        resp.redirect('/login');
    }
});

app.get("/logout", (req, resp) => {

    req.logout(()=>{});
    resp.redirect('/login');
});

app.use((err, req, resp, next) => {
    resp.status(404).send("Not Found");
});

app.listen(3000);

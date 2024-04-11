const express = require('express');
const session = require('express-session');

const passport = require('passport');
const GoogleStrategy = require('passport-google-oauth2').Strategy;

const GOOGLE_CLIENT_ID = '9';
const GOOGLE_CLIENT_SECRET = '';

const isLoggedIn = (req, res, next) => {
    if (req.user) {
        next();
    } else {
        res.redirect('/login');
    }
};

const app = express();

app.use(session({
    secret: 'someSecret',
    resave: false,
    saveUninitialized: false
}));
app.use(passport.initialize());
app.use(passport.session());
passport.use(
    new GoogleStrategy(
        {
            clientID: GOOGLE_CLIENT_ID,
            clientSecret: GOOGLE_CLIENT_SECRET,
            callbackURL: 'http://localhost:3000/auth/google/callback',
        },
        function (accessToken, refreshToken, profile, done) {
            return done(null, profile);
        }
    )
);

passport.serializeUser(function (user, done) {
    done(null, user);
});

passport.deserializeUser(function (user, done) {
    done(null, user);
});

app.get('/login', (req, res) => {
    res.sendFile(__dirname + '/zd1.html');
});

app.get(
    '/auth/google',
    passport.authenticate('google', { scope: ['email', 'profile'] })
);

app.get(
    '/auth/google/callback',
    passport.authenticate('google', {
        successRedirect: '/resource',
        failureRedirect: '/login',
    })
);

app.get('/resource', isLoggedIn, (req, res) => {
    res.send(`MainPage you credentials<br>${req.user.id}<br>${req.user.displayName}`);
});

app.get('/logout', (req, res, next) => {
    req.logout(function (err) {
        if (err) {
            return next(err);
        }
    });
    res.redirect('/login');

});

app.all('*', (req, res) => {
    res.status(404);
    throw new Error('Page not found');
});

app.use(function (err, req, res, next) {
    res.send(err.message);
});

app.listen(3000);
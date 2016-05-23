var express = require("express");
var app = express();
var path = require('path');
var bodyParser = require('body-parser');
var cookieParser = require("cookie-parser");

// models
var mongoose = require("mongoose");
require("./models");

var port = 1337;

app.use(cookieParser());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

require("./authentication")(app);
app.use((req, res, next) => (console.log("running"), next()));
// api routes
app.use("/api", require("./routes"));



app.use((err, req, res, next) => console.error(err, err.stack));

app.listen(port, () => console.log("listening on port:", port));


var express = require("express");
var app = express();
var path = require('path');
var bodyParser = require('body-parser');

// models
var mongoose = require("mongoose");
require("./models");
require("./authentication")(app);

var port = 1337;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

// api routes
app.use("/api", require("./routes"));

app.get("/", (req, res, next) => {
  
});

app.post("/", (req, res, next) => {

});

app.use((err, req, res, next) => console.error(err, err.stack));

app.listen(port, () => console.log("listening on port:", port));

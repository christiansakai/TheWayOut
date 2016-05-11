var express = require("express");
var app = express();
var path = require('path');
var bodyParser = require('body-parser');

var mongoose = require("mongoose");
require("./models");

var port = 1337;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));


app.user("/api", require("./routes"));

app.get("/", (req, res, next) => {
  
});

app.post("/", (req, res, next) => {

});

app.use((err, req, res, next) => console.error(err, err.stack));

app.listen(port, () => console.log("listening on port:", port));

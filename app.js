var express = require("express");
var app = express();
var path = require('path');
var bodyParser = require('body-parser');
var port = 1337;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

var playerCount = 0;
var bestTime = 999999;

app.get("/", (req, res, next) => {
  playerCount += 1;
  res.json({count: playerCount, bestTime: bestTime});

});

app.post("/", (req, res, next) => {
console.log(req.body);
	if(req.body.score < bestTime) {
    bestTime = req.body.score;
  }
  res.sendStatus(201);
});

app.use(function (err, req, res, next) {
    console.error(err, err.stack);
});

app.listen(port, () => {
  console.log("listening on port:", port);
});

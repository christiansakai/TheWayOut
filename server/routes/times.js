var mongoose = require("mongoose");
var Time = mongoose.model("Time");

module.exports = require("express").Router()

.get("/", (req, res, next) => {

})

.post("/", (req, res, next) => {
	Time.createTime(req.body)
	.then(time => res.json(time));

})

.put("/", (req, res, next) => {

})

.delete("/:id", (req, res, next) => {

});

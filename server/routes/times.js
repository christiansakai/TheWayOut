var mongoose = require("mongoose");
var Time = mongoose.model("Time");

module.exports = require("express").Router()

.get("/", (req, res, next) => {
  Time.find({})
  .populate("player level")
  .then(levels => levels.sort((a, b) => a.time - b.time))
  .then(levels => res.json(levels))
  .catch(next);
})

.post("/", (req, res, next) => {
	Time.createTime(req.body)
	.then(time => res.json(time));

})

.put("/", (req, res, next) => {

})

.delete("/:id", (req, res, next) => {

});

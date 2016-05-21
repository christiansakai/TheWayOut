var mongoose = require("mongoose");
var Time = mongoose.model("Time");
var Level = mongoose.model("Level");

module.exports = require("express").Router()

.get("/", (req, res, next) => {
  Time.find({})
  .populate("player level")
  .sort({time: 1})
  .then(levels => levels.map(level => ((level.player = level.player.sanitize()), level)))
  .then(levels => res.json(levels))
  .catch(next);
})

.get("/:level", ({params}, res, next) => {
  var seen = {};
  Level.findOne({name: params.level})
  .then(level => Time.find({level: level._id}))
  .sort({time: 1})
  .then(times => times.filter(({player}) => seen.hasOwnProperty(player) ? false : (seen[player] = true)))
  .limit(100)
  .catch(next);
})

.post("/", (req, res, next) => {
	Time.createTime(req.body)
	.then(time => res.json(time))
  .catch(next);
})

.put("/", (req, res, next) => {

})

.delete("/:id", (req, res, next) => {

});

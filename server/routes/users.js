var mongoose = require("mongoose");
var User = mongoose.model("User");
var Level = mongoose.model("Level");


module.exports = require("express").Router()

.get("/", (req, res, next) => {
  User.find()
  .then(players => players.map(player => player.sanitize()))
  .then(players => res.json(players))
  .catch(next);
})

.post("/", ({body}, res, next) => {
  User.create(body)
  .then(player => res.json(player))
  .catch(next);
})

.param("id", (req, res, next, id) => {
  User.findById(id)
  .populate("currentLevel")
  .populate("respawnPoint")
  .then(user => req.user = user)
  .then(() => next(), next);
})

.get("/:id/:level",(req, res, next) => {
  User.findTimeOfOneLevel(req.user._id, req.params.level)
  .then(timeinfo => res.json(timeinfo));
})

.get("/:id", ({user}, res, next) => res.json(user.sanitize()))


.post("/:id", ({user, body}, res, next) => {
  // body {currentLevel, X,Y,Z,Angle}
  console.log(body)
  user.updateInfos(body)
  .then(updatedPlayer => res.json(updatedPlayer.sanitize()))
  .catch(next);
})

.delete("/:id", ({user}, res, next) => {

});


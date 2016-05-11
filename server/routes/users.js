var mongoose = require("mongoose");
var User = mongoose.model("User");

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
  .then(user => req.player = user)
  .then(() => next(), next);
})

.get("/:id", ({player}, res, next) => res.json(player))

.put("/:id", ({player, body}, res, next) => {
  player.set(body);
  player.save()
  .then(updatedPlayer => res.json(updatedPlayer))
  .catch(next);
})

.delete("/:id", ({player}, res, next) => {

});


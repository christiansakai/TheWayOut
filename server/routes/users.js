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
  .then(user => req.user = user)
  .then(() => next(), next);
})

.get("/:id", ({user}, res, next) => res.json(user))

.get("/:id/times",(req, res, next) => {
  User.findTimeOfOneLevel(req.body)
  .then(timeinfo => res.json(timeinfo));
})

.put("/:id", ({user, body}, res, next) => {
  User.set(body);
  User.save()
  .then(updatedPlayer => res.json(updatedPlayer))
  .catch(next);
})

.delete("/:id", ({user}, res, next) => {

});


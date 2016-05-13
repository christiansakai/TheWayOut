var mongoose = require("mongoose");
var Schema = mongoose.Schema;

var schema = new Schema({
  level: {
    type: Schema.Types.ObjectId,
    ref: "Level",
    required: true
  },
  player: {
    type: Schema.Types.ObjectId,
    ref: "User",
    required: true
  },
  time: {
    type: Number,
    required: true
  }
});


module.exports = mongoose.model("Time", schema);

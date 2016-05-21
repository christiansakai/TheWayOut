var mongoose = require("mongoose");
var Schema = mongoose.Schema;
var Level = mongoose.model("Level");

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
  },
  date:{
    type: Date,
    default: Date.now()
  }
});

schema.statics.createTime = function(reqbody){
    return Level.findOne({name: reqbody.level})
    .then(level => {
        return this.create({
          "level":level._id,
          "player":reqbody.player,
          "time": reqbody.time
        })
    });
}

schema.statics.getTopTimes = function (level, player) {
  var query = {};
  player && (query.player = player);
  return Level.findOne({name: level})
  .then(foundLevel => {
    if(foundLevel) {
      query.level = foundLevel.id;
    }
    return query;
  })
  .then(search => this.find(search).sort({time: 1}).populate("player"));
};

module.exports = mongoose.model("Time", schema);

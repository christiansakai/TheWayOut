var mongoose = require("mongoose");
var _ = require('lodash');
var crypto = require('crypto');
var Schema = mongoose.Schema;
var Time = mongoose.model("Time");
var Level = mongoose.model("Level");
var Respawnpoint = mongoose.model("Respawnpoint");

var schema = new Schema({
  name: {
    type: String,
    required: true,
    unique: true
  },
  email: {
    type: String,
    required: true,
    unique: true
  },
  password: {
    type: String,
    required: true
  },
  salt: {
    type: String
  },
  currentLevel: {
    type: Schema.Types.ObjectId,
    ref: "Level"
  },
  respawnPoint: {
    X: {
      type: Number,
      default: 0
    },
    Y: {
      type: Number,
      default: 0
    },
    Z: {
      type: Number,
      default: 0
    },
    Angle: {
      type: Number,
      default: 0
    }
  }
});

// get all the time for a level for a player
schema.methods.getTopTimes = function(level){
  return Time.getTopTimes(level, this._id)
  .then(times => times.slice(0, 50));
};

// update user infos
schema.methods.updateInfos = function(body){
  var respawnPoint = {X: body.X, Y: body.Y, Z: body.Z, Angle: body.Angle};
  return Level.findOne({name: body.currentLevel})
  .then(level => {
    level && this.set({currentLevel: level._id, respawnPoint});
    return this.save();
  });
};

// sanitize userinfo
schema.methods.sanitize = function () {
    return _.omit(this.toJSON(), ['password', 'salt']);
};

// hash the password with salt
var generateSalt = function () {
    return crypto.randomBytes(16).toString('base64');
};

var encryptPassword = function (plainText, salt) {
    var hash = crypto.createHash('sha1');
    hash.update(plainText);
    hash.update(salt);
    return hash.digest('hex');
};

schema.pre('save', function (next) {
	if (this.isModified('password')) {
		this.salt = this.constructor.generateSalt();
		this.password = this.constructor.encryptPassword(this.password, this.salt);
	}
  next();
});

schema.statics.generateSalt = generateSalt;
schema.statics.encryptPassword = encryptPassword;

schema.method('correctPassword', function (candidatePassword) {
    return encryptPassword(candidatePassword, this.salt) === this.password;
}); 

module.exports = mongoose.model("User", schema);

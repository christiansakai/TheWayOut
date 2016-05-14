var mongoose = require("mongoose");
var _ = require('lodash');
var crypto = require('crypto');
var Schema = mongoose.Schema;
var Time = mongoose.model("Time");
var Level = mongoose.model("Level");

var schema = new Schema({
  name: {
    type: String,
    required: true
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
  checkpoint: {
    type: Number
  }
});

// get all the time for a level for a player
schema.statics.findTimeOfOneLevel = function(userId, level){
  return Level.findOne({name: level})
  .then(foundlevel => {
    return Time.find({"player":userId,"level":foundlevel._id}).exec()
  })
  
}


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

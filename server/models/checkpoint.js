var mongoose = require("mongoose");
var _ = require('lodash');
var crypto = require('crypto');
var Schema = mongoose.Schema;

var schema = new Schema({
	x: Number,
	y: Number,
	z: Number
})
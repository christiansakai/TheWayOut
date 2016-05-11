var mongoose = require("mongoose");
mongoose.connect("mongodb://localhost/unityportal");
var db = mongoose.connection;
db.on("error", console.error.bind(console, "connection error:"));

require("./user");
require("./time");
require("./level");
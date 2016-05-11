module.exports = require("express").Router()
.use("/times", require("./times"))
.use("/levels", require("./levels"))
.use("/users", require("./users"))

.use((req, res) => res.sendStatus(404));
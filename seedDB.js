var mongoose = require('mongoose');
var spawn = require('child_process').spawn;
var DATABASE_URI = "mongodb://localhost/unityportal";

/* Connect to the DB */
mongoose.connect(DATABASE_URI,function(){
    mongoose.connection.once('open', function() {
        /* Drop the DB */
        mongoose.connection.db.dropDatabase(function (err) {
          console.log('db dropped');
          var args = ['--db', 'unityportal', './dump/unityportal']
          var mongorestore = spawn('/usr/local/bin/mongorestore', args);

          mongorestore.stdout.on('data', function (data) {
              console.log('stdout: ' + data);
          });
          mongorestore.stderr.on('data', function (data) {
              console.log('stderr: ' + data);
          });
          mongorestore.on('exit', function (code) {
            console.log('mongorestore exited with code ' + code);
          });
          //process.exit(0);
      })

    })

});


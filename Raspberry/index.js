var app = require('http').createServer(handler)
var io = require('socket.io')(app);
var fs = require('fs');
var Gpio = require('onoff').Gpio,


button = new Gpio(4, 'in', 'both');
app.listen(80);

function handler (req, res) {
  res.writeHead(200);
  res.end("Hello world");
}

io.on('connection', function (socket) {
  console.log("Connection found");
  button.watch(function(err, value) {
    console.log("button value "+value);
    socket.emit("button pressed");
  });
  console.log("Connection found");
  socket.emit('connectionConfirmed');
  socket.on('phone message', function (data) {
    console.log("new phone message"data);
  });
});

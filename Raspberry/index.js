var app = require('http').createServer(handler)
var io = require('socket.io')(app);
var fs = require('fs');
var Gpio = require('onoff').Gpio;

var button = new Gpio(4, 'in', 'both');
var isConnected = false;

app.listen(80);

button.watch(function(err, value) {
  console.log("button value "+value);
  if (isConnected) {socket.emit("button pressed");}
});

function handler (req, res) {
  res.writeHead(200);
  res.end("Hello world");
}

io.on('connection', function (socket) {
  console.log("Connection found");
  isConnected = true;
  socket.emit('connectionConfirmed');
  socket.on('phone message', function (data) {
    console.log("new phone message", data);
  });
});

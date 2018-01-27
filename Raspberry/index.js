var app = require('http').createServer(handler)
const WebSocket = require('ws');
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

var unityConnection = new WebSocket.Server({server: app});

unityConnection.on('connection', function (socket) {
  console.log("Connection found");
  isConnected = true;
  socket.emit('connectionConfirmed');
  socket.on('message', function (data) {
    console.log("new phone message", data);
  });
});

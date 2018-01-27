var app = require('http').createServer(handler)
const WebSocket = require('ws');
var fs = require('fs');
var Gpio = require('onoff').Gpio;

var button = new Gpio(4, 'in', 'both');
var isConnected = false;

app.listen(80);

button.watch(function(err, value) {
  console.log("button value "+value);
  if (isConnected) {unityConnection.send("button pressed");}
});

function handler (req, res) {
  res.writeHead(200);
  res.end("Hello world");
}

var socketServer = new WebSocket.Server({server: app});

var unityConnection = null;
socketServer.on('connection', function (socket) {
  unityConnection = socket;
  console.log("Connection found");
  isConnected = true;
  unityConnection.send(JSON.serialize(jsonTest));
  unityConnection.on('message', function (data) {
    console.log("new phone message", data);
  });
});


var jsonTest = {
  "instruction": "challengeStart",
  "challengeType": "Sequence",
  "sequenceList": ["red","green","blue"],
  "timeLeft": 40
  "challengeLeft": 10
}

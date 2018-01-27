var app = require('http').createServer(handler)
const WebSocket = require('ws');
var fs = require('fs');
var Inputs = require("./gpio_input.js");

var button = new Gpio(4, 'in', 'both');
var isConnected = false;

app.listen(80);

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
  unityConnection.send(JSON.stringify(jsonTest));
  unityConnection.on('message', function (data) {
    console.log("new phone message", data);
  });
});


var jsonTest = {
  "instruction": "challengeStart",
  "challengeType": "Sequence",
  "sequenceList": ["red","green","blue"],
  "timeLeft": 40,
  "challengeLeft": 10
}


console.log("server ready");

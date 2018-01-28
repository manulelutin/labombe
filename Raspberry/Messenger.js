var http = require('http')
const WebSocket = require('ws');
const EventEmitter = require('events');

class Messenger extends EventEmitter {
  constructor() {
    super();
    this.app = http.createServer((req, res) => { res.writeHead(200); res.end("Hello world"); });
    this.app.listen(80);

    this.socketServer = new WebSocket.Server({server: this.app});
    this.socketServer.on('connection', player => {
      this.player = player;
      console.log("Player detected");
      this.player.on('message', data => {
        var jsonData = JSON.parse(data);
        if (jsonData.instruction) {
          this.emit(jsonData.instruction);
        }
      });
    });
  }
  sendMessage(instruction, params) {
    if (this.player) {
      var json = Object.assign(params, {instruction})
      this.player.send(JSON.stringify(json));
    }
  }
  startChallenge(params) {
    this.sendMessage("challengeStart", params);
  }
  playSound(soundName) {
    this.sendMessage("playSound", { soundName });
  }
  playSoundError() {
    this.playSound("ERROR");
  }
}

module.exports = Messenger;

const Messenger = require("./Messenger.js");
const ChallengeSequence= require("./ChallengeSequence.js");
var Inputs = require("./Inputs.js");
const STOP = {stop:"stop"};

var AvaillableChallenge = [
  ChallengeSequence
];

class ChallengeEngine {
  constructor() {
    this.currentChallenge = null;
    this.messenger = new Messenger();
    this.messenger.on("startGame", () => this.onStartGame());
    console.log("listening");
    setTimeout(() => this.onStartGame(), 100);
  }
  onStartGame() {
    console.log("onStartGame");
    this.startRandomChallenge();
  }
  update() {
    //console.log("update");

    //Si le jeu a commencé
    if(this.currentChallenge) {
      var updateResult = this.currentChallenge.update(Inputs, this.messenger, STOP);
      if (updateResult === STOP) {
        this.startRandomChallenge();
      }
    }
    Inputs.update();
  }
  startRandomChallenge() {
    this.startChallenge(AvaillableChallenge[Math.floor(Math.random()*AvaillableChallenge.length)])
  }
  startChallenge(Challenge) {
    var challenge = new Challenge();
    var params = challenge.start();
    var item = Object.assign(params, {
      timeLeft: 100,
      challengeLeft: 10,
    });
    this.messenger.startChallenge(item);
    console.log("challenge " +item.challengeType+ " started");
    this.currentChallenge = challenge;
  }
}

module.exports = ChallengeEngine;

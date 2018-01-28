const Messenger = require("./Messenger.js");
const Challenges = require("./Challenges/");
var Inputs = require("./Inputs.js");
const SyncTimer = require("./SyncTimer.js");
const STOP = "STOP";

const CHALLENGE_COUNT = 3;


class ChallengeEngine {
  constructor() {
    this.currentChallenge = null;
    this.messenger = new Messenger();
    this.messenger.on("startGame", () => this.onStartGame());
    console.log("listening");


    //setTimeout(() => this.onStartGame(), 100);
  }
  onStartGame() {
    this.pitime = new SyncTimer(() => this.gameLose());
    console.log("onStartGame");
    this.startRandomChallenge();
    this.challengeCount = CHALLENGE_COUNT;
  }
  update() {
    //console.log("update");

    //Si le jeu a commencé
    if(this.currentChallenge) {
      var updateResult = this.currentChallenge.update(Inputs, this.messenger, STOP);
      if (updateResult === STOP) {
        this.stopChallenge();
      }
    }
    Inputs.update();
  }
  stopChallenge() {
    if (this.challengeLeft<0) {
      this.gameWin();
    } else {
      this.startRandomChallenge();
    }

  }
  gameWin() {
    console.log("gameWin")
    this.pitime.stop();
    this.currentChallenge = null;
    this.messenger.gameWin(this.pitime.timeLeft);
  }
  gameLose() {
    console.log("game over");
    this.pitime.stop();
    this.currentChallenge = null;
    this.messenger.gameLose();
  }
  startRandomChallenge() {
    this.startChallenge(Challenges[Math.floor(Math.random()*Challenges.length)])
  }
  startChallenge(Challenge) {
    this.challengeCount--;
    var challenge = new Challenge();
    var params = challenge.start(Inputs);
    var item = Object.assign(params, {
      timeLeft: this.pitime.timeLeft(),
      challengeLeft: this.challengeCount,
    });
    this.messenger.startChallenge(item);
    console.log("challenge " +item.challengeType+ " started. "+this.pitime.timeLeft()+" seconds left.");
    this.currentChallenge = challenge;
  }
}

module.exports = ChallengeEngine;

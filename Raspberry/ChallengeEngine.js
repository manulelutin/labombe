const Messenger = require("./Messenger.js");
const Challenges = require("./Challenges/");
var Inputs = require("./Inputs.js");
const SyncTimer = require("./SyncTimer.js");
const STOP = "STOP";

const CHALLENGE_COUNT = 10;


class ChallengeEngine {
  constructor() {
    this.currentChallenge = null;
    this.messenger = new Messenger();
    this.messenger.on("startGame", () => this.onStartGame());
    console.log("listening");


    //setTimeout(() => this.onStartGame(), 100);
  }
  onStartGame() {
    if (this.pitime) {
      this.pitime.stop();
    }
    this.pitime = new SyncTimer(() => this.gameLose());
    console.log("onStartGame");
    this.challengeLeft = CHALLENGE_COUNT;
    this.startRandomChallenge();

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

    //console.log("1 : "+Inputs.getButtonDown("cableGrey")+ " 2 :  " + Inputs.getButtonDown("cablePurple")+ " 3 :  " +  Inputs.getButtonDown("cableBlack")+ " 4 :  " +  Inputs.getButtonDown("cableWhite"));
    Inputs.update();
  }
  stopChallenge() {
    console.log("stop challenge");
    if (this.challengeLeft<=0) {
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
    console.log("jdjdddddddddddddddddddd");
    this.startChallenge(Challenges[Math.floor(Math.random()*Challenges.length)])
  }
  startChallenge(Challenge) {
    this.challengeLeft--;
    var challenge = new Challenge();
    var params = challenge.start(Inputs);
    var item = Object.assign(params, {
      timeLeft: this.pitime.timeLeft(),
      challengeLeft: this.challengeLeft,
    });
    this.messenger.startChallenge(item);
    console.log("challenge " +item.challengeType+ " started. "+this.pitime.timeLeft()+" seconds left.");
    this.currentChallenge = challenge;
  }
}

module.exports = ChallengeEngine;

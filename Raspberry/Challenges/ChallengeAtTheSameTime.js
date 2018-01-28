const Challenge = require("./Challenge.js");
const {randomInt, shuffle} = require("./utils.js");
const BUTTONS_NAME = ["red","green","blue","yellow"];

class ChallengeAtTheSameTime extends Challenge {

  start() {
    var count = randomInt(2,BUTTONS_NAME.length);
    this.selectedButton = shuffle(BUTTONS_NAME).slice(0,count);
    console.log("starting AtTheSameTime with "+this.selectedButton.join(", "));
    return {
      "challengeType": "AtTheSameTime",
      "selectedButton": this.selectedButton,
    }
  }

  update(inputs, messenger, STOP) {
    var isStop = BUTTONS_NAME.every(name => {
      var isGood = this.selectedButton.indexOf(name) >= 0;
      if (inputs.getButtonPressed(name)) {
        messenger.playSound(isGood ? "BUTTON" : "ERROR");
      }
      if (inputs.getButtonDown(name) == isGood) {
        return true;
      } else {
        return false;
      }
    })
    return isStop ? STOP : null;
  }
}

module.exports = ChallengeAtTheSameTime;

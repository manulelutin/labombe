const Challenge = require("./Challenge.js");
const {randomPick, shuffle} = require("./utils.js");


const BUTTONS_NAME = ["cableOrange","cableGrey","cableWhite","cableBlack", "cablePurple"];
const INPUTS_NAME = ["cablePink", "cableRed"];

class ChallengeSequence extends Challenge {

  start(inputs) {
    var availablePins = shuffle(BUTTONS_NAME.filter(name => !inputs.getButtonDown(name)));
    var availableInputs = shuffle(INPUTS_NAME);
    var count = randomInt(1,availableInputs);

    this.cablesConnection = [];
    this.selectedButton = [];
    for(var i =0; i<count;i++) {
      this.selectedButton[i] = availablePins[i];
      this.randomisedSequence[i] = [availableInputs[i], availablePins[i]];
    }
    console.log("starting Connect with "+this.randomisedSequence.join(", "));
    return {
      "challengeType": "Connect",
      "cablesConnection": this.cablesConnection,
    }
  }



  update(inputs, messager, STOP) {
    var isStop = BUTTONS_NAME.all(name => {
      var isGood = this.selectedButton.indexOf(name) >= 0;
      if (!isGood && inputs.getButtonPressed(name)) {
        //messager.playSound("buzzer");
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

module.exports = ChallengeSequence;

const Challenge = require("./Challenge.js");
const {randomPick, shuffle, randomInt} = require("./utils.js");


const BUTTONS_NAME = ["cableOrange","cableGrey","cableWhite","cableBlack", "cablePurple"];
const INPUTS_NAME = ["cablePink", "cableRed"];

class ChallengeSequence extends Challenge {

  start(inputs) {
    var availablePins = shuffle(BUTTONS_NAME.filter(name => !inputs.getButtonDown(name)));
    var availableInputs = shuffle(INPUTS_NAME);
    var count = randomInt(1,Math.min(availableInputs.length, availablePins.length));

    this.cablesConnection = [];
    this.selectedButton = [];
    for(var i =0; i<count;i++) {
      this.selectedButton[i] = availablePins[i];
      this.cablesConnection[i] = [availableInputs[i], availablePins[i]];
    }
    console.log("starting Connect with "+this.cablesConnection.join(", "));
    return {
      "challengeType": "Connect",
      "cablesConnection": this.cablesConnection,
    }
  }

  update(inputs, messenger, STOP) {
    var isStop = BUTTONS_NAME.every(name => {
      var isGood = this.selectedButton.indexOf(name) >= 0;
      if(inputs.getButtonPressed(name)) {
          messenger.playSound("ELECTRIC_ON")
      }
      if(inputs.getButtonPressed(name)) {
          messenger.playSound("ELECTRIC_OFF")
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

const Challenge = require("./Challenge.js");
const {randomPick, shuffle, randomInt} = require("./utils.js");


const BUTTONS_NAME = ["cableGrey","cableWhite","cableBlack", "cablePurple"];
const INPUTS_NAME = ["cableOrange"];

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

    var isStop = BUTTONS_NAME.reduce((old, name) => {
      console.log(name);
      var isGood = this.selectedButton.indexOf(name) >= 0;
      if(inputs.getButtonPressed(name)) {
          messenger.playSound("ELECTRIC_ON");
          console.log("ELECTRIC_ON");
      }
      if(inputs.getButtonPressed(name)) {
          messenger.playSound("ELECTRIC_OFF");
          console.log("ELECTRIC_OFF");
      }

      return old && inputs.getButtonDown(name) == isGood;

    }, true)
    return isStop ? STOP : null;
  }
}

module.exports = ChallengeSequence;

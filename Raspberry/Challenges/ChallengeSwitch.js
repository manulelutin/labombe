const Challenge = require("./Challenge.js");
const {randomPick, shuffle, randomInt} = require("./utils.js");


const SWITCHES = [
  ["switch1up", "switch1down"],
  ["switch2up", "switch2down"],
  ["switch3up", "switch3down"]
];
const POSSIBLE_STATE = ["up","down","off"];

const getCurrentState = (switchPins, inputs) => {
  return inputs.getButtonDown(switchPins[0]) ? "up" : inputs.getButtonDown(switchPins[1]) ? "down" : "off";
}


const getCurrentStateChange = (switchPins, inputs) => {
  return inputs.getButtonPressed(switchPins[0]) ? "up" : inputs.getButtonPressed(switchPins[1]) ? "down" : "off";
}

const hasChanged = (switchPins, inputs) => {
  return inputs.getButtonPressed(switchPins[0]) || inputs.getButtonReleased(switchPins[0]) || inputs.getButtonPressed(switchPins[1]) || inputs.getButtonReleased(switchPins[1]);
}

class ChallengeSequence extends Challenge {

  start(inputs) {
    this.switchState = SWITCHES.map(switchPins => randomPick(POSSIBLE_STATE.filter(state => state != getCurrentState(switchPins, inputs))));
    console.log("starting Switch with "+this.switchState.join(", "));
    return {
      "challengeType": "Switch",
      "switchState": this.switchState,
    }
  }

  update(inputs, messenger, STOP) {
    var isStop = SWITCHES.reduce((old, switchPins, i) => {

      var state = getCurrentState(switchPins, inputs);
      var change = getCurrentStateChange(switchPins, inputs);
      //if (change=="up") { }
      //if (change=="off") {messenger.playSound("SWITCH_OFF"); }
      //if (change=="down") {messenger.playSound("SWITCH_DOWN"); }
      if (hasChanged(switchPins, inputs)) {
        if (state == this.switchState[i]) {
          messenger.playSound("SWITCH_UP");
        } else {
          messenger.playSound("SWITCH_OFF");
        }
      }
      return old && (state == this.switchState[i]);
    }, true);
    return isStop ? STOP : null;
  }
}

module.exports = ChallengeSequence;

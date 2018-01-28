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

class ChallengeSequence extends Challenge {

  start(inputs) {
    this.switchState = SWITCHES.map(switchPins => randomPick(POSSIBLE_STATE.filter(state => state != getCurrentState(switchPins, inputs))));
    console.log("starting Switch with "+this.switchState.join(", "));
    return {
      "challengeType": "Switch",
      "switchState": this.switchState,
    }
  }

  update(inputs, messager, STOP) {
    var isStop = SWITCHES.every((switchPins, i) => {
      var state = getCurrentState(switchPins, inputs);
      if(state == this.switchState[i]) {
        return true;
      }
    });
    return isStop ? STOP : null;
  }
}

module.exports = ChallengeSequence;

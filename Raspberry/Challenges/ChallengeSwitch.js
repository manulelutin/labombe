const Challenge = require("./Challenge.js");
const {randomPick, shuffle} = require("./utils.js");


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
    var availablePins = shuffle(BUTTONS_NAME.filter(name => !inputs.getButtonDown(name)));
    var availableInputs = shuffle(INPUTS_NAME);
    var count = randomInt(1,availableInputs);

    this.switchState = SWITCHES.map(switchPins => randomPick(POSSIBLE_STATE.filter(state => state != getCurrentState(switchPins, inputs))));
    return {
      "challengeType": "Switch",
      "switchState": this.switchState,
    }
  }

  update(inputs, messager, STOP) {
    var isStop = SWITCHES.all((switchPins, i) => {
      var state = getCurrentState(switchPins, inputs);
      if(state == this.switchState[i]) {
        return true;
      }
    });
    return isStop ? STOP : null;
  }
}

module.exports = ChallengeSequence;

const Challenge = require("./Challenge.js");
const {randomInt, randomPick} = require("./utils.js");

const BUTTONS_NAME = ["red","green","blue","yellow"];
const counts = [100, 100, 50, 50, 25];

class ChallengeRepeat extends Challenge {

  start() {
    this.count = randomPick(counts);
    this.button = randomPick(BUTTONS_NAME);
    console.log("starting Repeat with "+this.button+" "+this.count+" times");
    return {
      "challengeType": "Repeat",
      "button": this.button,
      "count": this.count
    }
  }

  update(inputs, messager, STOP) {
    if (inputs.getButtonPressed(this.button)) {
      this.count--;
      console.log(this.count+" repetition remaining")
    }
    return this.count<=0 ? STOP : null;
  }
}

module.exports = ChallengeRepeat;

const Challenge = require("./Challenge.js");
const {randomInt, randomPick} = require("./utils.js");

const BUTTONS_NAME = ["red","green","yellow"];
const COUNTS = [3,3,4,4,4,4,5,5,6,7,8, 9]

class ChallengeSequence extends Challenge {

  start() {
    this.randomisedSequence = [];
    this.sequenceProgress = 0;
    var count = randomPick(COUNTS);
    console.log(count);
    for(var i =0; i<count;i++) {
      this.randomisedSequence[i] = BUTTONS_NAME[Math.floor(Math.random()*BUTTONS_NAME.length)];
    }
    console.log("starting sequence with "+this.randomisedSequence.join(", "));
    return {
      "challengeType": "Sequence",
      "sequenceList": this.randomisedSequence,
    }
  }

  update(inputs, messenger, STOP) {
    var targetKey = this.randomisedSequence[this.sequenceProgress];
    var isStop =false;
    BUTTONS_NAME.forEach(name => {


      if (inputs.getButtonPressed(name)) {
        if (name === targetKey) {
          this.sequenceProgress++;
          messenger.playSound("BUTTON");
          console.log("sequence progressed to "+this.sequenceProgress)
          if (this.sequenceProgress>=this.randomisedSequence.length) {
            console.log("finished");
            isStop = true;

          }

        } else {
          messenger.playSoundError();
          this.sequenceProgress = 0;

          console.log("sequence back to zero because the player pressed on "+name+" instead of "+targetKey);
        }
      }
    })
    return isStop ? STOP : null;
  }
}

module.exports = ChallengeSequence;

const Challenge = require("./Challenge.js");

const BUTTONS_NAME = ["red","green","blue","yellow"];

class ChallengeSequence extends Challenge {

  start() {
    this.randomisedSequence = [];
    this.sequenceProgress = 0;
    for(var i =0; i<3;i++) {
      this.randomisedSequence[i] = BUTTONS_NAME[Math.floor(Math.random()*BUTTONS_NAME.length)];
    }
    console.log("starting sequence with "+this.randomisedSequence.join(", "));
    return {
      "challengeType": "Sequence",
      "sequenceList": this.randomisedSequence,
    }
  }

  update(inputs, messager, STOP) {
    var targetKey = this.randomisedSequence[this.sequenceProgress];
    BUTTONS_NAME.forEach(name => {
      if (inputs.getButtonDown(name)) {
        if (name === targetKey) {
          this.sequenceProgress++;
          console.log("sequence progressed to "+this.sequenceProgress)
          if (this.sequenceProgress>=this.randomisedSequence.length) {
            return STOP;
          }

        } else {
          //Play buzzer
          this.sequenceProgress = 0;
          console.log("sequence back to zero")
        }
      }
    })

  }
}

module.exports = ChallengeSequence;

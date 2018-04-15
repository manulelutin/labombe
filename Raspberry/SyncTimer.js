/**
 * Created by emmanuel on 27/01/2018.
 */
const Messenger = require("./Messenger.js");
var pi_gametime=0;
const maxtime = 90;



class SyncTimer {
    constructor(callback) {
        this.timeStart = Date.now();
        this.callback = callback
        this.setTimer()
    }
    timeLeft() {
      var t = (Date.now() - this.timeStart)/1000;
      return t;
    }
    setTimer() {
        //We're using second so * 1000.
        pi_gametime = setTimeout(() => this.endTimer(), maxtime * 1000);
        console.log("start challenge pi time");
    }
    endTimer() {
        this.stop();
        this.callback();
    }
    stop() {
      clearTimeout(pi_gametime);
    }
}

module.exports = SyncTimer;

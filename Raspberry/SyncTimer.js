/**
 * Created by emmanuel on 27/01/2018.
 */
const Messenger = require("./Messenger.js");
var pi_gametime=0;
const maxtime = 100;

class SyncTimer {
    constructor(messenger) {
        messenger.on("startGame", () => this.setTimer());
    }
    setTimer() {
        pi_gametime = setTimeout(this.endTimer(), maxtime);
        console.log("start challenge pi time");
    }
    endTimer() {
        console.log("end challenge pi time");
    }
}

module.exports = SyncTimer;

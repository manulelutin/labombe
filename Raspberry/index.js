var ChallengeEngine = require("./ChallengeEngine.js");

var engine = new ChallengeEngine();

this.gameLoop = setInterval(() => engine.update(), 50);

console.log("server ready");

var Gpio = require('onoff').Gpio;

var mappingButton = {
  "red": 10,
  "green": 17,
  "blue": 27,
  "yellow": 22,
  "switch1up": 5,
  "switch1down": 6,
  "switch2up": 13,
  "switch2down": 19,
  "switch3up": 26,
  "switch3down": 21,
  "cableOrange":3,
  "cableGrey":15,
  "cableWhite":18,
  "cableBlack":23,
  "cablePurple":24
}

var keys = {};
var keysDown = {};
var keysUp = {};

function getButtonDown(e) {
  return !!keys[e];
}
function getButtonPressed(e) {
  return !!keysDown[e];
}
function getButtonReleased(e) {
  return !!keysUp[e];
}
function update() {
  keysDown = {};
  keysUp = {};
}

Object.keys(mappingButton).forEach(key => {
  button = new Gpio(mappingButton[key], 'in', 'both');

  button.watch(function(err, value) {
    console.log("button "+ key + " value "+value);
    if (value) {
      if (!keys[key]) {
  			keysDown[key] = true;
  		}
  		keys[key] = true;
    }
    else {
      keys[key] = false;
      if (keys[key]) {
  			 keysUp[key] = true;
  		}
    }
    //console.log(JSON.stringify(keys));
  });
});




module.exports = {
  getButtonDown,
  getButtonPressed,
  getButtonReleased,
  update: update
}

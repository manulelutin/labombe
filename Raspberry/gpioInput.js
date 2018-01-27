var mappingButton = {
  "red": 4,
  "green": 17,
  "blue": 27,
  "yellow": 22
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

var Input = (() => {

	window.addEventListener("keydown", function (e) {
		if (!keys[e.keyCode]) {
			keysDown[e.keyCode] = true;
		}
		keys[e.keyCode] = true;
	});
	window.addEventListener("keyup", function (e) {
		keys[e.keyCode] = false;
		keysUp[e.keyCode] = true;
	});
	return {
		update: function update() {
			keysDown = {};
			keysUp = {};

		},
		//
		,
	};
})();


forEach

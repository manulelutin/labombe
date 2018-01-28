'use strict';
const shuffle = module.exports.shuffle = function (arr) {
	if (!Array.isArray(arr)) {
		throw new TypeError('Expected Array, got ' + typeof arr);
	}

	var rand;
	var tmp;
	var len = arr.length;
	var ret = arr.slice();

	while (len) {
		rand = Math.floor(Math.random() * len--);
		tmp = ret[len];
		ret[len] = ret[rand];
		ret[rand] = tmp;
	}

	return ret;
};

const randomInt = module.exports.randomInt = function(min, max) {
  return Math.floor(Math.random()*(min-max)+min);
};

const randomPick =  module.exports.randomPick = function (array) {
  return array(randomInt(0, array.length));
}

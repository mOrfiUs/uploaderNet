var MyBB = (function () {
    var _myBB = {};
    _myBB.createBB = function (item) { return __encrypt(item.data, item.salt); }
    return _myBB;
})();

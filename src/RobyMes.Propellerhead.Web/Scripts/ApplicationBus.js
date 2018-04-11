this.ptt = (function (ptt) {
    var ctor = function () {
        var self = this;

        self.newCustomerAdded = new Bacon.Bus();
        self.newNoteAdded = new Bacon.Bus();
    };
    ptt.ApplicationBus = function () {
        return new ctor();
    };
    return ptt;
}(this.ptt || {}));
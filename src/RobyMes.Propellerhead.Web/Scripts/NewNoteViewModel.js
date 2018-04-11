this.ptt = (function (ptt) {
    var ctor = function (apiService, applicationBus, customerId) {
        var self = this;

        self.note = ko.observable("");

        self.canInsertNewNote = ko.computed(function () {
            return self.note() && true;
        }, self);

        self.insertNewNote = function () {
            apiService.addCustomerNote(customerId, self.note())
                .done(function (result) {
                    self.note("");
                    applicationBus.newNoteAdded.push(true);
                })
                .fail(function () {
                    applicationBus.newNoteAdded.error(new Bacon.Error("Error inserting a new Note"));
                });
        };
    };
    ptt.NewNoteViewModel = function (apiService, applicationBus, customerId) {
        return new ctor(apiService, applicationBus, customerId);
    };
    return ptt;
}(this.ptt || {}));
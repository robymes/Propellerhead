this.ptt = (function (ptt) {
    var ctor = function (apiService, applicationBus) {
        var self = this;

        self.name = ko.observable("");

        self.canInsertNewItem = ko.computed(function () {
            return self.name() && true;
        }, self);

        self.insertNewItem = function () {
            apiService.newCustomer(self.name())
                .done(function (result) {
                    self.name("");
                    applicationBus.newCustomerAdded.push(true);
                })
                .fail(function () {
                    applicationBus.newCustomerAdded.error(new Bacon.Error("Error inserting a new Customer"));
                });
        };
    };
    ptt.NewCustomerViewModel = function (apiService, applicationBus) {
        return new ctor(apiService, applicationBus);
    };
    return ptt;
}(this.ptt || {}));
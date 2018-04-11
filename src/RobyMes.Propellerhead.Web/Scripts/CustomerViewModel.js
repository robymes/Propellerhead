this.ptt = (function (ptt) {
    var ctor = function (apiService, applicationBus, customerId) {
        var self = this,
            loadItem;

        self.name = ko.observable("");
        self.errorMessage = ko.observable("");
        self.creationDate = ko.observable("");

        loadItem = function (customerId) {
            apiService.getCustomerById(customerId)
                .done(function (item) {
                    self.name(item.name);
                    self.creationDate(item.creationDate);
                })
                .fail(function (error) {
                    self.errorMessage("An error has occurred loading customers");
                });
        };

        self.init = function () {
            loadItem(customerId);
        };
    };
    ptt.CustomerViewModel = function (apiService, applicationBus, customerId) {
        return new ctor(apiService, applicationBus, customerId);
    };
    return ptt;
}(this.ptt || {}));
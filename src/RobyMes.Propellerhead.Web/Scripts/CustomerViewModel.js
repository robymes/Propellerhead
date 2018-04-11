this.ptt = (function (ptt) {
    var ctor = function (apiService, applicationBus, customerId) {
        var self = this,
            loadItem,
            itemLoaded = false;

        self.name = ko.observable("");
        self.errorMessage = ko.observable("");
        self.creationDate = ko.observable("");
        self.status = ko.observable("");
        self.statusList = ko.observableArray([
            "Prospective",
            "Current",
            "NonActive"
        ]);
        self.notes = ko.observableArray([]);

        self.status.subscribe(function (newStatus) {
            if (itemLoaded == true) {
                apiService.updateCustomerStatus(customerId, newStatus)
                    .done(function () {
                        self.errorMessage("");
                    })
                    .fail(function (error) {
                        self.errorMessage("An error has occurred updating status");
                    });
            } else {
                itemLoaded = true;
            }
        });

        loadItem = function (customerId) {
            apiService.getCustomerById(customerId)
                .done(function (item) {
                    self.errorMessage("");
                    self.name(item.name);
                    self.creationDate(item.creationDate);
                    self.status(item.status);
                    self.notes.removeAll();
                    self.notes(item.notes);
                })
                .fail(function (error) {
                    self.errorMessage("An error has occurred loading customer");
                });
        };

        applicationBus.newNoteAdded
            .onValue(function () {
                self.errorMessage("");
                itemLoaded = false;
                loadItem(customerId);
            });

        applicationBus.newNoteAdded
            .onError(function (error) {
                self.errorMessage(error.message);
            });

        self.init = function () {
            loadItem(customerId);
        };
    };
    ptt.CustomerViewModel = function (apiService, applicationBus, customerId) {
        return new ctor(apiService, applicationBus, customerId);
    };
    return ptt;
}(this.ptt || {}));
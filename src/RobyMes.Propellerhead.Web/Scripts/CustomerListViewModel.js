this.ptt = (function (ptt) {
    var ctor = function (apiService, applicationBus) {
        var self = this,
            noOrdering = "glyphicon-minus",
            ascOrdering = "glyphicon-chevron-up",
            descOrdering = "glyphicon-chevron-down",
            loadItems,
            loadItemsOrderedByName,
            loadItemsOrderedByCreationDate;

        self.items = ko.observableArray([]);
        self.errorMessage = ko.observable("");
        self.nameOrdering = ko.observable(noOrdering);
        self.creationdateOrdering = ko.observable(noOrdering);

        self.orderByName = function () {
            self.creationdateOrdering(noOrdering);
            if (self.nameOrdering() == ascOrdering) {
                self.nameOrdering(descOrdering);
                loadItemsOrderedByName(10, 0, false);
            } else {
                self.nameOrdering(ascOrdering);
                loadItemsOrderedByName(10, 0, true);
            }
        };

        self.orderByCreationDate = function () {
            self.nameOrdering(noOrdering);
            if (self.creationdateOrdering() == ascOrdering) {
                self.creationdateOrdering(descOrdering);
                loadItemsOrderedByCreationDate(10, 0, false);
            } else {
                self.creationdateOrdering(ascOrdering);
                loadItemsOrderedByCreationDate(10, 0, true);
            }
        };

        loadItems = function (pageSize, pageindex) {
            self.nameOrdering(noOrdering);
            self.creationdateOrdering(noOrdering);
            apiService.getCustomers(pageSize, pageindex)
                .done(function (items) {
                    self.items.removeAll();
                    self.items(items);
                })
                .fail(function (error) {
                    self.errorMessage("An error has occurred loading customers");
                });
        };

        loadItemsOrderedByName = function (pageSize, pageindex, ascending) {
            apiService.getCustomersOrderByName(pageSize, pageindex, ascending)
                .done(function (items) {
                    self.items.removeAll();
                    self.items(items);
                })
                .fail(function (error) {
                    self.errorMessage("An error has occurred loading customers");
                });
        };

        loadItemsOrderedByCreationDate = function (pageSize, pageindex, ascending) {
            apiService.getCustomersOrderByCreationDate(pageSize, pageindex, ascending)
                .done(function (items) {
                    self.items.removeAll();
                    self.items(items);
                })
                .fail(function (error) {
                    self.errorMessage("An error has occurred loading customers");
                });
        };

        applicationBus.newCustomerAdded
            .onValue(function () {
                loadItems(10, 0);
            });

        applicationBus.newCustomerAdded
            .onError(function (error) {
                self.errorMessage(error.message);
            });

        self.init = function () {
            self.errorMessage("");
            self.nameOrdering(noOrdering);
            self.creationdateOrdering(noOrdering);
            loadItems(10, 0);
        };
    };
    ptt.CustomerListViewModel = function (apiService, applicationBus) {
        return new ctor(apiService, applicationBus);
    };
    return ptt;
}(this.ptt || {}));
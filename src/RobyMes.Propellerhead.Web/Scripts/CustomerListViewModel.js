this.ptt = (function (ptt) {
    var ctor = function (apiService, applicationBus) {
        var self = this,
            noOrdering = "glyphicon-minus",
            ascOrdering = "glyphicon-chevron-up",
            descOrdering = "glyphicon-chevron-down",
            loadItems,
            loadItemsOrderedByName,
            loadItemsOrderedByCreationDate,
            defaultPageSize = 25;

        self.items = ko.observableArray([]);
        self.errorMessage = ko.observable("");
        self.nameOrdering = ko.observable(noOrdering);
        self.creationdateOrdering = ko.observable(noOrdering);
        self.statusFilters = ko.observableArray([
            "Prospective",
            "Current",
            "NonActive"
        ]);
        self.statusFilter = ko.observable();

        self.statusFilter.subscribe(function (filter) {
            if (self.nameOrdering() === ascOrdering) {
                loadItemsOrderedByName(defaultPageSize, 0, true);
            } else if (self.nameOrdering() === descOrdering) {
                loadItemsOrderedByName(defaultPageSize, 0, false);
            } else if (self.creationdateOrdering() === ascOrdering) {
                loadItemsOrderedByCreationDate(defaultPageSize, 0, true);
            } else if (self.creationdateOrdering() === descOrdering) {
                loadItemsOrderedByCreationDate(defaultPageSize, 0, false);
            } else {
                loadItems(defaultPageSize, 0);
            }
        });

        self.orderByName = function () {
            self.creationdateOrdering(noOrdering);
            if (self.nameOrdering() === ascOrdering) {
                self.nameOrdering(descOrdering);
                loadItemsOrderedByName(defaultPageSize, 0, false);
            } else {
                self.nameOrdering(ascOrdering);
                loadItemsOrderedByName(defaultPageSize, 0, true);
            }
        };

        self.orderByCreationDate = function () {
            self.nameOrdering(noOrdering);
            if (self.creationdateOrdering() === ascOrdering) {
                self.creationdateOrdering(descOrdering);
                loadItemsOrderedByCreationDate(defaultPageSize, 0, false);
            } else {
                self.creationdateOrdering(ascOrdering);
                loadItemsOrderedByCreationDate(defaultPageSize, 0, true);
            }
        };

        loadItems = function (pageSize, pageindex) {
            self.nameOrdering(noOrdering);
            self.creationdateOrdering(noOrdering);
            apiService.getCustomers(pageSize, pageindex, self.statusFilter())
                .done(function (items) {
                    self.errorMessage("");
                    self.items.removeAll();
                    self.items(items);
                })
                .fail(function (error) {
                    self.errorMessage("An error has occurred loading customers");
                });
        };

        loadItemsOrderedByName = function (pageSize, pageindex, ascending) {
            apiService.getCustomersOrderByName(pageSize, pageindex, ascending, self.statusFilter())
                .done(function (items) {
                    self.errorMessage("");
                    self.items.removeAll();
                    self.items(items);
                })
                .fail(function (error) {
                    self.errorMessage("An error has occurred loading customers");
                });
        };

        loadItemsOrderedByCreationDate = function (pageSize, pageindex, ascending) {
            apiService.getCustomersOrderByCreationDate(pageSize, pageindex, ascending, self.statusFilter())
                .done(function (items) {
                    self.errorMessage("");
                    self.items.removeAll();
                    self.items(items);
                })
                .fail(function (error) {
                    self.errorMessage("An error has occurred loading customers");
                });
        };

        applicationBus.newCustomerAdded
            .onValue(function () {
                self.errorMessage("");
                loadItems(defaultPageSize, 0);
            });

        applicationBus.newCustomerAdded
            .onError(function (error) {
                self.errorMessage(error.message);
            });

        self.init = function () {
            self.errorMessage("");
            self.nameOrdering(noOrdering);
            self.creationdateOrdering(noOrdering);
            loadItems(defaultPageSize, 0);
        };
    };
    ptt.CustomerListViewModel = function (apiService, applicationBus) {
        return new ctor(apiService, applicationBus);
    };
    return ptt;
}(this.ptt || {}));
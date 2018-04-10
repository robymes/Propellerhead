this.ptt = (function (ptt) {
    ptt.ajax = function (type, url, data, contentType, cors, headers) {
        var options = {
            url: url,
            contentType: contentType || "application/json",
            cache: false,
            type: type,
            crossDomain: cors,
            data: data ? JSON.stringify(data) : null
        };
        if (headers) {
            options.headers = headers;
        }
        return jQuery.ajax(options);
    };
    ptt.postJson = function (url, data, cors) {
        return ptt.ajax("POST", url, data, null, cors, null);
    };
    ptt.getJson = function (url, cors) {
        return ptt.ajax("GET", url, null, null, cors, null);
    };
    return ptt;
}(this.ptt || {}));
this.ptt = (function (ptt) {
    var ctor = function () {
        var self = this,
            route = "http://" + location.host + "/api/",
            getCustomersServiceUrl = route + "GetCustomers",
            getCustomersOrderByNameServiceUrl = route + "GetCustomersOrderByName",
            getCustomersOrderByCreationDateServiceUrl = route + "GetCustomersOrderByCreationDate",
            newCustomerServiceUrl = route + "NewCustomer";

        self.getCustomers = function (pageSize, pageIndex) {
            var request = {
                PageSize: pageSize,
                PageIndex: pageIndex,
                NameFilter: null,
                CreatioDateFilter: null,
                CustomerStatusFilter: null
            };
            return ptt.postJson(getCustomersServiceUrl, request, false);
        };

        self.getCustomersOrderByName = function (pageSize, pageIndex, ascending) {
            var request = {
                Query: {
                    PageSize: pageSize,
                    PageIndex: pageIndex,
                    NameFilter: null,
                    CreatioDateFilter: null,
                    CustomerStatusFilter: null
                },
                Ascending: ascending
            };
            return ptt.postJson(getCustomersOrderByNameServiceUrl, request, false);
        };

        self.getCustomersOrderByCreationDate = function (pageSize, pageIndex, ascending) {
            var request = {
                Query: {
                    PageSize: pageSize,
                    PageIndex: pageIndex,
                    NameFilter: null,
                    CreatioDateFilter: null,
                    CustomerStatusFilter: null
                },
                Ascending: ascending
            };
            return ptt.postJson(getCustomersOrderByCreationDateServiceUrl, request, false);
        };

        self.newCustomer = function (name) {
            var request = {
                Name: name
            };
            return ptt.postJson(newCustomerServiceUrl, request, false);
        };
    };
    ptt.ApiService = function () {
        return new ctor();
    };
    return ptt;
}(this.ptt || {}));
this.ptt = (function (ptt) {
    var ctor = function () {
        var self = this;

        self.newCustomerAdded = new Bacon.Bus();
    };
    ptt.ApplicationBus = function () {
        return new ctor();
    };
    return ptt;
}(this.ptt || {}));
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
this.ptt = (function (ptt) {
    var ctor = function () {
        var apiService = ptt.ApiService(),
            applicationBus = ptt.ApplicationBus(),
            viewModel = {
                customerList: ptt.CustomerListViewModel(apiService, applicationBus),
                newCustomer: ptt.NewCustomerViewModel(apiService, applicationBus)
            };
        ko.applyBindings(viewModel);
        viewModel.customerList.init();
    };
    ptt.IndexApp = function () {
        return new ctor();
    };
    return ptt;
}(this.ptt || {}));
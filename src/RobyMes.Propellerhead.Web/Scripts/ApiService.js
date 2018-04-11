this.ptt = (function (ptt) {
    var ctor = function () {
        var self = this,
            route = "http://" + location.host + "/api/",
            getCustomersServiceUrl = route + "GetCustomers",
            getCustomersOrderByNameServiceUrl = route + "GetCustomersOrderByName",
            getCustomersOrderByCreationDateServiceUrl = route + "GetCustomersOrderByCreationDate",
            getCustomerByIdServiceUrl = route + "GetCustomerById",
            newCustomerServiceUrl = route + "NewCustomer",
            updateCustomerStatusServiceUrl = route + "UpdateCustomerStatus",
            addCustomerNoteServiceUrl = route + "AddCustomerNote";

        self.getCustomers = function (pageSize, pageIndex) {
            var request = {
                pageSize: pageSize,
                pageIndex: pageIndex,
                nameFilter: null,
                creatioDateFilter: null,
                customerStatusFilter: null
            };
            return ptt.postJson(getCustomersServiceUrl, request, false);
        };

        self.getCustomersOrderByName = function (pageSize, pageIndex, ascending) {
            var request = {
                Query: {
                    pageSize: pageSize,
                    pageIndex: pageIndex,
                    nameFilter: null,
                    creatioDateFilter: null,
                    customerStatusFilter: null
                },
                ascending: ascending
            };
            return ptt.postJson(getCustomersOrderByNameServiceUrl, request, false);
        };

        self.getCustomersOrderByCreationDate = function (pageSize, pageIndex, ascending) {
            var request = {
                Query: {
                    pageSize: pageSize,
                    pageIndex: pageIndex,
                    nameFilter: null,
                    creatioDateFilter: null,
                    customerStatusFilter: null
                },
                ascending: ascending
            };
            return ptt.postJson(getCustomersOrderByCreationDateServiceUrl, request, false);
        };

        self.getCustomerById = function (id) {
            var request = {
                id: id
            };
            return ptt.postJson(getCustomerByIdServiceUrl, request, false);
        };

        self.newCustomer = function (name) {
            var request = {
                name: name
            };
            return ptt.postJson(newCustomerServiceUrl, request, false);
        };

        self.updateCustomerStatus = function (id, status) {
            var request = {
                id: id,
                status: status
            };
            return ptt.postJson(updateCustomerStatusServiceUrl, request, false);
        };

        self.addCustomerNote = function (id, note) {
            var request = {
                id: id,
                note: note
            };
            return ptt.postJson(addCustomerNoteServiceUrl, request, false);
        };
    };
    ptt.ApiService = function () {
        return new ctor();
    };
    return ptt;
}(this.ptt || {}));
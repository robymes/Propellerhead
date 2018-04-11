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
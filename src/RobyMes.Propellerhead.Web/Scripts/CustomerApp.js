this.ptt = (function (ptt) {
    var ctor = function (customerId) {
        var apiService = ptt.ApiService(),
            applicationBus = ptt.ApplicationBus(),
            viewModel = {
                customer: ptt.CustomerViewModel(apiService, applicationBus, customerId),
                newNote: ptt.NewNoteViewModel(apiService, applicationBus, customerId),
            };
        ko.applyBindings(viewModel);
        viewModel.customer.init();
    };
    ptt.CustomerApp = function (customerId) {
        return new ctor(customerId);
    };
    return ptt;
}(this.ptt || {}));
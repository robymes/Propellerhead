describe("Given the home page", function () {
    var apiService,
        applicationBus,
        customerListViewModel;

    beforeEach(function () {
        apiService = ptt.ApiService();
        spyOn(apiService, "getCustomers").and.returnValue([
            "Item01",
            "Item02",
        ]);
        applicationBus = ptt.ApplicationBus();
        customerListViewModel = ptt.CustomerListViewModel(apiService, applicationBus);
    });

    it("before load it doesn't cotain any element", function () {
        expect(customerListViewModel.items().length).toEqual(0);
    });

});
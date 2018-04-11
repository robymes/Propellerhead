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
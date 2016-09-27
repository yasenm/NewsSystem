APP["HttpRequester"] = {};
APP.HttpRequester = (function () {
    var JSONContentType = 'jsonp',
        HTMLContentType = 'text/html',

    makeRequest = function (url, type, data, headers) {
        return $.ajax({
            url: url,
            type: type || 'GET',
            dataType: "json",
            data: (type === 'POST') ? JSON.stringify(data) : undefined,
            contentType: 'application/json',
        })
    }

    getData = function (url) {
        return makeRequest(url);
    },

    postData = function (url, data, headers) {
        return makeRequest(url, 'POST', data, headers);
    },

    putData = function (url, headers) {
        return makeRequest(url, 'PUT', {}, headers);
    },

    makeHTMLRequest = function myfunction(url, data, method) {
        return $.ajax({
            url: url,
            data: data,
            type: method,
        })
    },

    getHTML = function (url, data) {
        return makeHTMLRequest(url, data, "GET");
    },

    postHTML = function (url, data) {
        return makeHTMLRequest(url, data, "POST");
    }

    return {
        getData: getData,
        postData: postData,
        putData: putData,
        getHTML: getHTML,
        postHTML: postHTML
    }
}())
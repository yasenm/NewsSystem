APP["HttpRequester"] = {};
APP.HttpRequester = (function () {
    var JSONContentType = 'jsonp',
        HTMLContentType = 'text/html',

    makeRequest = function (url, type, data, headers) {
        return $.ajax({
            url: url,
            type: type || 'GET',
            data: (type === 'POST') ? JSON.stringify(data) : undefined,
            contentType: JSONContentType,
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

    getHTML = function (url) {
        return $.ajax({
            url: url,
            type: 'GET',
        })
    }

    return {
        getData: getData,
        postData: postData,
        putData: putData,
        getHTML: getHTML
    }
}())
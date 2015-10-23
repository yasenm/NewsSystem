APP["BuildUrl"] = {};
APP.BuildUrl = function (connectionUrl) {
    var url = document.URL.substring(7);
    url = url.substring(0, url.indexOf('/'));
    var result = 'http://' + url + '/' + connectionUrl;
    return result;
}
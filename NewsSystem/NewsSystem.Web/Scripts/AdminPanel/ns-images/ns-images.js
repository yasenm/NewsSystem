APP['NSImages'] = {};

APP.NSImages = (function () {


    return {
        init: function (tokensUrl) {
            APP.HttpRequester.getData(tokensUrl)
                .success(function (data) {
                    $('#tokenfield').tokenfield({
                        autocomplete: {
                            source: data,
                            delay: 100
                        },
                        showAutocompleteOnFocus: true
                    })
                })
        }
    }
}());
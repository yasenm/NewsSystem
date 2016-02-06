APP['Albums'] = {};
APP.Albums = (function () {
    var albumsGridSelector = '#albums-grid',

        albumsGridUpdate = function (url, data) {
            APP.HttpRequester.getHTML(url, data)
                .success(function (success) {
                    $(albumsGridSelector).html(success);
                })
                .error(function (error) {
                    $(albumsGridSelector).html(error);
                });
        }

    return {
        init: function () {
            $('.album-category-search-link').click(function (e) {
                e.preventDefault();
                var searchUrl = $(this).attr('href');
                albumsGridUpdate(searchUrl);
            });

            $("#search-album-field").keyup(function () {
                var searchTextObj = { searchText: $(this).val() };
                var searchUrl = $(this).data("search-url");
                albumsGridUpdate(searchUrl, searchTextObj);
            });
        },

        initEditTokens: function (tokensUrl) {
            APP.HttpRequester.getData(tokensUrl)
                .success(function (data) {
                    $('#tokenfield').tokenfield({
                        autocomplete: {
                            source: data,
                            delay: 100
                        },
                        showAutocompleteOnFocus: false
                    })
                })
        }
    }
}());
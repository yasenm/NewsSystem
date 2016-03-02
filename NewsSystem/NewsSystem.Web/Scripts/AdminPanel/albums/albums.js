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

            $('#search-albums-btn').click(function () {
                var searchText = $('#search-albums-by-text-sfield').val();
                var tags = $('#search-albums-by-tags-sfield').val();
                var data = {
                    searchText: searchText,
                    tags: tags,
                };

                var searchUrl = $(this).data('urllink');
                albumsGridUpdate(searchUrl, data);
            })

            APP.HttpRequester.getData($('#search-albums-by-tags-sfield').data('tokensurl'))
            .success(function (data) {
                $('#search-albums-by-tags-sfield').tokenfield({
                    autocomplete: {
                        source: data,
                        delay: 100
                    },
                    showAutocompleteOnFocus: false
                })
            })
        },

        initEdit: function () {
            $("#ChooseImg").click(function () {
                $(".modal").modal();
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
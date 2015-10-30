APP['Albums'] = {};
APP.Albums = (function () {


    return {
        init: function () {
            $("#search-album-field").keyup(function () {
                var searchTextObj = { searchText: $(this).val()};
                var searchUrl = $(this).data("search-url");
                APP.HttpRequester.getHTML(searchUrl, searchTextObj)
                    .success(function (success) {
                        $("#albums-grid").html(success);
                    })
                    .error(function (error) {
                        $("#albums-grid").html(error);
                    });
            })
        }
    }
}());
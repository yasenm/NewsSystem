APP['NSImages'] = {};

APP.NSImages = (function () {
    var nsimagesToChooseGridSelector = '#choose-img-modal-container',
        nsimagesSelectedSelector = '.image-select-input:checked',

        clickForImagesToChoose = function () {
            $('#search-album-choose-images-grid').click(function (e) {
                e.preventDefault();
                var data = {
                    albumId: $(this).data('album'),
                    text: $('#nsimagesSearchTextfield').val(),
                    tags: $('#nsimagesSearchTokenfield').val(),
                }
                var searchUrl = $(this).attr('href');
                nsImagesToChooseGridUpdate(searchUrl, data);
            });
        },

        getSelectedImages = function () {
            var albumId = $('#hidden-edit-album-id').val();
            var selectedImagesIds = [];
            var selectedImages = $(nsimagesSelectedSelector)
                .each(function () {
                    selectedImagesIds.push($(this).data('imgid'));
                });

            var data = {
                albumId: albumId,
                imagesIds : selectedImagesIds,
            };

            return data;
        },

        clickForSelectedImages = function () {
            $('#pushImagesToAlbum').click(function (e) {
                e.preventDefault();
                var data = getSelectedImages();
                var url = $(this).attr('href');
                
                APP.HttpRequester.postData(url, data)
                    .success(function (success) {
                        $('#album-edit-nsimages-grid').html(success);
                        $('#search-album-choose-images-grid').click();
                    })
                    .error(function (error) {
                        $('#album-edit-nsimages-grid').html(error.responseText);
                        $('#search-album-choose-images-grid').click();
                    });
            })
        }

        registerEventsForImagesToChooseGrid = function () {
            clickForImagesToChoose();
        },

        nsImagesToChooseGridUpdate = function (url, data) {
            APP.HttpRequester.getHTML(url, data)
                .success(function (success) {
                    $(nsimagesToChooseGridSelector).html(success);
                    registerEventsForImagesToChooseGrid();
                })
                .error(function (error) {
                    $(nsimagesToChooseGridSelector).html(error);
                });
        }

    return {
        init: function (tokensUrl, selector) {
            APP.HttpRequester.getData(tokensUrl)
                .success(function (data) {
                    $(selector).tokenfield({
                        autocomplete: {
                            source: data,
                            delay: 100
                        },
                        showAutocompleteOnFocus: false
                    })
                })
        },

        searchImagesForAlbumInit: function () {
            registerEventsForImagesToChooseGrid();
            clickForSelectedImages();
        },

        indexInit: function () {
            $('.show-url-btn').click(function (e) {
                e.preventDefault();
                var link = $(this).attr('href');
                $('#image-link-holder').text(link);
                $('.modal').modal();
            });

            $('#search-images-by-tags-btn').click(function () {
                var tags = $('#search-images-by-tags-sfield').val(),
                    sText = $('#search-images-by-text-sfield').val();

                var data = {
                    tags: tags,
                    searchText: sText,
                };

                var url = $(this).data('urllink');
                APP.HttpRequester.getHTML(url, data)
                    .success(function (success) {
                        $("#images-grid-wrapper").html(success);
                        registerEventsForImagesToChooseGrid();
                    })
                    .error(function (error) {
                        $("#images-grid-wrapper").html(error);
                    });
            })
        }
    }
}());
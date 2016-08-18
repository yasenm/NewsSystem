APP['CommonModalTriggers'] = {};
APP.CommonModalTriggers = (function () {


    return {
        initCoverImageEvents: function () {

            $("#CoverImageChoice").click(function () {
                $("#cover-image-modal").modal();
            });

            $(".cover-image-art-select-btn").click(function () {
                var chosenImageId = $(this).data('img-id');
                $('#cover-image-input').val(chosenImageId);
                $('#cover-image-holder').attr('src', '/NSImage/NSImage?imageId=' + chosenImageId);
                $('.cover-image-modal').hide();
            })

            $('#search-cover-images-grid').click(function (e) {
                e.preventDefault();
                var data = {
                    searchText: $('#nsimagesSearchTextfield').val(),
                    searchTags: $('#nsimagesSearchTokenfield').val()
                };
                var href = $(this).attr('href');

                APP.HttpRequester.getHTML(href, data)
                    .success(function (listHtml) {
                        $('#cover-image-modal-grid').html(listHtml);
                    })
            })

        },

        initAlbumSelectEvents: function () {

            $("#AlbumChoice").click(function () {
                $("#albums-grid-modal").modal();
            });

            //$(".album-select-input").change(function () {
            //    var chosenAlbumId = $(this).val();
            //    $('#album-selected-input').val(chosenAlbumId);
            //})

            $(".album-art-select-btn").click(function () {
                var chosenImageId = $(this).data('album-id');
                $('#album-selected-input').val(chosenImageId);
                $('.albums-grid-modal').hide();
            })

            $('#search-albums-grid').click(function (e) {
                e.preventDefault();
                var data = {
                    searchText: $('#albumSearchTextfield').val(),
                    searchTags: $('#albumSearchTokenfield').val()
                };
                var href = $(this).attr('href');

                APP.HttpRequester.getHTML(href, data)
                    .success(function (listHtml) {
                        $('#album-choice-modal-grid').html(listHtml);
                    })
            })
        }
    }
}());
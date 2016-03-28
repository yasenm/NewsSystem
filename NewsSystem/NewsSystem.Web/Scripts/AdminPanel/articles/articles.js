APP['Articles'] = {};
APP.Articles = (function () {
    

    return {
        initCRUD: function () {
            $("#AlbumChoice").click(function () {
                $("#albums-grid-modal").modal();
            });

            $("#CoverImageChoice").click(function () {
                $("#cover-image-modal").modal();
            });

            $(".cover-image-radio").change(function () {
                var chosenImageId = $(this).val();
                $('#cover-image-input').val(chosenImageId);
            })
        }
    }
}());
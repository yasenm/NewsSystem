/// <reference path="../../common/http-requester.js" />

APP['Category'] = {};
APP.Category = (function () {
    var sortableCategorySlctr = ".sortable-category",
        categoryFormSlctr = '#add-cat-form',

        sortCategoryTree = function () {
            $(sortableCategorySlctr).each(function (index) {
                var $this = $(this);
                var idSlctr = "#" + $this.attr('id');
                $(idSlctr).sortable({
                    connectWith: sortableCategorySlctr
                }).disableSelection();
            });
        },

        updateCategoryTree = function () {
            $('.update-category-tree-btn').on('click', function () {
                var $btn = $(this);
                var data = [];
                $(sortableCategorySlctr).each(function (ev) {
                    var $this = $(this);
                    var catObj = { Id: $this.data('id'), Children: [] };
                    $this.find('.moveable-category').each(function () {
                        var $that = $(this);
                        catObj.Children.push($that.data('id'));
                    })
                    data.push(catObj);
                });

                APP.HttpRequester.postHTML($btn.data('href'), { 'model': data })
                    .success(function (success) {
                        toastr.success('Saved category state!');
                        $('#sortable-category-list-container').html(success);
                        init();
                    })
                    .fail(function (fail) {
                        toastr.error('Could not save!!');
                    });
            });
        },

        editCategoryName = function () {
            $('.edit-category-name-btn').on('click', function (ev) {
                ev.preventDefault();
                var $btn = $(this);
                var id = $btn.parent().data('id');
                bootbox.prompt('Change category title to:', function (result) {
                    if (result !== null) {
                        APP.HttpRequester.postHTML($btn.attr('href'), { id: id, newName: result })
                            .success(function (success) {
                                toastr.success('Changed name of category!');
                                $('#sortable-category-list-container').html(success);
                                init();
                            })
                            .fail(function (error) {
                                toastr.error('Could not new name save!!');
                            });
                    }
                });
            })
        },

        getFormData = function ($form) {
            var unindexed_array = $form.serializeArray();
            var indexed_array = {};

            $.map(unindexed_array, function (n, i) {
                indexed_array[n['name']] = n['value'];
            });

            return indexed_array;
        },

        formFunction = function () {
            $('#save-category-btn').on('click', function (ev) {
                ev.preventDefault();
                var $btn = $(this);
                tinyMCE.triggerSave();
                var formData = getFormData($(categoryFormSlctr));
                APP.HttpRequester.postHTML($btn.attr('href'), formData)
                    .success(function (success) {
                        toastr.success('Added category');
                        $('#sortable-category-list-container').html(success);
                        init();
                    })
                    .fail(function (error) {
                        toastr.error('Could not new name save!!');
                    });
            });
        }

    init = function () {
        sortCategoryTree();
        updateCategoryTree();
        editCategoryName();
        formFunction();
    }

    return {
        init: init
    }
}());
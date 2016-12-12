/// <reference path="../../common/http-requester.js" />
/// <reference path="../../toastr.js" />

APP["Comments"] = {};
APP.Comments = (function () {
    var deleteCommentSlctr = '.delete-comment-btn',
        commentsForNewsContainer = '#article-comments',

    registerDeleteEventForComments = function () {
        $(deleteCommentSlctr).on('click', function (e) {
            e.preventDefault();
            var $that = $(this);
            var postUrl = $that.attr('href');
            APP.HttpRequester.postHTML(postUrl, null)
                .success(function (success) {
                    toastr.success("Comment deleted!");
                    $(commentsForNewsContainer).html(success);
                    registerDeleteEventForComments();
                })
                .fail(function (fail) {
                    toastr.error("Was not able to remove comment!");
                });
        });
    },

    init = function () {
        registerDeleteEventForComments();
    }


    return {
        init: init
    }
})();
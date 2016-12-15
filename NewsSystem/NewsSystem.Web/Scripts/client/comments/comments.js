/// <reference path="../../common/http-requester.js" />
/// <reference path="../../toastr.js" />

APP["Comments"] = {};
APP.Comments = (function () {
    var deleteCommentSlctr = '.delete-comment-btn',
        commentsForNewsContainer = '#article-comments',
        voteCommentSlctr = '.vote-for-comment-btn',

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

    registerVoteEventForComments = function () {
        $(voteCommentSlctr).on('click', function (e) {
            e.preventDefault();
            var $that = $(this);
            var postUrl = $that.attr('href');
            APP.HttpRequester.postHTML(postUrl, null)
                .success(function (success) {
                    toastr.success("Vote registered!");
                    $(commentsForNewsContainer).html(success);
                    registerVoteEventForComments();
                })
                .fail(function (fail) {
                    toastr.error("Something went wrong! Try again in few minutes!");
                });
        });
    },

    init = function () {
        registerDeleteEventForComments();
        registerVoteEventForComments();
    }


    return {
        init: init
    }
})();
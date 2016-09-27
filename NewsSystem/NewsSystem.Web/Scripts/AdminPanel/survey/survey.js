APP['Survey'] = {};

APP.Survey = (function () {
    var answerContainerSelector = '.answers-container',
        questionCreateBtnSelector = '.create-question-btn',
        answerToAddContainerSelector = '.answer-to-add-container',
        answerAddBtnSelector = '.add-answer-btn',
        answerRemoveBtnSelector = '.remove-answer',

        initAnswersPartial = function () {
            $(answerRemoveBtnSelector).click(function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.parent().parent().html(' ').hide();
            })
        },

        attachRemoveAnswerEvent = function (callbackEventHandler) {
            $('.remove-answer-btn').on('click', function (e) {
                e.preventDefault();
                var $this = $(this);
                var link = $this.attr("href");
                var questionId = $this.data("questionid");
                var answerId = $this.data("answerid");

                bootbox.confirm({
                    message: "Are you sure you want to remove this answer?",
                    buttons: {
                        confirm: {
                            label: 'Yes',
                            className: 'btn-success'
                        },
                        cancel: {
                            label: 'No',
                            className: 'btn-danger'
                        }
                    },
                    callback: function (result) {
                        if (result) {
                            var data = {
                                answerId: answerId,
                                questionId: questionId
                            };

                            APP.HttpRequester.postHTML(link, data)
                                .success(function (success) {
                                    $('#answers-list').html(success);
                                    callbackEventHandler(callbackEventHandler);
                                    toastr.success("Answer removed!");
                                }).error(function (error) {
                                    toastr.error("Something went wrong!");
                                })
                        }
                    }
                });
            })
        }

    return {
        init: function () {
            $('#add-answer-popup').on('click', function (e) {
                e.preventDefault();
                var $this = $(this);
                var link = $this.attr("href");
                var questionId = $this.data("questionid");
                console.log(link);

                bootbox.prompt({
                    title: "Add answer!",
                    inputType: 'text',
                    callback: function (result) {
                        var data = {
                            questionId: questionId,
                            aDescription: result
                        };
                        console.log(data);
                        APP.HttpRequester.postHTML(link, data)
                            .success(function (success) {
                                $('#answers-list').html(success);
                                attachRemoveAnswerEvent(attachRemoveAnswerEvent);
                                toastr.success("Answer added!");
                            }).error(function (error) {
                                toastr.error("Something went wrong!");
                            })

                        console.log(result);
                    }
                });
            });

            attachRemoveAnswerEvent(attachRemoveAnswerEvent);
            //$('.remove-answer-btn').on('click', function (e) {
            //    e.preventDefault();
            //    var $this = $(this);
            //    var link = $this.attr("href");
            //    var questionId = $this.data("questionid");
            //    var answerId = $this.data("answerid");

            //    bootbox.confirm({
            //        message: "Are you sure you want to remove this answer?",
            //        buttons: {
            //            confirm: {
            //                label: 'Yes',
            //                className: 'btn-success'
            //            },
            //            cancel: {
            //                label: 'No',
            //                className: 'btn-danger'
            //            }
            //        },
            //        callback: function (result) {
            //            if (result) {
            //                var data = {
            //                    answerId: answerId,
            //                    questionId: questionId
            //                };

            //                APP.HttpRequester.postHTML(link, data)
            //                    .success(function (success) {
            //                        $('#answers-list').html(success);
            //                        attachRemoveAnswerEvent();
            //                        toastr.success("Answer removed!");
            //                    }).error(function (error) {
            //                        toastr.error("Something went wrong!");
            //                    })
            //            }
            //        }
            //    });
            //})
        }
    }

}());
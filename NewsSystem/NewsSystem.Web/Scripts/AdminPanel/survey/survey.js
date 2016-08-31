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
        };

    return {
        init: function () {
            $(answerAddBtnSelector).click(function (e) {
                e.preventDefault();
                var nextAnswerFormUrl = $(this).attr('href');
                APP.HttpRequester.getHTML(nextAnswerFormUrl)
                    .success(function (success) {
                        $(answerContainerSelector).html($(answerContainerSelector).html() + success);
                        initAnswersPartial();
                    })
                    .error(function (error) {
                        alert('Adding answer form went wrong!');
                    });
            });

            $(questionCreateBtnSelector).click(function () {
                var answers = [];
                $('.answer-description').each(function () {
                    answers.push({ Description: $(this).val() });
                })
                console.log(answers);
            });

            initAnswersPartial();
        }
    }

}());
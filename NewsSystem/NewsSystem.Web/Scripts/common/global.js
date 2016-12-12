APP["Global"] = {};
APP.Global = (function () {
    var weatherUrl = '',

    getLang = function () {
        if (navigator.languages !== undefined)
            return navigator.languages[0];
        else
            return navigator.language;
    },

    showCurrentTime = function () {
        var dt = new Date();
        var dtContainerSlctr = '#datetime';
        var formatedHour = moment(dt).locale(getLang()).format('HH:mm');
        var formatedWeekday = moment(dt).locale('bg').format('dddd');
        var formatedDate = moment(dt).locale('bg').format('D MMMM YYYY');

        $(dtContainerSlctr + ' .time').html(formatedHour);
        $(dtContainerSlctr + ' .weekday').html(formatedWeekday);
        $(dtContainerSlctr + ' .date').html(formatedDate);
    },

    startCurrentTimeCycle = function () {
        showCurrentTime();
        setInterval(showCurrentTime, 1000);
    },

    getWeather = function () {
        APP.HttpRequester.getHTML(weatherUrl)
             .success(function (success) {
                 $('#weather-widget-li').html(success);
             })
             .error(function (error) {
                 getWeather();
             });
    },

    startWeatherCycle = function (url) {
        weatherUrl = url;
        getWeather();
        setInterval(getWeather, 300000);
    }

    return {
        getLang: getLang,
        initTime: startCurrentTimeCycle,
        initWeather: startWeatherCycle
    }
}());
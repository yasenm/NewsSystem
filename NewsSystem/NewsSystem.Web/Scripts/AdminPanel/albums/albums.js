APP['Albums'] = {};
APP.Albums = (function () {


    return {
        init: function () {
            $("#search-album-field").keyup(function () {
                alert(this.textContent);
            })
        }
    }
}());
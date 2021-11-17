jQuery.extend({
    getStatusId: function (statusName) {
        var id = -1;
        $.ajax({
            type: 'get',
            url: 'https://localhost:5001/api/Anime/GetStatusId/' + statusName,
            dataType: 'json',
            async: false,
            success: function (data) {
                id = data;
            },
            error: function (errorThrown) {
                console.log(errorThrown);
            }
        });
        return id;
    },
    getAgeRestrictionId: function (ARCode) {
        var id = -1;
        $.ajax({
            type: 'get',
            url: 'https://localhost:5001/api/Anime/GetAgeRestrictionId/' + ARCode,
            dataType: 'json',
            async: false,
            success: function (data) {
                id = data;
            },
            error: function (errorThrown) {
                console.log(errorThrown);
            }
        });
        return id;
    },
    getAllGenres: function () {
        var genres = Array();
        $.ajax({
            type: 'get',
            url: 'https://localhost:5001/api/Anime/GetAllGenres',
            dataType: 'json',
            async: false,
            success: function (data) {
                genres = data;
            },
            error: function (errorThrown) {
                console.log(errorThrown);
            }
        });
        return genres;
    }
})
    
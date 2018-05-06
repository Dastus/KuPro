$(function () {
    $("[data-autocomplete-source]").each(function () {
        var target = $(this);
        target.autocomplete({ minLength: 3,
        source: function (request, response) {
        $.ajax({
            url: '@Url.Action("AutoCompleteTownSearch", "Home")',
            dataType: "json",
            data: {
                town : request.term,
                region : document.getElementById('#region').value,
            },
            success: function(data) {
                response(data);
            }
            });
        }

        });
    });
});
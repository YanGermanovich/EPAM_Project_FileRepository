function InitializeCurrentForm() {
    $("#search_form input[type='text']").on("keyup", function () {
        $("#search_form").submit();
    })
    $("#search_form input[type='number']").on("change", function () {
        $("#search_form").submit();
    })
    $("#search_form select").on("change", function () {
        $("#search_form").submit();
    })

    $("#search_form").on("submit", function(e) {
        e.preventDefault();
        $.post("//" + document.location.host + "/Home/SearchForm", $(this).serialize(), function (data) {
            $("#dataTableContainer").html(data);
        });

    });
}
    
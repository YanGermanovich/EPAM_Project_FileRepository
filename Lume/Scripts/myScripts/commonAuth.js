$(function () {
    $("#homePage").on("click", function (e) {
        e.preventDefault();
        RefreshForm("//" + document.location.host + "/Home/Index");
    });
    $("#searchPage").on("click", function (e) {
        e.preventDefault();
        RefreshForm("//" + document.location.host + "/Home/Search");
    })
    
    InitializeForm();
});

function SetHandlerRating() {
    $('#resourceViewRating').on('rating.change', function (event, value, caption) {
        var iNum = value.replace(/\./g, ',');
        $.post("//" + document.location.host + "/Home/Rating", "mark=" + iNum + " &id_resource=" + $("#currentResourceID").val(), function (data) {
            $("#ratingResourceView").html("<input id='resourceViewRating' class='rating' value='" + data + "' data-step='0.1' data-size='xs' data-show-clear='false'>")
            $("#resourceViewRating").attr("data-readonly", true);
            InitializeRating("#resourceViewRating");
            SetHandlerRating()
        })
    });
}

function InitializeRating(selector)
{
    $(selector).rating();
    
}

function RefreshForm(page) {
    page = page || document.location.href;
    $("html").attr("class", "")
    $.get(page, function (data) {
        $("#main_page").html(data);
        InitializeRating('#table_body .rating');
        $.get("//" + document.location.host + "/Home/GetMostPopular", function (data) {
            $("#most_popular_container").html(data);
            InitializeForm();
        });
        

    })
}
function InitializeForm() {
    $(".view_button").on("click", function () {
        $.post("//" + document.location.host + "/Home/ResourceView", "id_resource=" + $(this).val(), function (data) {
            $("#resourceViewName").html(data[0]["Name"]);
            $("#resourceViewDescription").html(data[0]["Description"]);
            $("#resourceViewRating").val();
            $("#ratingResourceView").html("<input id='resourceViewRating' class='rating' value='" + data[0]["Rating"] + "' data-step='0.1' data-size='xs' data-show-clear='false'>")
            $("#resourceViewViews").html("Views: " + data[0]["Views"]);
            $("#currentResourceID").val(data[0]["Id"]);
            $("#downloadLink").attr("href", "//" + document.location.host + "/Home/Download?id_resource=" + data[0]["Id"]);
            $("#resourceViewRating").attr("data-readonly", data[1]);
            $('#resourceViewRating').rating();
            SetHandlerRating()
        });
    });
    $('#closeResourceView').on("click", function () {
        RefreshForm();
    })
    $(".delResource").on("click", function () {
        if (confirm("Are you sure you want delete this resource?")) {
            $.get("//" + document.location.host + "/Home/Remove", "id_resource=" + $(this).attr("value"), function () {
                RefreshForm();
            });
        }
    });
    $(".changePage").on("click", function (e) {
        e.preventDefault();
        RefreshForm($(this).attr("href"))
    })
    $(".rating_number_noJS").addClass("rating_number");
    InitializeCurrentForm();
}
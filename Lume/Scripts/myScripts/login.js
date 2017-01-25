$(function () {
    $.get("//" + document.location.host + "/Account/GetLogInForm", function (data) {
        $("#logInFormJS").html(data);
    });
    
    $("#loginDropdown").on("submit", function (e) {
        LogInAjaxOnBegin()
        e.preventDefault();
        $.post("//" + document.location.host + "/Account/Login", $(this).serialize(), function (data) {
            $("#logInForm").html(data);
            LogInAjaxOnSucces()
        });
    });
})
function LogInAjaxOnBegin() {
    $("#logInForm").addClass("loading_hide");
    $("#loading_icon").addClass("loading_show");
}
function LogInAjaxOnSucces() {
    $("#logInForm").removeClass("loading_hide");
    $("#loading_icon").removeClass("loading_show");
}
$(document).on('ready', function () {
    

});

function InitializeCurrentForm()
{
    $("#file_upload_input").fileinput({
        allowedFileExtensions: ["mp4", "avi", "mkv", "mp3", "txt", "doc", "docx"],
        elErrorContainer: "#errorBlock"
    });

    $("#uploadForm").on("submit", function (e) {
        e.preventDefault();
        var formData = new FormData();
        formData.append("file", $(e.target["UploadFile"])[0].files[0]);
        formData.append("Name", $(e.target["Name"]).val())
        formData.append("Description", $(e.target["Description"]).val());
        $.ajax({
            type: "POST",
            url: "//" + document.location.host + "/Home/Upload",
            contentType: false,
            processData: false,
            data: formData,
            success: function (data) {
                if (data == null)
                    $("#myModalUpload .close").click();
                else
                    $("#uploadFormContainer").html(data);
            }
        })
    });

    $("#uploadLinkBtn").attr("data-toggle", "modal")
    $("#uploadLinkBtn").attr("data-target", "#myModalUpload")
    $("#uploadLinkBtn").attr("href", "");
}
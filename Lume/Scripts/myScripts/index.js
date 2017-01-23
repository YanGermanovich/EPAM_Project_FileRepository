$(document).on('ready', function () {
    $("#file_upload_input").fileinput({
        allowedFileExtensions: ["mp4", "avi", "mkv", "mp3", "txt", "doc", "docx"],
        elErrorContainer: "#errorBlock"
    });
    $('#resource_table').DataTable();

});

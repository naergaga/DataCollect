$(function () {
    console.log("hadle-action");
    function sendPost(url, formData) {

    }

    $(".btnDeleteRow").click(function () {
        var deleteUrl = $(".btnDeleteRow").data("link");
        console.log(deleteUrl);
        $.post(deleteUrl, function (result) {
            if (result.success) {
                window.location.reload();
            }
            else {
                alert(result.message);
            }
        }, "json");
    });
});
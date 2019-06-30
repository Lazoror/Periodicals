$(document).ready(function () {
    $(".selectpicker-magazine").selectpicker({
        "title": "Select magazine"
    }).selectpicker("render");

    $(".selectpicker").selectpicker({
        "title": "Select month"
    }).selectpicker("render");

    $(".selectpicker-topic").selectpicker({
        "title": "Select topic"
    }).selectpicker("render");

    $("#customMagazine").hide();

    $(".custom-magazine-appear").on("click", function () {

        if ($(".magazine-select").is(":visible")) {
            $(".magazine-select").hide();
            $("#customMagazine").show();
        } else {
            $(".magazine-select").show();
            $("#customMagazine").hide();
        }
    });
});
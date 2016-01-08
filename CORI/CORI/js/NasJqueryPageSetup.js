
$(function () {
    $("#sortable1, #sortable2,#sortable3,#canvas").sortable({
        connectWith: ".connectedSortable",
        remove: function (event, ui) {

            ui.item.clone().appendTo('#canvas');
            $(this).sortable('cancel');
        }
    }).disableSelection();
});
$(function () {
    var spinner = $("#spinner").spinner();

    $("#disable").click(function () {
        if (spinner.spinner("option", "disabled")) {
            spinner.spinner("enable");
        } else {
            spinner.spinner("disable");
        }
    });
    $("#destroy").click(function () {
        if (spinner.spinner("instance")) {
            spinner.spinner("destroy");
        } else {
            spinner.spinner();
        }
    });
    $("#getvalue").click(function () {
        alert(spinner.spinner("value"));
    });
    $("#setvalue").click(function () {
        spinner.spinner("value", 5);
    });

    $("button").button();
});
$('#sendHtml').click(function () {

    var htmlTest = $("#canvas").html();
    var pageName = $('#pageName').val();
    //var esc = escape(htmlTest);
    //alert(typeof(htmlTest));
    var data = htmlTest ;
    alert("Starting post");
    $.ajax({
        url: window.Position,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ ime : pageName ,html: htmlTest }),
        success: function (result) {
            alert("Server said: " + result);
        }
    });

});

$('#clearCanvas').click(function () {

    var canvas = $("#canvas").empty();

});
$('.items').click(function ()
{
    selected_item = $(".items");

});
$('#clearLastAdded').click(function () {

    var canvas = $("#canvas");
    var index = canvas.index(selected_item);

    canvas.removeChild(canvas.childNodes[index]);

});
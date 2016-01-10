
$(function () {
    $("#sortable1, #sortable2,#sortable3, #sortable4, #canvas").sortable({
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
$(document).ready(function()
{
    $('#othersHtml').click(function () {
        $.ajax({
            url: "http://localhost:3359/home/OtherPeopleHtml",
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ otherPeople: "Give Me the People to Save My Soul" }),
            success: function (result) {
                //alert("Server said: " + result); // ovo ne treba, sluzi samo kao test
                $("#outputTest2").text(result);

            }
        });
    });
});
$(document).ready(function () {
    $('#myHtml').click(function () {
        //  var userName = "omega";
        alert("Starting post YourHtml"); // ovo ne treba, sluzi samo kao test
        $.ajax({
            url: "http://localhost:3359/home/MyHtml",
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ userName: "--- STAVI IME USERA ---" }),
            success: function (result) {
                //alert("Server said: " + result); // ovo ne treba, sluzi samo kao test
               // $("#outputTest").text(result);
                var objectResult = JSON.parse(result);
                var len = objectResult.pageList.length;
                for (i = 0; i < len; i++)
                {
                    $("#sortable4").append(objectResult.pageList[i].html);
                }
            }
        });
    });
});
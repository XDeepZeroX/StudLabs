let historyIsOpen = false;
var modal = document.getElementById("modal");
var btn = document.getElementById("hisotry-btn");

window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
        historyIsOpen = false;
    }
}

$("#hisotry-btn").click(() => {
    historyIsOpen = !historyIsOpen;
    $('#modal').slideToggle(100);

    if (historyIsOpen) {
        var target = $('#body-history')[0];
        $(target).html("");
        var spinner = new Spinner(defaults).spin(target);
        $.ajax({
            url: `/${Controller}/GetMainHistory`,
            type: 'GET',
            complete: function (result) {
                console.log(result);
                $('#body-history').html(result.responseText);
                //let modalBody = $(".modal-body")[0];
                //$(modalBody).html(result.responseText);
                //openModal();
            }
        });
    }
})


function selectTable(id) {
    $('#modal').slideUp(0);
    historyIsOpen = false;
    var target = $('#content-task')[0];
    var spinner = new Spinner(defaults).spin(target);
    $.ajax({
        url: `/${Controller}/GetTableFromDb/${id}`,
        type: 'GET',
        complete: function (result) {
            $('#content-task').html(result.responseText);
            //let modalBody = $(".modal-body")[0];
            //$(modalBody).html(result.responseText);
            //openModal(); 
            $("select").select2();
        }
    });
}




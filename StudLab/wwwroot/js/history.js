let historyIsOpen = false;
$("#hisotry-btn").click(() => {
    historyIsOpen = !historyIsOpen;
    $('#history-user').slideToggle(100);

    if (historyIsOpen) {
        var target = $('#body-history')[0];
        $(target).html("");
        var spinner = new Spinner(defaults).spin(target);
        $.ajax({
            url: `/Transport/GetHistory`,
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
    $('#history-user').slideUp(0);
    historyIsOpen = false;
    var target = $('#content-task')[0];
    var spinner = new Spinner(defaults).spin(target);
    $.ajax({
        url: `/Transport/GetTableFromDb/${id}`,
        type: 'GET',
        complete: function (result) {
            $('#content-task').html(result.responseText);
            //let modalBody = $(".modal-body")[0];
            //$(modalBody).html(result.responseText);
            //openModal();
        }
    });
}
let historyIsOpen = false;
var modal = document.getElementById("modal");
var lastControllerName = "";

window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
        historyIsOpen = false;
    }
}

$(".units a").click(function () {
    historyIsOpen = !historyIsOpen;
    $('#modal').slideToggle(100);
    var nameController = this.dataset.controller;
    lastControllerName = nameController;
    if (historyIsOpen) {
        var target = $('#body-history')[0];
        $(target).html("");
        var spinner = new Spinner(defaults).spin(target);
        $.ajax({
            url: `/${nameController}/GetAllHistory`,
            type: 'GET',
            complete: function (result) {
                console.log(result);
                $('#modal').html(result.responseText);
            }
        });
    }
});


function selectTable(id) {
    //$('#modal').slideUp(0);
    //historyIsOpen = false;
    //var target = $('#content-task')[0];
    //var spinner = new Spinner(defaults).spin(target);
    //$.ajax({
    //    url: `/${Controller}/GetTableFromDb/${id}`,
    //    type: 'GET',
    //    complete: function (result) {
    //        $('#content-task').html(result.responseText);
    //        //let modalBody = $(".modal-body")[0];
    //        //$(modalBody).html(result.responseText);
    //        //openModal(); 
    //        $("select").select2();
    //    }
    //});

    window.location.href = `/${lastControllerName}/GetTableFromDb?id=${id}&page=page`;
}




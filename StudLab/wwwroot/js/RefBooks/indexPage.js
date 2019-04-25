
// <!-- Modal Dialog -->
var spanClose = document.getElementsByClassName("close-modal")[0];
spanClose.onclick = closeModal;
window.onclick = function (event) {
    let modal = $('#modal-dialog')[0];
    if (event.target === modal) {
        modal.style.display = "none";
    }
}    
var btn = $(".btn-create")[0]; //Кнопка создать у справочников !!!!!!
btn.addEventListener("click", Add);
function closeModal(){
    $('#modal-dialog')[0].style.display = "none";
}
function Add() {
    // $(".modal-body").html("");
    if(!modalIsCreate()){
        $.ajax({
            url: `/${Controller}/Create`,
            type: 'GET',
            complete: function (result) {
                let modalBody = $(".modal-body")[0];
                $(modalBody).html(result.responseText);
                openModal();
            }
        });
    }
    else{
        openModal();
    }
}
function openModal(){
    $('#modal-dialog')[0].style.display = "block";
}
function modalIsCreate(){
    return ($(".modal-body .create-entity").length === 1) ? true : false;
}
function refreshPage(){
    $.ajax({
        url: `/${Controller}/ListEntities`,
        type: 'GET',
        complete: function (result) {
            let bodyContent = $(".body-content .records-table")[0];
            //$(bodyContent).html(result.responseText);
            //initTable();
        }
    });
}

function Complete(jqXHR,  textStatus) {
    console.log(jqXHR)
    console.log(textStatus)
    $("body").append(jqXHR.responseText);
    closeModal(); 
    refreshPage(); //Обновляем таблицу
}

function Del(e, id){    
    e.preventDefault();
    $.ajax({
        url: `/${Controller}/Delete/${id}`,
        method: 'DELETE',
        complete: Complete
    });
}

function Edit(e, id){
    e.preventDefault();
    $.ajax({
        url: `/${Controller}/Edit/${id}`,
        type: 'GET',
        complete: function (result) {
            let modalBody = $(".modal-body")[0];
            $(modalBody).html(result.responseText);
            openModal();
        }
    });
}
function Details(e, id){
    e.preventDefault();    
    $.ajax({
        url: `/${Controller}/Details/${id}`,
        type: 'GET',
        complete: function (result) {
            let modalBody = $(".modal-body")[0];
            $(modalBody).html(result.responseText);
            openModal();
        }
    });
}
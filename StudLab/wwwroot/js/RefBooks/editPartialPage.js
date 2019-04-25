
// отправка формы
$("form.edit-entity").submit(function (e) {
    e.preventDefault();
    var body = $(this).serialize();
    EditPut(body)
});

function EditPut(body) {
    $.ajax({
        url: "/Techniques/Edit",
        //contentType: "application/json",
        method: "PUT",
        data: body,
        complete: Complete
    })
}



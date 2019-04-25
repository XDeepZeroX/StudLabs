
// отправка формы
$("form.create-entity").submit(function (e) {
    e.preventDefault();
    var body = $(this).serialize();
    Create(body)
});

function Create(body) {
    $.ajax({
        url: "/Techniques/Create",
        //contentType: "application/json",
        method: "POST",
        data: body,
        complete: Complete
    })
}



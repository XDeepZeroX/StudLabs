// отправка формы
function FormPreventDefault() {
    $("form").submit(function (e) {
        e.preventDefault();
    });
}
FormPreventDefault();

function step1(form) { 

    var body = $(form).serialize();
    //step1(body)
    $.ajax({
        url: "/Transport/GetTable",
        //contentType: "application/json",
        method: "POST",
        data: body,
        complete: (jqXHR,  textStatus)=>{
            console.log(jqXHR)
            console.log(textStatus)
            if (jqXHR.responseText !== "")
                $("#content-task").html(jqXHR.responseText);       
            $("select").select2();
            FormPreventDefault();
        }
    })
}

function step2(form) {
    var body = $(form).serialize(); 
    $.ajax({
        url: "/Transport/Decision",
        //contentType: "application/json",
        method: "POST",
        data: body,
        complete: (jqXHR,  textStatus)=>{
            console.log(jqXHR)
            console.log(textStatus)
            if (jqXHR.responseText !== "")
                $("#content-task").html(jqXHR.responseText);       
            $("select").select2();
            FormPreventDefault();
        }
    })
}

function step2back() {
    $.ajax({
        url: "/Transport/GenerateTable",
        //contentType: "application/json",
        method: "GET",
        complete: (jqXHR, textStatus) => {
            console.log(jqXHR)
            console.log(textStatus)
            if (jqXHR.responseText !== "")
                $("#content-task").html(jqXHR.responseText);
            $("select").select2();
        }
    })

}

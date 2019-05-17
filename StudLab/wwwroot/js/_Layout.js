$(document).ready(function () {
    $("select").select2();

    $('#menu > li.sub > a').click(function () {
        $('#menu li ul').slideUp();
        if ($(this).next().is(":visible")) {
            $(this).next().slideUp();
        } else {
            $(this).next().slideToggle(300);
        }
        $('#menu li a').removeClass('active');
        $(this).addClass('active');
    });
    $('.sub ul li a').click(function () {
        $('.sub ul li a').removeClass('active');
        $(this).addClass('active');
    });
    // MENU HOVER
    $('.hover-menu').hover(()=>{
        $('.hover-menu').animate({'width': '220px'},300);
    }, ()=>{
        if(!$('#nav-toggle').prop("checked")){
            $('#menu li ul').slideUp(100);
            $('.hover-menu').animate({'width': '70px'},100);
        }
    })

    //Sorted button onClick
    $("th .fas").click(function () {
        if (this.classList.contains("fa-exchange-alt")) { //None -> UP
            this.classList.remove("fa-exchange-alt");
            this.classList.add("fa-sort-amount-up");
        } else if (this.classList.contains("fa-sort-amount-up")) { //UP -> Down
            this.classList.remove("fa-sort-amount-up");
            this.classList.add("fa-sort-amount-down");
        } else if (this.classList.contains("fa-sort-amount-down")) {   //Down -> UP         
            this.classList.add("fa-sort-amount-up");
            this.classList.remove("fa-sort-amount-down");
        }
    })
// MENU
    /* Открытие меню */
    var main = function() { //главная функция
        $('.nav-toggle').click(function() { //выбираем класс icon-menu и добавляем метод click с функцией, вызываемой при клике
            let isOpen = $('#nav-toggle').prop("checked");
            console.log(isOpen);
            $('.navigation').animate({ //выбираем класс menu и метод animate
                width: ((isOpen) ? '70px' : '220px')  //теперь при клике по иконке, меню, скрытое за левой границей на 285px, изменит свое положение на 0px и станет видимым
            }, 300).toggleClass('hover-menu'); //скорость движения меню в мс
            
            $('body').animate({ //выбираем тег body и метод animate
                padding: ((isOpen) ? '0 0 0 70px' : '0 0 0 220px'),
                // padding_left: '220px', //чтобы всё содержимое также сдвигалось вправо при открытии меню, установим ему положение 285px
                // width: $("html").width()-220
            }, 300); //скорость движения меню в мс
            $('#menu li ul').slideUp(250);
            // $('.table').css({"width": $('.table').parent().width()});
            $('.table').css({"width":"100%"});            
        });
    };
    $(document).ready(main); //как только страница полностью загрузится, будет вызвана функция main, отвечающая за работу меню
});
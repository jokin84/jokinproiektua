// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*Saskitik ezabatzeko*/
$(document).ready(function () {
    $(".RemoveLink").click(function (e) {
        e.preventDefault();
        // Get the id from the link
        var recordToDelete = $(this).attr("data-id");
        if (recordToDelete != '') {
            // Perform the ajax post
            $.post("/Saskia/SaskiaKendu", { "id": recordToDelete },
                function (data) {
                    // Successful requests get here
                    // Update the page elements
                    if (data.itemKopurua === 0) {
                        $('#row-' + data.ezabatutakoId).fadeOut('slow');
                    } else {
                        $('#item-count-' + data.ezabatutakoId).text(data.itemKopurua);
                    }
                    $('#cart-total').text(data.saskiaGuztira);
                    $('#update-message').text(data.mezua);

                    //Aurrerago gehituko dugun lerroa
                    //$('#cart-status').text(data.itemGuztiak);
                });
        }
    });
})

/*Saskira gehitzeko*/
$(document).ready(function () {
    $(".AddLink").click(function (e) {
        e.preventDefault();
        // Get the id from the link
        var recordToAdd = $(this).attr("data-id");
        if (recordToAdd !== '') {
            // Perform the ajax post
            $.post("/Saskia/SaskiaGehituJson", { "id": recordToAdd },
                function (data) {
                    // Successful requests get here
                    // Update the page elements
                    $('#item-count-' + data.ezabatutakoId).text(data.itemKopurua);
                    $('#cart-total').text(data.saskiaGuztira);
                    $('#update-message').text(data.mezua);
                    $('#cart-status').text('Cart (' + data.itemGuztiak + ')');
                });
        }
    });
})



$(document).ready(function () {
    //variables
    var pass1 = $('[name=Pasword1]');
    var pass2 = $('[name=Pasword2]');
    var confirmacion = "Las contraseñas si coinciden";
    var longitud = "La contraseña debe estar formada entre 6-10 carácteres (ambos inclusive)";
    var negacion = "No coinciden las contraseñas";
    var vacio = "La contraseña no puede estar vacía";
    //oculto por defecto el elemento span
    var span = $('<span></span>').insertAfter(pass2);
    span.hide();
    //función que comprueba las dos contraseñas
    function coincidePassword() {
        var valor1 = pass1.val();
        var valor2 = pass2.val();
        //muestro el span
        span.show().removeClass();
        //condiciones dentro de la función
        if (valor1 != valor2) {
            span.text(negacion).addClass('negacion');
        }
        if (valor1.length == 0 || valor1 == "") {
            span.text(vacio).addClass('negacion');
        }
        if (valor1.length < 6 || valor1.length > 10) {
            span.text(longitud).addClass('negacion');
        }
        if (valor1.length != 0 && valor1 == valor2) {
            span.text(confirmacion).removeClass("negacion").addClass('confirmacion');
        }
    }
    //ejecuto la función al soltar la tecla
    pass2.keyup(function () {
        coincidePassword();
    });
});



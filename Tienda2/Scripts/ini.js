$(document).ready(function () {
    $('.validar').each(function () {
        $(this).validate({
            errorElement: 'div',
            errorPlacement: function (error, element) {
                var placement = $(element).data('error');
                if (placement) {
                    $(placement).append(error);
                } else {
                    error.insertAfter(element);
                }
            }
        });
    });

    $('input').blur(function () {
        this.value = this.value.toUpperCase();
    });

    $("body").on('click', 'button', function () {

        // Si el boton no tiene el atributo ajax no hacemos nada
        if ($(this).data('ajax') === undefined) return;

        // El metodo .data identifica la entrada y la castea al valor más correcto
        if ($(this).data('ajax') !== true) return;

        var form = $(this).closest("form");
        var buttons = $("button", form);
        var button = $(this);
        var url = form.attr('action');

        if (button.data('confirm') !== undefined) {
            if (button.data('confirm') === '') {
                if (!confirm('¿Esta seguro de realizar esta acción?')) return false;
            } else {
                if (!confirm(button.data('confirm'))) return false;
            }
        }

        if (button.data('delete') !== undefined) {
            if (button.data('delete') === true) {
                url = button.data('url');
            }
        } else if (!form.valid()) {
            return false;
        }

        // Creamos un div que bloqueara todo el formulario
        var block = $('<div class="block-loading" />');
        form.prepend(block);

        // En caso de que haya habido un mensaje de alerta
        $("#card-alert", form).remove();

        // Para los formularios que tengan CKupdate
        //if (form.hasClass('CKupdate')) CKupdate();

        form.ajaxSubmit({
            dataType: 'JSON',
            type: 'POST',
            url: url,
            success: function (r) {
                block.remove();
                if (r.response) {
                    if (!button.data('reset') !== undefined) {
                        if (button.data('reset')) form.reset();
                    }
                    else {
                        form.find('input:file').val('');
                    }
                }

                // Mostrar mensaje
                if (r.message !== null) {
                    if (r.message.length > 0) {
                        var css = "";
                        if (r.response) css = "green";
                        else css = "red";

                        var message = '<div id="card-alert" class="card ' + css + ' "><div class="card-content white-text"><p><i class="mdi-alert-error"></i>' + r.message + '</p></div><button type="button" class="close white-text" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button></div>';
                        form.prepend(message);
                        setTimeout('$("#card-alert .close").click(function () { $(this).closest("#card-alert").fadeOut("slow") });', 0);
                    }
                }

                // Ejecutar funciones
                if (r.function != null) {
                    setTimeout(r.function, 0);
                }
                // Redireccionar
                if (r.href != null) {
                    if (r.href === 'self') window.location.reload(true);
                    else window.location.href = r.href;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                block.remove();
                form.prepend('<div class="alert alert-warning alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + errorThrown + ' | <b>' + textStatus + '</b></div>');
            }
        });
        return false;
    });
});

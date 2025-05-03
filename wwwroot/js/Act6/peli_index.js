$(document).ready(function () {
    // Capturamos los radio buttons de selección
    $('input[name="selectedUser"]').change(function () {
        var idUsuario = $(this).val(); // Obtenemos el ID del usuario seleccionado

        var urlDetalles = window.AppConfig.detailPageUrl; // Reutilizo la url almacenada en su respectiva vista

        $('#btnDetalle').attr('href', urlDetalles + '/' + idUsuario);
        //$('#btnEditar').attr('href', urlModificar + '/' + idUsuario);
        //$('#btnEliminar').attr('href', urlEliminar + '/' + idUsuario);

        // Mostrar el div con los enlaces de acción
        // Por alguna razon el checked lo esta tomando de forma inversa (sin el !)
        if (!($(this).prop('checked'))) {
            $('#conteAction').css('display', 'none');
            $('#actionActivo').prop('checked', false);
        }
        else {
            $('#conteAction').css('display', 'flex');
            $('#actionActivo').prop('checked', true);
        }
    });

    // Hace que la fila entera sea clickeable para seleccionar el radio button
    $('.filapeli').click(function() {
        $('.filapeli').css('background-color', '') // Quita el color del fondo de las demas filas
        if (!($(this).find('input[name="selectedUser"]').prop('checked'))){
            $(this).find('input[name="selectedUser"]').prop('checked', true).trigger('change');
            $(this).css('background-color', 'rgba(0,0,0,0.4)');
        }
        else{
            $(this).find('input[name="selectedUser"]').prop('checked', false).trigger('change');
        }
    });
});
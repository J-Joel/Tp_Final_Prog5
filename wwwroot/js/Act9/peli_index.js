$(document).ready(function () {
    // Capturamos los radio buttons de selección
    $('input[name="selectedUser"]').change(function () {
        var idUsuario = $(this).val(); // Obtenemos el ID del usuario seleccionado

        var urlDetalles = window.AppConfig.detailPageUrl; // Reutilizo la url almacenada en su respectiva vista
        var urlEdit = window.AppConfig.editPageUrl;
        var urlDelete = window.AppConfig.deletePageUrl;

        $('#btnDetalle').attr('href', urlDetalles + '/' + idUsuario);
        $('#btnEditar').attr('href', urlEdit + '/' + idUsuario);
        $('#btnEliminar').attr('href', urlDelete + '/' + idUsuario);

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

    function DeleteCierre() {
        // Elimina la clase 'show' para activar la transición de opacidad de 1 a 0
        $('#deleteModal').removeClass('show');

        // Espera a que la transición de CSS finalice antes de poner display: none
        var transitionDuration = parseFloat($('#deleteModal').css('transition-duration')) * 1000; // Obtiene duración en ms
        //console.log("Duración de transición:", transitionDuration, "ms");

        setTimeout(function () {
            $('#deleteModal').css('display', 'none'); // Oculta el modal completamente después de la transición
        }, transitionDuration);
    }
    function DeleteAbrir() {
        $('#deleteModal').css('display', 'flex');
        var triggerReflow = $('#deleteModal')[0].offsetHeight;
        $('#deleteModal').addClass('show');
    }

    $('#openBorrar').on('click', function () { DeleteAbrir(); });

    $('#deleteCancel').on('click', function () { DeleteCierre(); });
    $('#cancelDelete').on('click', function () { DeleteCierre(); });
    $(window).on('click', function (event) { if ($(event.target).is($('#deleteModal'))) { DeleteCierre(); } });
    $(document).on('keydown', function (event) {
        if (event.key === "Escape") {
            if ($('#deleteModal').hasClass('show')) { // Verifica si tiene la clase 'show'
                DeleteCierre();
            }
        }
    });
});
// ********************************************** Efecto de seguimiento(Elementos y texto) ******************************************* //
const buttons = document.querySelectorAll(".nav-link"); // Lee los elementos que tiene la clase 

buttons.forEach(button => { // Por cada elemento del array
    button.addEventListener("mousemove", (e) => { // Añade el evento
        const { x, y } = button.getBoundingClientRect();
        button.style.setProperty("--x", (e.clientX - x) + "px");
        button.style.setProperty("--y", (e.clientY - y) + "px");
    });
});

// *********************************************************************************************************************************** //
$(document).ready(function () {
    // ********************************************* Modal dinamico(Carga de html) ************************************************** //
    var modal = $('#dynamicModal');// El div principal del modal
    var modalBody = $('#cuerpoModal');// El div donde se cargará el contenido
    var modalClose = $('.cierreModal'); // El botón de cerrar

    // Objeto para almacenar la caché de vistas parciales
    var partialViewCache = {};

    // === Función para cargar contenido (un formulario parcial) y mostrar/actualizar el modal ===
    function CargaModal(url) {
        // === === === NUEVO: Intentar cargar desde la caché === === ===
        if (partialViewCache[url]) {
            //console.log("Cargando desde caché:", url); // Para depuración
            var cachedData = partialViewCache[url];
            modalBody.html(cachedData); // Carga el HTML desde la caché
            // Reinicializar validación después de cargar desde la caché
            var form = modalBody.find('form');
            if (form.length > 0 && $.validator && $.validator.unobtrusive) {
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);
            }
            modal.css('display', 'flex');
            var triggerReflow = modal[0].offsetHeight;
            modal.addClass('show');
            return; // Salimos de la función, no se hace la peticion ajax
        }
        $.get(url).done(function (response) {
            // Solo cacheamos si la respuesta es HTML (probablemente una vista parcial)
            if (typeof response === 'string' && response.indexOf('<form') > -1) {
                partialViewCache[url] = response; // Almacena el HTML en la caché
                //console.log("Guardado en caché:", url); // Para depuración
            }
            modalBody.html(response); // Carga el HTML recibido en el cuerpo del modal

            var form = modalBody.find('form'); // Encuentra el formulario dentro del modal
            if (form.length > 0) {
                // Remover cualquier validación vieja si existe (de formularios anteriores cargados)
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                // Parsear y aplicar validación al nuevo formulario cargado
                $.validator.unobtrusive.parse(form);
            }
            modal.css('display', 'flex');
                
            modal.addClass('show');
        }).fail(function () {
            // Opcional: Manejar errores si la petición AJAX falla
            modalBody.html('<p class="text-danger">Error al cargar el formulario. Inténtelo de nuevo.</p>');
            modal.addClass('show');
        });
    };
    // Esta lógica ya elimina la clase 'show' y espera a la transición
    function CierreModal() {
        // Elimina la clase 'show' para activar la transición de opacidad de 1 a 0
        modal.removeClass('show');

        // Espera a que la transición de CSS finalice antes de poner display: none
        var transitionDuration = parseFloat(modal.css('transition-duration')) * 1000; // Obtiene duración en ms
        //console.log("Duración de transición:", transitionDuration, "ms");

        setTimeout(function () {
            modal.css('display', 'none'); // Oculta el modal completamente después de la transición
        }, transitionDuration);
    }
    // Selecciona el enlace por su ID y añade el manejador 'click'
    $('#login').on('click', function (event) {
        event.preventDefault(); // Previene la navegación por defecto
        // Obtiene la URL del formulario que debe cargar este enlace desde el atributo data-form-url
        var formUrl = $(this).data('form-url');
        // Llama a la función para cargar el formulario y mostrar el modal
        CargaModal(formUrl);
    });

    // Los elementos encargados de redireccionar al login y registro(en el modal), se le atribuye la funcion click.
    modalBody.on('click', '#loginARegistro, #registroALogin', function (event) {
        event.preventDefault();
        var formUrl = $(this).data('form-url');
        CargaModal(formUrl);
    });

    // Envio de Peticiones Ajax(Afecta al login y registro)
    modalBody.on('submit', 'form', function (event) {
        event.preventDefault(); // Previene el envío estándar

        var form = $(this);
        var formUrl = form.attr('action');
        var formMethod = form.attr('method') || 'POST';
        // Opcional: Validación del lado del cliente antes de enviar
        if (form.valid && !form.valid()) {
            return;
        }
        var formData = form.serialize();
        $.ajax({
            url: formUrl,
            type: formMethod,
            data: formData,
            success: function (response, textStatus, jqXHR) {
                // 1. Si el servidor retorna una redirección (302)
                if (jqXHR.status === 302) {
                    window.location.href = jqXHR.getResponseHeader('Location');
                    return;
                }

                // 2. Si la respuesta es HTML (vista parcial con errores o éxito)
                if (typeof response === 'string' && response.indexOf('<form') > -1) {
                    // Si recibimos HTML que parece un formulario...
                    modalBody.html(response); // Reemplaza el contenido del modal
                    // === Reinicializar validación para el contenido recién cargado ===
                    var updatedForm = modalBody.find('form');
                    if (updatedForm.length > 0) {
                        updatedForm.removeData('validator');
                        updatedForm.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse(updatedForm);
                    } else {
                        // Si no hay formulario, podría ser una vista de éxito
                        setTimeout(() => {
                            window.location.reload(); // Recargar o redirigir
                        }, 1500);
                    }
                    return;
                }

                // 3. Si es JSON (ej: { success: true, redirect: "/dashboard" })
                if (typeof response === 'object' && response.success) {
                    window.location.href = response.redirect;
                    return;
                }

                // 4. Respuesta inesperada
                console.error("Respuesta no manejada:", response);
                modalBody.html('<p class="text-danger">Error inesperado. Recargue la página.</p>');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error AJAX:", textStatus, errorThrown);
                modalBody.html('<p class="text-danger">Error de conexión: ' + errorThrown + '</p>');
            }
        });
    });
    
    // === Lógica para Cerrar el Modal ===
    modalClose.on('click', function () { CierreModal(); });
    $(window).on('click', function (event) { if ($(event.target).is(modal)) { CierreModal(); } });
    $(document).on('keydown', function (event) {
        if (event.key === "Escape") {
            if (modal.hasClass('show')) { // Verifica si tiene la clase 'show'
                CierreModal();
            }
        }
    });

    var notifi = $('.conte-notif');
    var notifiClose = $('.cierre-notif');
    function CargarNotifi() {
        notifi.css('display', 'flex');

        notifi.addClass('show');
        return;
    }
    function CierreNotifi() {
        notifi.removeClass('show');

        var transitionDuration = parseFloat(notifi.css('transition-duration')) * 1000; // Obtiene duración en ms

        setTimeout(function () {
            notifi.css('display', 'none');
        }, transitionDuration);
        return;
    }
    notifiClose.on('click', function () { CierreNotifi(); });
    $(window).on('click', function (event) { if (!$(event.target).closest(notifi).length > 0) { CierreNotifi(); } }); // Porque no funciona del mismo modo que el modal??
    $(document).on('keydown', function (event) {
        if (event.key === "Escape") {
            if (notifi.hasClass('show')) { // Verifica si tiene la clase 'show'
                CierreNotifi();
            }
        }
    });
    CargarNotifi();
});
// ***************************************************************************************************************** //

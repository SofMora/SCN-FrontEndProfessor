// Cargar datos del profesor en el perfil
$(document).ready(function () {
    const professorId = 0;
    $('#professor-form').submit(function (event) {
        event.preventDefault();  // Detiene el envío del formulario
        saveChanges1();  // Llama a la función que maneja la lógica de guardar
    });

    // Event handler for "Ver comentarios" button (display comments)
    $(document).on("click", ".view-comments", function () {
        const newsId = $(this).data("news-id");

        // Show the modal
        $("#commentsModal").show();

        // Fetch and display comments
        fetchComments(newsId);
    });

   
    // Show login modal on page load
    $('#loginModal').fadeIn();

    // Close modal when close button is clicked
    $('#closeModal').click(function () {
        $('#loginModal').fadeOut();
    });

    // Switch to Login Tab
    $('#loginTab').click(function () {
        $('#loginFormContainer').show();
        $('#registerFormContainer').hide();
        $(this).addClass('active');
        $('#registerTab').removeClass('active');
    });

    // Switch to Register Tab
    $('#registerTab').click(function () {
        $('#registerFormContainer').show();
        $('#loginFormContainer').hide();
        $(this).addClass('active');
        $('#loginTab').removeClass('active');
    });

    //pmc
    $("#loginForm").submit(function (event) {
        event.preventDefault();

        // Obtener valores del formulario
        const username = $("#username").val();
        const password = $("#password").val();

        // Petición AJAX con método GET
        $.ajax({
            url: `https://localhost:44388/api/Professor/GetValidateProfessor/${username}/${password}`, 
            type: "GET",
            success: function (response) {

                localStorage.setItem('professorId', response.id);


                Swal.fire({
                    icon: "success",
                    title: "Inicio de sesión exitoso",
                    text: "Bienvenido, " + response.name + " (ID: " + response.id + ")"
                }).then(() => {
                    LoadProfessorData(response.id);
                    loadNewsAndComments();
                    $("#loginModal").fadeOut();
                });
            },
            error: function (xhr) {
                let errorMessage = "Usuario o contraseña incorrectos";
                if (xhr.status === 404) {
                    errorMessage = "El usuario no existe o las credenciales son incorrectas.";
                }
                Swal.fire({
                    icon: "error",
                    title: "Error de autenticación",
                    text: errorMessage
                });
            }
        });
    });
   

    // Evento para el formulario de registro
    $("#registerForm").submit(function (event) {
        event.preventDefault();
        registrarEstudiante(); // Function for registration logic
    });

    // Function to fetch and display comments
    function fetchComments(newsId) {
        $.ajax({
            url: `https://localhost:44388/api/Comment/GetCommentsByNewsId/${newsId}`,
            type: "GET",
            success: function (data) {
                let commentsContent = "";

                if (data.length === 0) {
                    commentsContent = "<p>No hay comentarios aún.</p>";
                } else {
                    data.forEach(comment => {
                        commentsContent += `
                        <div class="comment-item">
                            <p>${comment.description}</p>
                            <span class="comment-date">${new Date(comment.commentDate).toLocaleDateString()}</span>
                        </div>
                    `;
                    });
                }

                $("#commentsList").html(commentsContent);
                $("#submitComment").data("news-id", newsId); // Store newsId for later use
            },
            error: function () {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: "No se pudieron cargar los comentarios.",
                });
            }
        });
    }

    // Event handler for adding a new comment
    $("#submitComment").on("click", function () {
        const newsId = $(this).data("news-id");
        const description = $("#newComment").val().trim();

        if (description === "") {
            Swal.fire({
                icon: "warning",
                title: "Advertencia",
                text: "El comentario no puede estar vacío.",
            });
            return;
        }

        const newComment = {
            idNews: newsId,
            description: description,
            author: 13
        };

        // Submit the comment
        submitComment(newsId, newComment);
    });

    // Function to submit a new comment
    function submitComment(newsId, newComment) {
        $.ajax({
            url: "https://localhost:44388/api/Comment/AddComment",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(newComment),
            success: function () {
                Swal.fire({
                    icon: "success",
                    title: "Éxito",
                    text: "Comentario agregado correctamente.",
                });

                // Refresh comments
                fetchComments(newsId);

                // Clear the input field
                $("#newComment").val("");
            },
            error: function () {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: "No se pudo agregar el comentario.",
                });
            }
        });
    }


});
// Función para cargar los datos del profesor en el perfil

function LoadProfessorData(id) {
    $.ajax({
        url: `https://localhost:44388/api/Professor/GetProfessorById/${id}`,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result) {
                $('#professor-name').text(result.name);
                $('#professor-lastname').text(result.lastname);
                $('#professor-email').text(result.email);
                $('#professor-description').text(result.description);

                if (result.photo) {
                    $('#professor-photo').attr('src', `data:image/jpeg;base64,${btoa(String.fromCharCode(...new Uint8Array(result.photo)))}`);
                } else {
                    $('#professor-photo').attr('src', '/images/default-profile.png');
                }
            } else {
                console.error('No se recibieron datos del profesor');
            }
        },
        error: function (errorMessage) {
            console.error("Error al cargar los datos del profesor: ", errorMessage);
        }
    });
}

// Función para abrir el modal de edición
function openModal1() {
    var professorId = localStorage.getItem('professorId');
    if (!professorId) {
        console.error("No se encontró el ID del profesor.");
    } else {
        // Ahora el ID está disponible, puedes seguir con el proceso de abrir el modal
        $('#new-professor-name').val($('#professor-name').text());
        $('#new-professor-lastname').val($('#professor-lastname').text());
        $('#professor-profile-url').val($('#professor-social').attr('href'));
        $('#new-professor-description').val($('#professor-description').text());

        // Mostrar el modal
        $('#edit-professor-modal').removeClass('hidden');
    }
}


// Función para cerrar el modal
function closeModal1() {
    
    $('#edit-professor-modal').addClass('hidden');
}

// Función para guardar los cambios del perfil
function saveChanges1() {
    var professorId = localStorage.getItem('professorId');
    if (!professorId) {
        console.error('Error: No se encontró el ID del profesor.');
        return; // Detener si no hay ID
    }

    var updatedProfessor = {
        id: professorId,
        name: $('#new-professor-name').val(),
        lastName: $('#new-professor-lastname').val(),
        email: $('#new-professor-email').val(),
        userName: $('#new-professor-username').val(),
        password: $('#new-professor-password').val() || '',
        statusProfessor: $('#new-status').val(),
        socialLink: $('#professor-profile-url').val(),
        description: $('#new-professor-description').val(),
        photo: '' // Aquí iría la foto en base64 si se proporciona
    };

    sendProfessorData(updatedProfessor);
}

function sendProfessorData(updatedProfessor) {
    $.ajax({
        url: `https://localhost:44388/api/Professor/UpdateProfessor/${updatedProfessor.id}`,
        type: "PUT",
        data: JSON.stringify(updatedProfessor),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);  // Revisa lo que se recibe en la respuesta
            if (result.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Perfil actualizado',
                    text: 'El perfil se ha actualizado correctamente.',
                    confirmButtonText: 'OK'
                }).then(() => {
                    // Aquí ejecutas lo que necesites tras cerrar el popup
                    closeModal1();
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'No se pudo actualizar el perfil.',
                });
            }
        },
        error: function (errorMessage) {
            console.error('Error al actualizar el perfil: ', errorMessage.responseText);
            Swal.fire({
                icon: 'error',
                title: 'Error al actualizar',
                text: 'Ocurrió un problema al actualizar el perfil.',
            });
        }
    });
}



// Mostrar vista previa de la foto seleccionada en el modal
$('#new-professor-photo').on('change', function (event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            $('#professor-photo').attr('src', e.target.result);  // Actualiza la imagen en el perfil
        };
        reader.readAsDataURL(file);  // Convierte la imagen a base64
    }
});


function loadNewsAndComments() {
    console.log("Cargando noticias...");

    $.ajax({
        url: "https://localhost:44388/api/New",
        type: "GET",
        success: function (data) {
            let newsContent = "";

            if (data.length === 0) {
                newsContent = "<p>No hay noticias disponibles.</p>";
            } else {
                data.forEach(news => {
                    newsContent += `
                        <article class="news-item">
                            <div class="news-content">
                                <h3>${news.title}</h3>
                                <p>${news.textNews}</p>
                                <span class="news-date">${new Date(news.dateNews).toLocaleDateString()}</span>
                               <br> </br>
                                <button class="view-comments" data-news-id="${news.id}">Ver comentarios</button>
                            </div>
                        </article>
                    `;
                });
            }

            $(".news-scroll").html(newsContent);
        },
        error: function () {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "No se pudo cargar las noticias.",
            });
        }
    });


    // Close the modal when clicking on the close button
    $(".close-btn").on("click", function () {
        $("#commentsModal").hide();
    });

    // Optionally, close the modal if the user clicks outside the modal content
    $(window).on("click", function (event) {
        if ($(event.target).is("#commentsModal")) {
            $("#commentsModal").hide();
        }
    });

  

}
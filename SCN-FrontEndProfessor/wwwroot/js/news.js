//document.addEventListener('DOMContentLoaded', function () {
//    const newsScroll = document.querySelector('.news-scroll');

//    // Función para cargar las noticias desde la API
//    function loadNewsAndComments() {
//        console.log("Cargando noticias...");

//        $.ajax({
//            url: "https://localhost:44388/api/New",
//            type: "GET",
//            success: function (data) {
//                let newsContent = "";

//                if (data.length === 0) {
//                    newsContent = "<p>No hay noticias disponibles.</p>";
//                } else {
//                    data.forEach(news => {
//                        newsContent += `
//                        <article class="news-item">
//                            <div class="news-content">
//                                <h3>${news.title}</h3>
//                                <p>${news.textNews}</p>
//                                <span class="news-date">${new Date(news.dateNews).toLocaleDateString()}</span>
//                               <br> </br>
//                                <button class="view-comments" data-news-id="${news.id}">Ver comentarios</button>
//                            </div>
//                        </article>
//                    `;
//                    });
//                }

//                $(".news-scroll").html(newsContent);
//            },
//            error: function () {
//                Swal.fire({
//                    icon: "error",
//                    title: "Error",
//                    text: "No se pudo cargar las noticias.",
//                });
//            }

//        });
//    }
//    //// Función para cargar los comentarios desde la API
//    //async function loadComments(newsId) {
//    //    try {
//    //        const response = await fetch(`https://localhost:44388/api/Comment/GetCommentsByNewsId/${newsId}`);
//    //        if (!response.ok) {
//    //            throw new Error('No se pudieron cargar los comentarios.');
//    //        }
//    //        const comments = await response.json();

//    //        let commentsContent = "";

//    //        if (comments.length === 0) {
//    //            commentsContent = "<p>No hay comentarios aún.</p>";
//    //        } else {
//    //            comments.forEach(comment => {
//    //                commentsContent += `
//    //                    <div class="comment-item">
//    //                        <p>${comment.description}</p>
//    //                        <span class="comment-date">${new Date(comment.commentDate).toLocaleDateString()}</span>
//    //                    </div>
//    //                `;
//    //            });
//    //        }
//    //        $("#submitComment").data("news-id", newsId); // Store newsId for later use

//    //        document.getElementById("commentsList").innerHTML = commentsContent;

//    //    } catch (error) {
//    //        console.error(error);
//    //        document.getElementById("commentsList").innerHTML = "<p>No se pudieron cargar los comentarios. Intenta más tarde.</p>";
//    //    }
//    //}

//    // Llamada a la función para cargar las noticias al cargar la página
//    loadNewsAndComments();

//    // Event handler for "Ver comentarios" button (display comments)
//    $(document).on("click", ".view-comments", function () {
//        const newsId = $(this).data("news-id");

//        // Show the modal
//        $("#commentsModal").show();

//        // Fetch and display comments
//        fetchComments(newsId);
//    });

//    // Close the modal when clicking on the close button
//    $(".close-btn").on("click", function () {
//        $("#commentsModal").hide();
//    });

//    // Optionally, close the modal if the user clicks outside the modal content
//    $(window).on("click", function (event) {
//        if ($(event.target).is("#commentsModal")) {
//            $("#commentsModal").hide();
//        }
//    });



//    // Event handler for adding a new comment
//    $("#submitComment").on("click", function () {
//        const newsId = $(this).data("news-id");
//        const description = $("#newComment").val().trim();
//        console.log('Prueba');

//        if (description === "") {
//            Swal.fire({
//                icon: "warning",
//                title: "Advertencia",
//                text: "El comentario no puede estar vacío.",
//            });
//            return;
//        }
//        const newComment = {
//            idNews: newsId,
//            description: description,
//            author: 13
//        };

//        // Submit the comment
//        submitComment(newsId, newComment);
//    });

//    function submitComment(newsId, newComment) {
//        console.log(newComment);

//        $.ajax({
//            url: "https://localhost:44388/api/Comment/AddComment",
//            type: "POST",
//            contentType: "application/json",
//            data: JSON.stringify(newComment),
//            success: function () {
//                Swal.fire({
//                    icon: "success",
//                    title: "Éxito",
//                    text: "Comentario agregado correctamente.",
//                });

//                // Refresh comments
//                fetchComments(newsId);

//                // Clear the input field
//                $("#newComment").val("");
//            },
//            error: function () {
//                Swal.fire({
//                    icon: "error",
//                    title: "Error",
//                    text: "No se pudo agregar el comentario.",
//                });
//            }
//        });
//    }

    
//});

//// Function to fetch and display comments
//function fetchComments(newsId) {
//    $.ajax({
//        url: "https://localhost:44388/api/Comment/GetCommentsByNewsId/${newsId}",
//        type: "GET",
//        success: function (data) {
//            let commentsContent = "";

//            if (data.length === 0) {
//                commentsContent = "<p>No hay comentarios aún.</p>";
//            } else {
//                data.forEach(comment => {
//                    commentsContent += `
//                     <div class="comment-item">
//                         <p>${comment.description}</p>
//                         <span class="comment-date">${new Date(comment.commentDate).toLocaleDateString()}</span>
//                     </div>
//                 `;
//                });
//            }

//            $("#commentsList").html(commentsContent);
//            $("#submitComment").data("news-id", newsId); // Store newsId for later use
//        },
//        error: function () {
//            Swal.fire({
//                icon: "error",
//                title: "Error",
//                text: "No se pudieron cargar los comentarios.",
//            });
//        }
        
//     });

//  }
 
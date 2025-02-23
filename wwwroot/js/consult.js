document.addEventListener("DOMContentLoaded", function () {
    
    fetchConsultations();
});

function submitResponse(consultId) {
    const responseText = document.getElementById("response-text").value.trim();
    if (!responseText) {
        alert("Please enter a response.");
        return;
    }

    // Obtener la fecha actual en formato ISO (como lo espera la API)
    const dateComment = new Date().toISOString();

    // Asume que el ID del autor es el ID del usuario actual, puedes cambiar esto según tu sistema
    const author = 1; // Reemplázalo con el ID del usuario actual si es necesario

    // Preparar el cuerpo de la solicitud con los datos correctos
    const commentData = {
        id: 0, // Si el ID debe ser 0 o generado por la base de datos
        idConsult: consultId, // El ID de la consulta
        descriptionComment: responseText, // El texto de la respuesta
        author: author, // El ID del autor
        dateComment: dateComment // Fecha de la respuesta
    };

    // Hacer la solicitud POST a la API
    fetch(`https://localhost:44388/api/CommentConsult/AddComment`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(commentData)
    })
        .then((res) => {
            if (!res.ok) {
                return res.text().then(text => { throw new Error(text) });
            }
            return res.json();
        })
        .then(() => {
            alert("Response submitted successfully.");
            closeModal();
        })
        .catch((err) => {
            console.error("Error submitting comment:", err);
            alert(`Error: ${err.message}`);
        });
}

// Obtener las consultas
async function fetchConsultations() {
    try {
        const response = await fetch("https://localhost:44388/api/Consult/GetAllConsults");
        if (!response.ok) throw new Error("Error fetching consultations");

        const consultations = await response.json();
        populateTables(consultations);
    } catch (error) {
        console.error("Error fetching data:", error);
    }
}

// Poblamos las tablas de consultas
function populateTables(consultations) {
    const publicTable = document.getElementById("public-consultations");
    const privateTable = document.getElementById("private-consultations");

    publicTable.innerHTML = "";
    privateTable.innerHTML = "";

    consultations.forEach((consult) => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${consult.author}</td>
            <td>${consult.descriptionConsult}</td>
            <td>${new Date(consult.dateConsult).toLocaleDateString()}</td>
            <td>${consult.idCourse}</td>
            <td class="text-center">
                <button class="btn btn-primary btn-sm" onclick="openModal(${consult.id}, '${encodeURIComponent(consult.descriptionConsult)}')">Reply</button>
                <button class="btn btn-info btn-sm" onclick="viewReplies(${consult.id})">View Replies</button>
            </td>
        `;

        if (consult.typeConsult == 1) {
            publicTable.appendChild(row);
        } else {
            privateTable.appendChild(row);
        }
    });
}


// Abre el modal para responder la consulta
function openModal(id, description) {
    const modal = document.getElementById("response-modal");
    const responseText = document.getElementById("response-text");

    // Limpia el textarea antes de abrir el modal
    responseText.value = "";

    document.getElementById("consult-description").innerText = description;
    modal.style.display = "block";

    document.getElementById("submit-response").onclick = function () {
        submitResponse(id);
    };
}

// Mostrar los comentarios de una consulta
function viewReplies(consultId) {
    fetch(`https://localhost:44388/api/CommentConsult/GetReplies/${consultId}`)
        .then(response => response.json())
        .then(comments => {
            const repliesModal = document.getElementById("replies-modal");
            const repliesList = document.getElementById("replies-list");

            repliesList.innerHTML = ""; // Limpiar respuestas anteriores

            comments.forEach(comment => {
                const replyItem = document.createElement("div");
                replyItem.classList.add("reply-item");
                replyItem.innerHTML = `
                    <p><strong>${comment.author}</strong> (${new Date(comment.dateComment).toLocaleString()}):</p>
                    <p>${comment.descriptionComment}</p>
                    <hr>
                `;
                repliesList.appendChild(replyItem);
            });

            repliesModal.style.display = "block"; // Mostrar el modal con las respuestas
        })
        .catch(error => {
            console.error("Error fetching replies:", error);
            alert("No hay respuestas.");
        });
}


// Cerrar modal de respuestas
document.querySelectorAll(".close").forEach((closeBtn) => {
    closeBtn.addEventListener("click", closeModal);
});

window.addEventListener("click", function (event) {
    const modal = document.getElementById("response-modal");
    const repliesModal = document.getElementById("replies-modal");
    if (event.target === modal || event.target === repliesModal) {
        closeModal();
    }
});

function closeModal() {
    document.getElementById("response-modal").style.display = "none";
    document.getElementById("replies-modal").style.display = "none";
}

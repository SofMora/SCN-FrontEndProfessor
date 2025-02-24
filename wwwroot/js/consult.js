document.addEventListener("DOMContentLoaded", function () {
    fetchConsultations();
});

function showMessage(message, type = 'success') {
    Swal.fire({
        icon: type === 'success' ? 'success' : 'error',
        title: type === 'success' ? 'Éxito' : 'Error',
        text: message
    });
}

function submitResponse(consultId) {
    const responseText = document.getElementById("response-text").value.trim();
    if (!responseText) {
        showMessage("Por favor ingrese una respuesta.", 'error');
        return;
    }

    const dateComment = new Date().toISOString();
    const author = 1;

    const commentData = {
        id: 0,
        idConsult: consultId,
        descriptionComment: responseText,
        author: author,
        dateComment: dateComment
    };

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
            showMessage("Respuesta enviada correctamente.");
            closeModal();
        })
        .catch((err) => {
            console.error("Error al enviar el comentario:", err);
            showMessage(`Error: ${err.message}`, 'error');
        });
}

async function fetchConsultations() {
    try {
        const response = await fetch("https://localhost:44388/api/Consult/GetAllConsults");
        if (!response.ok) throw new Error("Error al obtener las consultas");

        const consultations = await response.json();
        populateTables(consultations);
    } catch (error) {
        console.error("Error al obtener los datos:", error);
        showMessage("No se pudieron cargar las consultas.", 'error');
    }
}

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
                <button class="btn btn-info btn-sm" onclick="viewReplies(${consult.id})">View replies</button>
            </td>
        `;

        if (consult.typeConsult == 1) {
            publicTable.appendChild(row);
        } else {
            privateTable.appendChild(row);
        }
    });
}

function openModal(id, description) {
    const modal = document.getElementById("response-modal");
    const responseText = document.getElementById("response-text");

    responseText.value = "";

    document.getElementById("consult-description").innerText = description;
    modal.style.display = "block";

    document.getElementById("submit-response").onclick = function () {
        submitResponse(id);
    };
}

function viewReplies(consultId) {
    fetch(`https://localhost:44388/api/CommentConsult/GetReplies/${consultId}`)
        .then(response => response.json())
        .then(comments => {
            const repliesModal = document.getElementById("replies-modal");
            const repliesList = document.getElementById("replies-list");

            repliesList.innerHTML = "";

            if (comments.length === 0) {
                showMessage("No hay respuestas disponibles.", 'info');
                return;
            }

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

            repliesModal.style.display = "block";
        })
        .catch(error => {
            console.error("Error al obtener respuestas:", error);
            showMessage("No existen respuestas.", 'error');
        });
}

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

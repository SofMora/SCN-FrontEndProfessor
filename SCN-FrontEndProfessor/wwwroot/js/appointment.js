document.addEventListener("DOMContentLoaded", function () {
    loadAppointmentsByProfessor();
});

// Cargar todas las citas relacionadas con el profesor desde la API
function loadAppointmentsByProfessor() {
    const professorId = localStorage.getItem('professorId'); // Obtener el ID del profesor desde localStorage

    if (!professorId) {
        console.error("No se encontró el ID del profesor. Por favor, inicia sesión.");
        return;
    }

    fetch(`https://localhost:44388/api/Appointment/GetAppointmentsByProfessor/${professorId}`)
        .then(response => response.json())
        .then(data => {
            const scheduledTable = document.getElementById("scheduled-appointments");
            const requestsTable = document.getElementById("appointment-requests");

            scheduledTable.innerHTML = ""; // Limpiar tablas antes de agregar nuevas filas
            requestsTable.innerHTML = "";

            data.forEach(appointment => {
                const formattedDate = new Date(appointment.dateAppointment).toLocaleString();

                const row = `<tr>
                    <td>${appointment.idStudent}</td>
                    <td>${appointment.subjectAppointment}</td>
                    <td>${formattedDate}</td>
                    <td>${appointment.descriptionAppointment || "No description"}</td>
                    <td>${appointment.commentStatus || "No comments"}</td>
                    ${!appointment.statusAppointment ? ` 
                        <td>
                            <button class="btn btn-success" onclick="openDecisionModal(${appointment.id}, '${appointment.idStudent}')">✔</button>
                            <button class="btn btn-danger" onclick="openDecisionModal(${appointment.id}, '${appointment.idStudent}')">✖</button>
                        </td>` : ""
                    }
                </tr>`;

                if (appointment.statusAppointment) {
                    scheduledTable.innerHTML += row;
                } else {
                    requestsTable.innerHTML += row;
                }
            });
        })
        .catch(error => console.error('Error fetching data:', error));
}



//document.addEventListener("DOMContentLoaded", function () {
//    loadAllAppointments();
//});

//// Cargar todas las citas desde la API
//function loadAllAppointments() {
//    fetch("https://localhost:44388/api/Appointment/GetAllAppointments")
//        .then(response => response.json())
//        .then(data => {
//            const scheduledTable = document.getElementById("scheduled-appointments");
//            const requestsTable = document.getElementById("appointment-requests");

//            scheduledTable.innerHTML = ""; // Limpiar tablas antes de agregar nuevas filas
//            requestsTable.innerHTML = "";

//            data.forEach(appointment => {
//                const formattedDate = new Date(appointment.dateAppointment).toLocaleString();

//                const row = `<tr>
//                    <td>${appointment.idStudent}</td>
//                    <td>${appointment.subjectAppointment}</td>
//                    <td>${formattedDate}</td>
//                    <td>${appointment.descriptionAppointment || "No description"}</td>
//                    <td>${appointment.commentStatus || "No comments"}</td>
//                    ${!appointment.statusAppointment ? `
//                        <td>
//                            <button class="btn btn-success" onclick="openDecisionModal(${appointment.id}, '${appointment.idStudent}')">✔</button>
//                            <button class="btn btn-danger" onclick="openDecisionModal(${appointment.id}, '${appointment.idStudent}')">✖</button>
//                        </td>` : ""
//                    }
//                </tr>`;

//                if (appointment.statusAppointment) {
//                    scheduledTable.innerHTML += row;
//                } else {
//                    requestsTable.innerHTML += row;
//                }
//            });
//        })
//        .catch(error => console.error('Error fetching data:', error));
//}

// Variables para rastrear la cita actual
let currentAppointmentId = null;

// Abre el modal y almacena el ID de la cita
function openDecisionModal(appointmentId, studentName) {
    currentAppointmentId = appointmentId;
    document.getElementById("modal-student").innerText = studentName;
    document.getElementById("decision-modal").style.display = "block";
}

// Envía la decisión al backend
function sendDecision(status) {
    let reason = document.getElementById("decision-message").value.trim();

    if (!currentAppointmentId) {
        Swal.fire("Error", "No se encontró el ID de la cita.", "error");
        return;
    }

    if (!reason) {
        Swal.fire("Advertencia", "Por favor ingresa una razón.", "warning");
        return;
    }

    // Convertir "approve" en true y "reject" en false
    let requestData = {
        statusAppointment: status === "accepted",
        commentStatus: reason
    };

    console.log("URL enviada:", `https://localhost:44388/api/Appointment/UpdateStatus/${currentAppointmentId}`);
    console.log("Cuerpo de la petición:", JSON.stringify(requestData));

    $.ajax({
        url: `https://localhost:44388/api/Appointment/UpdateStatus/${currentAppointmentId}`,
        type: "PUT",
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(requestData),
        success: function () {
            Swal.fire("Éxito", "Estado actualizado correctamente", "success");
            closeDecisionModal();
            loadAllAppointments(); // Recargar la lista de citas
        },
        error: function (xhr) {
            console.error("Error en la petición AJAX:", xhr.responseText);
            Swal.fire("Error", "No se pudo actualizar la cita", "error");
        }
    });
}



// Cierra el modal
function closeDecisionModal() {
    document.getElementById("decision-modal").style.display = "none";
    document.getElementById("decision-message").value = ""; // Limpiar el textarea
    currentAppointmentId = null; // Resetear la variable
}



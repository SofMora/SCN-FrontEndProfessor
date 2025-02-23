document.addEventListener('DOMContentLoaded', function () {
    const scheduleList = document.getElementById('schedule-list');
    const professorId = localStorage.getItem('professorId'); // Obtenemos el ID del profesor desde el localStorage

    if (!professorId) {
        console.error("No se encontró el ID del profesor. Por favor, inicia sesión.");
        scheduleList.innerHTML = '<tr><td colspan="2" class="text-center text-danger">Por favor, inicia sesión para ver tu horario.</td></tr>';
        return;
    }

    async function loadSchedule() {
        try {
            const response = await fetch(`https://localhost:44388/api/ScheduleProfessor/GetSchedulesByProfessorId/${professorId}`);
            if (!response.ok) {
                throw new Error('Error al cargar el horario');
            }
            const schedules = await response.json();

            scheduleList.innerHTML = '';

            if (schedules.length === 0) {
                scheduleList.innerHTML = '<tr><td colspan="2" class="text-center text-warning">No tienes horarios asignados.</td></tr>';
                return;
            }

            schedules.forEach(schedule => {
                const row = document.createElement('tr');

                const dayCell = document.createElement('td');
                dayCell.textContent = schedule.day;
                row.appendChild(dayCell);

                const timeCell = document.createElement('td');
                timeCell.textContent = schedule.time;
                row.appendChild(timeCell);

                scheduleList.appendChild(row);
            });
        } catch (error) {
            console.error(error);
            scheduleList.innerHTML = '<tr><td colspan="2" class="text-center text-danger">No se pudo cargar el horario. Intenta más tarde.</td></tr>';
        }
    }

    loadSchedule();
});



//document.addEventListener('DOMContentLoaded', function () {
//    const scheduleList = document.getElementById('schedule-list');

//    // Función para cargar el horario desde la API
//    async function loadSchedule() {
//        try {
//            const response = await fetch('https://localhost:44388/api/ScheduleProfessor/GetAllSchedules'); // Reemplaza con la URL correcta de tu API
//            if (!response.ok) {
//                throw new Error('Error al cargar el horario');
//            }
//            const schedules = await response.json();

//            // Limpiamos el contenedor antes de agregar los horarios
//            scheduleList.innerHTML = '';

//            // Recorremos los horarios y los agregamos al contenedor
//            schedules.forEach(schedule => {
//                const row = document.createElement('tr');

//                const dayCell = document.createElement('td');
//                dayCell.textContent = schedule.day;
//                row.appendChild(dayCell);

//                const timeCell = document.createElement('td');
//                timeCell.textContent = schedule.time;
//                row.appendChild(timeCell);

//                // Añadimos la fila al cuerpo de la tabla
//                scheduleList.appendChild(row);
//            });
//        } catch (error) {
//            console.error(error);
//            // Si hay un error, puedes mostrar un mensaje
//            scheduleList.innerHTML = '<tr><td colspan="2" class="text-center text-danger">No se pudo cargar el horario. Intenta más tarde.</td></tr>';
//        }
//    }

//    // Llamamos a la función para cargar el horario cuando la página haya cargado
//    loadSchedule();
//});
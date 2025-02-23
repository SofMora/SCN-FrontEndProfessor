document.addEventListener('DOMContentLoaded', function () {
    const coursesList = document.getElementById('courses-list');

    // Función para cargar los cursos desde la API
    async function loadCourses() {
        try {
            const response = await fetch('https://localhost:44388/api/Course/GetAllCourses'); // Cambia la URL por la correcta de tu API
            if (!response.ok) {
                throw new Error('Error al cargar los cursos');
            }
            const courses = await response.json();

            // Limpiamos el contenedor antes de agregar los cursos
            coursesList.innerHTML = '';

            // Recorremos los cursos y los agregamos al contenedor
            courses.forEach(course => {
                const courseElement = document.createElement('div');
                courseElement.classList.add('col-md-4', 'course-card');

                // Se define el HTML para cada curso
                courseElement.innerHTML = `
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">${course.name}</h5>
                            <p class="card-text">${course.description}</p>
                            <p><strong>Cycle:</strong> ${course.cycle}</p>
                            <p><strong>Status:</strong> ${course.statusCourse}</p>
                        </div>
                    </div>
                `;

                // Añadimos el curso al contenedor de cursos
                coursesList.appendChild(courseElement);
            });
        } catch (error) {
            console.error(error);
            // Si hay un error, puedes mostrar un mensaje
            coursesList.innerHTML = '<p class="error-message">No se pudieron cargar los cursos. Intenta más tarde.</p>';
        }
    }

    // Llamamos a la función para cargar los cursos cuando la página haya cargado
    loadCourses();
});

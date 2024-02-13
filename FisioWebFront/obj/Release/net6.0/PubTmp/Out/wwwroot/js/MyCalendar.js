document.addEventListener('DOMContentLoaded', function () {
    locale: 'es';
    var events = [];
    $.ajax
        ({
            url: '/Citas/ConsultarCitas/',
            type: 'GET',
            dataType: 'JSON',
            //data: JSON.stringify(object),
            // contentType: "application/json"
            error: function (error) {
                alert('ERROR ' + error);
            },
            success: function (result) {

                $.each(result, function (i, data) {
                    $('#description').text(data.horaInicio_cita);
                    events.push(
                        {
                            aspectRatio: 2,
                            title: data.descripcion_cita,
                            description: data.descripcion_cita,
                            start: moment(data.horaInicio_cita).format('YYYY-MM-DD'),
                            end: moment(data.horaFinal_cita),
                            textColor: 'rgba(255,255,255,1)',
                            backgroundColor: 'rgba(57,6,232,1)'
                        }
                    );
                });
                callback(events);
            }
        })
})
function callback(result) {

    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl,
        {
            initialView: 'dayGridMonth',
            locale: 'es',
            timeZone: 'local',
            headerToolbar:
            {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek'
            },
            hiddenDays: [0],
            navLinks: true, // can click day/week names to navigate views
            selectable: true,
            selectMirror: true,
            businessHours:
            {
                // days of week. an array of zero-based day of week integers (0=Sunday)
                daysOfWeek: [1, 2, 3, 4, 5, 6], // Monday - Thursday

                startTime: '08:00', // a start time (10am in this example)
                endTime: '17:00', // an end time (6pm in this example)
            },
            select: function (arg) {
                //if (confirm('¿Desea agendar una cita?')) {
                //    var title = prompt('Ingrese una descripción para cita');
                //    if (title) {
                //        calendar.addEvent({
                //            title: title,
                //            start: arg.start,
                //            end: arg.end,
                //            allDay: arg.allDay
                //        })
                //    }
                //}

                //Linea para llamar el MODAL al darle click a una casilla del calendario
                $('#addEventModal').modal('show');

                calendar.unselect()
            },
            eventClick: function (arg) {
                if (confirm('¿Desea cancelar su cita?')) {
                    arg.event.remove()
                }
            },
            editable: true,

            timeZone: 'UTC',
            events: result
        });

    calendar.render();
};
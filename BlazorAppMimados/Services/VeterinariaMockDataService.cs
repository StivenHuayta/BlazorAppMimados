using Mimados_WebApp.Models;

namespace Mimados_WebApp.Services
{
    public class VeterinariaMockDataService
    {
        public int EmpleadoIdActual { get; set; } = 1;
        public string VeterinarioActual { get; set; } = "Dr. Carlos Mendoza";
        public string CmvpActual { get; set; } = "CMVP 08421";
        public string SedeActual { get; set; } = "Sede Central";

        private List<Mascota> _mascotas = new();
        private List<Cita> _citas = new();
        private List<Atencion> _atenciones = new();

        public VeterinariaMockDataService()
        {
            InicializarDatos();
        }

        private void InicializarDatos()
        {
            _mascotas = new List<Mascota>
            {
                new Mascota
                {
                    MascotaId = 1,
                    ClienteId = 1,
                    Nombre = "Firulais",
                    Especie = "CANINO",
                    Raza = "Mestizo",
                    Sexo = "MACHO",
                    FechaNacimiento = DateTime.Today.AddYears(-4).AddMonths(-2),
                    PesoReferencial = 15.5m,
                    NombreCliente = "Juan Pérez Romero",
                    DniCliente = "45871234",
                    TelefonoCliente = "987654321",
                    TelefonoEmergencia = "977112233",
                    DireccionCliente = "Av. Universitaria 1801, San Miguel",
                    Alergias = "Ninguna conocida"
                },
                new Mascota
                {
                    MascotaId = 2,
                    ClienteId = 2,
                    Nombre = "Michi",
                    Especie = "FELINO",
                    Raza = "Siamés",
                    Sexo = "HEMBRA",
                    FechaNacimiento = DateTime.Today.AddYears(-2).AddMonths(-5),
                    PesoReferencial = 4.2m,
                    NombreCliente = "Ana Gómez Salazar",
                    DniCliente = "71239845",
                    TelefonoCliente = "912345678",
                    TelefonoEmergencia = "922446688",
                    DireccionCliente = "Calle Las Flores 230, Pueblo Libre",
                    Alergias = "Hipersensibilidad a antiparasitarios organofosforados"
                },
                new Mascota
                {
                    MascotaId = 3,
                    ClienteId = 3,
                    Nombre = "Rocky",
                    Especie = "CANINO",
                    Raza = "Golden Retriever",
                    Sexo = "MACHO",
                    FechaNacimiento = DateTime.Today.AddYears(-3).AddMonths(-1),
                    PesoReferencial = 28.0m,
                    NombreCliente = "Carlos Ruiz Peña",
                    DniCliente = "40192837",
                    TelefonoCliente = "998877665",
                    TelefonoEmergencia = "988112244",
                    DireccionCliente = "Av. La Marina 1540, San Miguel",
                    Alergias = "Alergia a picadura de pulga (DAPP)"
                },
                new Mascota
                {
                    MascotaId = 4,
                    ClienteId = 4,
                    Nombre = "Luna",
                    Especie = "CANINO",
                    Raza = "Poodle",
                    Sexo = "HEMBRA",
                    FechaNacimiento = DateTime.Today.AddYears(-5),
                    PesoReferencial = 6.8m,
                    NombreCliente = "María López Prado",
                    DniCliente = "47281903",
                    TelefonoCliente = "976543210",
                    TelefonoEmergencia = "955331122",
                    DireccionCliente = "Jr. Tacna 450, Magdalena",
                    Alergias = "Ninguna conocida"
                },
                new Mascota
                {
                    MascotaId = 5,
                    ClienteId = 5,
                    Nombre = "Zeus",
                    Especie = "CANINO",
                    Raza = "Pastor Alemán",
                    Sexo = "MACHO",
                    FechaNacimiento = DateTime.Today.AddYears(-6).AddMonths(-3),
                    PesoReferencial = 34.5m,
                    NombreCliente = "Pedro Castillo Vera",
                    DniCliente = "41982736",
                    TelefonoCliente = "954321987",
                    TelefonoEmergencia = "944221100",
                    DireccionCliente = "Av. Sucre 890, Pueblo Libre",
                    Alergias = "Sensibilidad gástrica a alimento seco de ave"
                }
            };

            var hoy = DateTime.Today;

            _citas = new List<Cita>
            {
                new Cita
                {
                    CitaId = 101,
                    EmpleadoId = 1,
                    MascotaId = 1,
                    FechaHoraInicio = hoy.AddHours(9).AddMinutes(0),
                    FechaHoraFin = hoy.AddHours(9).AddMinutes(45),
                    Estado = "CONFIRMADA",
                    Motivo = "Control general y revisión por decaimiento y vómitos aislados",
                    SedeNombre = "Sede Central",
                    NombreMascota = "Firulais",
                    EspecieMascota = "CANINO",
                    RazaMascota = "Mestizo",
                    SexoMascota = "MACHO",
                    NombreCliente = "Juan Pérez Romero",
                    TelefonoCliente = "987654321",
                    NombreVeterinario = VeterinarioActual,
                    CodigoCmvp = CmvpActual,
                    ServicioSolicitado = "Consulta General",
                    MontoEstimado = 60.00m
                },
                new Cita
                {
                    CitaId = 102,
                    EmpleadoId = 1,
                    MascotaId = 2,
                    FechaHoraInicio = hoy.AddHours(10).AddMinutes(30),
                    FechaHoraFin = hoy.AddHours(11).AddMinutes(15),
                    Estado = "PROGRAMADA",
                    Motivo = "Refuerzo anual de Vacuna Triple Felina y control de peso",
                    SedeNombre = "Sede Central",
                    NombreMascota = "Michi",
                    EspecieMascota = "FELINO",
                    RazaMascota = "Siamés",
                    SexoMascota = "HEMBRA",
                    NombreCliente = "Ana Gómez Salazar",
                    TelefonoCliente = "912345678",
                    NombreVeterinario = VeterinarioActual,
                    CodigoCmvp = CmvpActual,
                    ServicioSolicitado = "Vacunación",
                    MontoEstimado = 75.00m
                },
                new Cita
                {
                    CitaId = 103,
                    EmpleadoId = 1,
                    MascotaId = 3,
                    FechaHoraInicio = hoy.AddHours(12).AddMinutes(0),
                    FechaHoraFin = hoy.AddHours(12).AddMinutes(45),
                    Estado = "PROGRAMADA",
                    Motivo = "Evaluación de claudicación en miembro posterior izquierdo tras paseo",
                    SedeNombre = "Sede Central",
                    NombreMascota = "Rocky",
                    EspecieMascota = "CANINO",
                    RazaMascota = "Golden Retriever",
                    SexoMascota = "MACHO",
                    NombreCliente = "Carlos Ruiz Peña",
                    TelefonoCliente = "998877665",
                    NombreVeterinario = VeterinarioActual,
                    CodigoCmvp = CmvpActual,
                    ServicioSolicitado = "Traumatología y Ortopedia",
                    MontoEstimado = 90.00m
                },
                new Cita
                {
                    CitaId = 104,
                    EmpleadoId = 1,
                    MascotaId = 4,
                    FechaHoraInicio = hoy.AddDays(-1).AddHours(15).AddMinutes(30),
                    FechaHoraFin = hoy.AddDays(-1).AddHours(16).AddMinutes(15),
                    Estado = "ATENDIDA",
                    Motivo = "Control dermatológico por eritema y prurito intenso en vientre",
                    SedeNombre = "Sede Central",
                    NombreMascota = "Luna",
                    EspecieMascota = "CANINO",
                    RazaMascota = "Poodle",
                    SexoMascota = "HEMBRA",
                    NombreCliente = "María López Prado",
                    TelefonoCliente = "976543210",
                    NombreVeterinario = VeterinarioActual,
                    CodigoCmvp = CmvpActual,
                    ServicioSolicitado = "Dermatología Veterinaria",
                    MontoEstimado = 70.00m
                },
                new Cita
                {
                    CitaId = 105,
                    EmpleadoId = 1,
                    MascotaId = 5,
                    FechaHoraInicio = hoy.AddDays(-2).AddHours(11).AddMinutes(0),
                    FechaHoraFin = hoy.AddDays(-2).AddHours(11).AddMinutes(45),
                    Estado = "CANCELADA",
                    Motivo = "Cita cancelada por viaje del tutor",
                    SedeNombre = "Sede Central",
                    NombreMascota = "Zeus",
                    EspecieMascota = "CANINO",
                    RazaMascota = "Pastor Alemán",
                    SexoMascota = "MACHO",
                    NombreCliente = "Pedro Castillo Vera",
                    TelefonoCliente = "954321987",
                    NombreVeterinario = VeterinarioActual,
                    CodigoCmvp = CmvpActual,
                    ServicioSolicitado = "Consulta General",
                    MontoEstimado = 60.00m
                },
                new Cita
                {
                    CitaId = 106,
                    EmpleadoId = 1,
                    MascotaId = 1,
                    FechaHoraInicio = hoy.AddDays(1).AddHours(10).AddMinutes(0),
                    FechaHoraFin = hoy.AddDays(1).AddHours(10).AddMinutes(30),
                    Estado = "PROGRAMADA",
                    Motivo = "Control de seguimiento y retiro de puntos quirúrgicos",
                    SedeNombre = "Sede Central",
                    NombreMascota = "Firulais",
                    EspecieMascota = "CANINO",
                    RazaMascota = "Mestizo",
                    SexoMascota = "MACHO",
                    NombreCliente = "Juan Pérez Romero",
                    TelefonoCliente = "987654321",
                    NombreVeterinario = VeterinarioActual,
                    CodigoCmvp = CmvpActual,
                    ServicioSolicitado = "Control Médico",
                    MontoEstimado = 40.00m
                }
            };

            _atenciones = new List<Atencion>
            {
                new Atencion
                {
                    AtencionId = 1,
                    CitaId = 98,
                    EmpleadoId = 1,
                    MascotaId = 1,
                    FechaHoraRegistro = hoy.AddDays(-60),
                    PesoActual = 15.2m,
                    Temperatura = 38.6m,
                    Sintomas = "Paciente acude por control rutinario y aplicación de antipulgas. Sin síntomas de alarma.",
                    Diagnostico = "Paciente clínicamente sano. Excelente condición corporal (3/5).",
                    TratamientoRecetado = "Aplicación tópica de Simparica Trio 10-20kg. Desparasitación interna con Fenbendazol vía oral por 3 días.",
                    Observaciones = "Mantener dieta equilibrada. Programar próximo control en 6 meses.",
                    MontoTotal = 65.00m,
                    NombreVeterinario = "Dr. Carlos Mendoza",
                    CodigoCmvp = "CMVP 08421",
                    NombreMascota = "Firulais",
                    ServicioRealizado = "Control Rutinario y Desparasitación"
                },
                new Atencion
                {
                    AtencionId = 2,
                    CitaId = 99,
                    EmpleadoId = 1,
                    MascotaId = 1,
                    FechaHoraRegistro = hoy.AddDays(-120),
                    PesoActual = 14.8m,
                    Temperatura = 38.9m,
                    Sintomas = "Dolor a la palpación en pabellón auricular derecho, sacudidas de cabeza constantes y secreción parduzca.",
                    Diagnostico = "Otitis externa eritemato-ceruminosa bilateral (más severa en oído derecho).",
                    TratamientoRecetado = "Limpieza ótica con solución fisiológica. Posatex gotas óticas: 4 gotas cada 24 hrs por 7 días. Meloxicam 0.1 mg/kg cada 24 hrs por 3 días.",
                    Observaciones = "Evitar entrada de agua al baño durante el tratamiento. Cita de revisión en 8 días.",
                    MontoTotal = 95.00m,
                    NombreVeterinario = "Dr. Carlos Mendoza",
                    CodigoCmvp = "CMVP 08421",
                    NombreMascota = "Firulais",
                    ServicioRealizado = "Consulta Especializada"
                },
                new Atencion
                {
                    AtencionId = 3,
                    CitaId = 104,
                    EmpleadoId = 1,
                    MascotaId = 4,
                    FechaHoraRegistro = hoy.AddDays(-1).AddHours(16).AddMinutes(0),
                    PesoActual = 6.8m,
                    Temperatura = 38.4m,
                    Sintomas = "Presenta eritema en zona inguinal y axilar con rascado frecuente de 4 días de evolución.",
                    Diagnostico = "Dermatitis alérgica por contacto estacional leve.",
                    TratamientoRecetado = "Apoquel 3.6 mg: 1/2 tableta cada 12 hrs por 5 días, luego cada 24 hrs por 5 días. Baño medicado con champú de clorhexidina 2 veces por semana.",
                    Observaciones = "Evitar contacto con césped húmedo recién fertilizado. Reevaluar en 10 días.",
                    MontoTotal = 110.00m,
                    NombreVeterinario = "Dr. Carlos Mendoza",
                    CodigoCmvp = "CMVP 08421",
                    NombreMascota = "Luna",
                    ServicioRealizado = "Dermatología Veterinaria"
                },
                new Atencion
                {
                    AtencionId = 4,
                    CitaId = 95,
                    EmpleadoId = 1,
                    MascotaId = 2,
                    FechaHoraRegistro = hoy.AddDays(-180),
                    PesoActual = 4.0m,
                    Temperatura = 38.2m,
                    Sintomas = "Chequeo felino preventivo semestral.",
                    Diagnostico = "Paciente en óptimo estado fisiológico.",
                    TratamientoRecetado = "Vacuna Rabia y desparasitación con Profender pipeta spot-on.",
                    Observaciones = "Monitorear consumo de agua en fuente.",
                    MontoTotal = 80.00m,
                    NombreVeterinario = "Dra. Elena Rojas",
                    CodigoCmvp = "CMVP 10255",
                    NombreMascota = "Michi",
                    ServicioRealizado = "Vacunación y Desparasitación"
                }
            };
        }

        public List<Cita> GetCitas() => _citas;

        public Cita? GetCitaById(int citaId) => _citas.FirstOrDefault(c => c.CitaId == citaId);

        public List<Mascota> GetMascotas() => _mascotas;

        public Mascota? GetMascotaById(int mascotaId) => _mascotas.FirstOrDefault(m => m.MascotaId == mascotaId);

        public List<Atencion> GetHistorialPorMascota(int mascotaId)
        {
            return _atenciones
                .Where(a => a.MascotaId == mascotaId)
                .OrderByDescending(a => a.FechaHoraRegistro)
                .ToList();
        }

        public void CambiarEstadoCita(int citaId, string nuevoEstado)
        {
            var cita = _citas.FirstOrDefault(c => c.CitaId == citaId);
            if (cita != null)
            {
                cita.Estado = nuevoEstado;
            }
        }

        public Atencion RegistrarAtencion(Atencion nuevaAtencion)
        {
            nuevaAtencion.AtencionId = _atenciones.Count > 0 ? _atenciones.Max(a => a.AtencionId) + 1 : 1;
            nuevaAtencion.FechaHoraRegistro = DateTime.Now;
            nuevaAtencion.EmpleadoId = EmpleadoIdActual;
            nuevaAtencion.NombreVeterinario = VeterinarioActual;
            nuevaAtencion.CodigoCmvp = CmvpActual;

            var mascota = GetMascotaById(nuevaAtencion.MascotaId);
            if (mascota != null)
            {
                nuevaAtencion.NombreMascota = mascota.Nombre;
                if (nuevaAtencion.PesoActual.HasValue)
                {
                    mascota.PesoReferencial = nuevaAtencion.PesoActual.Value;
                }
            }

            _atenciones.Insert(0, nuevaAtencion);

            if (nuevaAtencion.CitaId > 0)
            {
                CambiarEstadoCita(nuevaAtencion.CitaId, "ATENDIDA");
            }

            return nuevaAtencion;
        }
    }
}


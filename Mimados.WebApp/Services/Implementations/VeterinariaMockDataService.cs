using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services
{
    public class VeterinariaMockDataService
    {
        public int EmpleadoIdActual { get; set; }
        public string VeterinarioActual { get; set; } = string.Empty;
        public string CmvpActual { get; set; } = string.Empty;
        public string SedeActual { get; set; } = string.Empty;

        private List<Mascota> _mascotas = new();
        private List<Cita> _citas = new();
        private List<Atencion> _atenciones = new();

        public VeterinariaMockDataService()
        {
            InicializarDatos();
        }

        private void InicializarDatos()
        {
            _mascotas = new List<Mascota>();
            _citas = new List<Cita>();
            _atenciones = new List<Atencion>();
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

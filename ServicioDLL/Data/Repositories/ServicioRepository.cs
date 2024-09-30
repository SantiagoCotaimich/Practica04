using ServicioDLL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioDLL.Data.Repositories
{
    public class ServicioRepository : IServicioRepository
    {

        private ServiciosDBContext _context;

        public ServicioRepository(ServiciosDBContext context)
        {
            _context = context;
        }

        public void Create(TServicio servicio)
        {
            _context.TServicios.Add(servicio);
            _context.SaveChanges();
        }


        public bool Delete(int id)
        {
            var servicioDeleted = GetById(id);
            if (servicioDeleted != null)
            {
                _context.TServicios.Remove(servicioDeleted);

                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }


        public TServicio? GetById(int id)
        {
            return _context.TServicios.Find(id);
        }

        public List<TServicio> GetByNameCost(string? nombre = null, int? costo = null)
        {
            var query = _context.TServicios.AsQueryable();


            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(s => s.Nombre.Contains(nombre));
            }


            if (costo.HasValue)
            {
                query = query.Where(s => s.Costo == costo.Value);
            }


            return query.ToList();
        }



        public void Update(TServicio servicio)
        {
            if (servicio != null)
            {
                _context.TServicios.Update(servicio);

                _context.SaveChanges();
            }
        }
    }
}

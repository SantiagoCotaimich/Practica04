using ServicioDLL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioDLL.Data.Repositories
{
    public interface IServicioRepository
    {
        public void Create(TServicio servicio);
        void Update(TServicio servicio);
        TServicio? GetById(int id);
        List<TServicio> GetByNameCost(string? nombre, int? costo);
        public bool Delete(int id);
    }
}

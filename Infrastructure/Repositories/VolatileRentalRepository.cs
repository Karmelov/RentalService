using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using RentalAPI.Domain.Models;
using RentalAPI.Domain.Repositories;

namespace RentalAPI.Infrastructure.Repositories
{
    public class VolatileRentalRepository : IRentalRepository
    {
        Dictionary<int, Rental> Instances;

        public VolatileRentalRepository()
        {
            Instances = new Dictionary<int, Rental>();
        }

        public async Task<IEnumerable<Rental>> ListAllAsync()
        {
            List<Rental> result = Instances.Values.OrderBy(x => x.Id).ToList();
            return result;
        }

        public async Task<Rental> GetById(int id) {
            if (!Instances.ContainsKey(id))
            {
                return null;
            }
            return Instances[id];
        }
    }
}

using AutoMapper;
using RentalAPI.Domain.Models;
using RentalAPI.Resources;
using System.Linq;
using System.Threading.Tasks;

namespace RentalAPI.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<Rental, RentalStartResource>();

        }
    }
}

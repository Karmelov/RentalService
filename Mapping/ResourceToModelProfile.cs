using AutoMapper;
using RentalAPI.Domain.Models;
using RentalAPI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalAPI.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<CreateRentalResource, Rental>();
        }
    }
}

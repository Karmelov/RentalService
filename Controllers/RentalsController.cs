using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalAPI.Domain.Repositories;
using RentalAPI.Domain.Models;

namespace RentalAPI.Controllers
{
    [Route("/[controller]")]
    public class RentalsController : Controller
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalsController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            var rentals = await _rentalRepository.ListAllAsync();
            return rentals;
        }

        [HttpGet("{id}")]
        public async Task<Rental> GetById(int id)
        {
            var rental = await _rentalRepository.GetById(id);
            if (rental == null)
            {
                Response.StatusCode = 404;
            }
            return rental;
        }
    }
}
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParts.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryManager _repository;
        private IMapper _mapper;
        public CarsController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllOwners")]
        public async Task<IActionResult> GetAllOwners()
        {
            var cars = await _repository.Cars.GetAllCarsAsync();

            _logger.LogInfo($"Returned all cars from database.");
            _repository.Logs.AddLog(new Log { Type = "Info", Action = "Get list of owners", Message = $"Returned all cars from database" });
            await _repository.SaveAsync();
            var carsResult = _mapper.Map<IEnumerable<CarsDto>>(cars);

            return Ok(carsResult);
        }

        [HttpGet("{id}", Name = "CarById")]
        public async Task<IActionResult> GetCarById(Guid id)
        {
            var car = await _repository.Cars.GetCarByIdAsync(id);
            if (car == null)
            {
                _logger.LogWarn($"Company with id: {id} doesn't exist in the database.");
                _repository.Logs.AddLog(new Log { Type = "Warn", Action = "Get car by id", Message = $"Company with id: {id} doesn't exist in the database." });
                await _repository.SaveAsync();

                return NotFound();
            }
            else
            {
                var carDto = _mapper.Map<CarsDto>(car);
                return Ok(carDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody]CarForCreationDto car)
        {
            if(car == null)
            {
                _logger.LogError("CarForCreationDto object sent from client is null.");
                _repository.Logs.AddLog(new Log { Type = "Error", Action = "Create Car", Message = $"CarForCreationDto object sent from client is null." });
                await _repository.SaveAsync();

                return BadRequest("CarForCrationDto object is null");
            }

            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid car object sent from client.");
                _repository.Logs.AddLog(new Log { Type = "Error", Action = "Create Car", Message = $"Invalid car object sent from client." });
                await _repository.SaveAsync();

                return BadRequest("Invalid model object");
            }

            var carEntity = _mapper.Map<Cars>(car);

            _repository.Cars.CreateCar(carEntity);
            await _repository.SaveAsync();

            var createdCar = _mapper.Map<CarsDto>(carEntity);

            return CreatedAtRoute("CarById", new { id = createdCar.Id }, createdCar);
        }
    }
}

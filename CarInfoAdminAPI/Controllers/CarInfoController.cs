using CarInfo.DataAccess.Domain;
using CarInfo.DataAccess.Domain.Identity;
using CarInfo.DataAccess.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarInfoAdminAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class CarInfoController : ControllerBase
    {
        private ICarRepository _carRepository;
        public CarInfoController(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }
        [HttpGet("GetCars")]
        public async Task<List<CarDTO>> GetCarDetails()
        {
            var GetCarInfo = await _carRepository.GetCarList();
            return GetCarInfo;

        }
        [HttpGet("CarRefernceList")]
        public async Task<CarReferenceViewModel> GetCarRefernceList()
        {
            var carReference = await _carRepository.CarRefernceList();
            return carReference;

        }
        [HttpPost("AddCar")]
        public async Task<ActionResult> CreateListOfCar(CarDTO carDTO)
        {
            var GetCarInfo = await _carRepository.AddCarList(carDTO);
            return Ok(GetCarInfo);

        }
        [HttpGet("GetCarById")]
        public async Task<ActionResult> GetCarById(int id)
        {
            var GetCarInfo = await _carRepository.GetCarById(id);
            return Ok(GetCarInfo);

        }
        [HttpDelete("DeleteCarById")]
        public async Task<ActionResult> DeleteCarById(int id)
        {
            var GetCarInfo = await _carRepository.DeleteCarById(id);
            return Ok(GetCarInfo);

        }

        [HttpPut("UpdateCarDetails")]
        public async Task<ActionResult> UpdateCarDetails(int id,CarDTO carDTO)
        {
            var GetCarInfo = await _carRepository.UpdateCarDetails(id,carDTO);
            return Ok(GetCarInfo);

        }
    }
}

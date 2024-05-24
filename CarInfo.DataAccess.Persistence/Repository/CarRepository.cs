using CarInfo.DataAccess.Domain;
using CarInfo.DataAccess.Domain.Identity;
using CarInfo.DataAccess.Domain.Interfaces;
using CarInfo.DataAccess.Persistence.Exceptions;
using CarInfo.DataAccess.Persistence.Mapping;

namespace CarInfo.DataAccess.Persistence.Repository
{
    //repository responsible for the intraction with the cars relates data in the database with mapper data class access
    public class CarRepository : ICarRepository
    {
        private readonly MapperData _mapperData;
        public CarRepository(MapperData mapperData)
        {
            this._mapperData = mapperData;
        }

        /// <summary>
        /// Retrieved Car details
        /// </summary>
        /// <returns>the list of Car details</returns>
        public async Task<List<CarDTO>> GetCarList()
        {
            List<CarDTO> carDTOs = await _mapperData.CarInfoDetails();
            return carDTOs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<CarReferenceViewModel> CarRefernceList()
        {
            CarReferenceViewModel carReferenceView = new CarReferenceViewModel();

            carReferenceView = await _mapperData.GetCarReferenceDataList();
            return carReferenceView;
        }
        /// <summary>
        /// Add the car details 
        /// </summary>
        /// <param name="carDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<ResponseStatus> AddCarList(CarDTO carDTO)
        {
            ResponseStatus responseStatus = new ResponseStatus();
            var result = await _mapperData.AddListOfCar(carDTO);
            if (result)
            {
                responseStatus.statusCode = 1;
                responseStatus.message = "List of Car has been successfully inserted";
            }
            else
            {
                throw new NotFoundException(nameof(ResponseStatus), "Data not found", "");
            }
            return responseStatus;
        }
        /// <summary>
        /// retrived the particular car details based on Car Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<CarDTO> GetCarById(int id)
        {
            CarDTO carDTO = new CarDTO();
            var result = await _mapperData.GetCarById(id);
            if (result == null)
            {
                throw new NotFoundException(nameof(ResponseStatus), "Data not found", "");
            }
            return result;
        }

        /// <summary>
        /// Delete action perform on Car entity based on Car id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return the status of action of delete performance</returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<ResponseStatus> DeleteCarById(int id)
        {
            ResponseStatus responseStatus = new ResponseStatus();
            var data = await GetCarById(id);
            if (data == null)
            {
                responseStatus.status = "Error";
                responseStatus.message = "Data not found";
                responseStatus.statusCode = 1;
                throw new NotFoundException(nameof(ResponseStatus), responseStatus.message, "");
            }
            data.Id = id;
            responseStatus = await _mapperData.DeleteCarById(data);
            return responseStatus;
        }
        /// <summary>
        /// Update the particular Car details based on Car Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carDTO"></param>
        /// <returns>status after update</returns>
        /// <exception cref="NotFoundException"></exception>

        public async Task<ResponseStatus> UpdateCarDetails(int id, CarDTO carDTO)
        {
            ResponseStatus responseStatus = new ResponseStatus();
            var data = await GetCarById(id);
            if (data == null)
            {
                responseStatus.status = "Error";
                responseStatus.message = "Data has not update Succsessfully";
                responseStatus.statusCode = 0;
                throw new NotFoundException(nameof(ResponseStatus), responseStatus.message, "");
            }
            else
            {
                responseStatus = await _mapperData.UpdateCar(carDTO);
            }
            return responseStatus;
        }
    }
}

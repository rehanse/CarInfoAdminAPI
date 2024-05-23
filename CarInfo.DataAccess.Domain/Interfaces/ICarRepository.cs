using CarInfo.DataAccess.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.DataAccess.Domain.Interfaces
{
    public interface ICarRepository
    {
        Task<List<CarDTO>> GetCarList();
        Task<CarReferenceViewModel> CarRefernceList();
        Task<ResponseStatus> AddCarList(CarDTO carDTO);
        Task<CarDTO> GetCarById(int carId);
        Task<ResponseStatus> DeleteCarById(int carId);
        Task<ResponseStatus> UpdateCarDetails(int id,CarDTO carDTO);
    }
}

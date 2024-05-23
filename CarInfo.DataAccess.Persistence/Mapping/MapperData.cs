using CarInfo.DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CarInfo.DataAccess.Domain.Identity;
using CarInfo.DataAccess.Persistence.Exceptions;
namespace CarInfo.DataAccess.Persistence.Mapping
{
    public class MapperData
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        public MapperData(ApplicationDBContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            _mapper = mapper;

        }
        public async Task<List<CarDTO>> CarInfoDetails()
        {
            var response = (from cr in _dbContext.car
                            join mr in _dbContext.manufacturer on cr.manifactureId equals mr.id
                            join ct in _dbContext.carTransmissionTypes on cr.carTransmissionId equals ct.id
                            join ctype in _dbContext.carType on cr.typeId equals ctype.id
                            select new CarDTO
                            {
                                Id = cr.Id,
                                carname = cr.carname,
                                carImage = cr.carImage,
                                carModel = cr.model,
                                manifactureId = cr.manifactureId,
                                typeId = cr.typeId,
                                engine = cr.engine,
                                BHP = cr.BHP,
                                carTransmissionId = cr.carTransmissionId,
                                mileage = cr.mileage,
                                seat = cr.seat,
                                airBagDetails = cr.airBagDetails,
                                bootspace = cr.bootspace,
                                price = cr.price,
                                manufacturer = new Manufacturer
                                {
                                    id = mr.id,
                                    name = mr.name,
                                    contactPerson = mr.contactPerson,
                                    registeredOffice = mr.registeredOffice
                                },
                                carTransmissionType = new CarTransmissionType
                                {
                                    id = ct.id,
                                    name = ct.name
                                },
                                CarType = new CarType
                                {
                                    id = ctype.id,
                                    type = ctype.type
                                }
                            }).AsNoTracking().ToList();

            //_mapper.Map<List<CarDTO>>(cars);
            return response;
        }
        public async Task<bool> AddListOfCar(CarDTO carDTO)
        {
            try
            {
               var car =  _mapper.Map<Car>(carDTO);
              await  _dbContext.car.AddAsync(car);
              await  _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new NotFoundException(nameof(Car), "Data not found from database", ex.Message);
            }
        }
        public async Task<CarReferenceViewModel> GetCarReferenceDataList()
        {
            CarReferenceViewModel carReferenceView = new CarReferenceViewModel();
            carReferenceView.manufacturer = _dbContext.manufacturer.AsQueryable();
            carReferenceView.carType = _dbContext.carType.AsQueryable();
            carReferenceView.cartransmisionTypes = _dbContext.carTransmissionTypes.AsQueryable();
            return carReferenceView;
        }
        public async Task<CarDTO> GetCarById(int carId)
        {
            CarDTO carDTO = new CarDTO();
            var response = (from cr in _dbContext.car
                            join mr in _dbContext.manufacturer on cr.manifactureId equals mr.id
                            join ct in _dbContext.carTransmissionTypes on cr.carTransmissionId equals ct.id
                            join ctype in _dbContext.carType on cr.typeId equals ctype.id
                            select new CarDTO
                            {
                                Id = cr.Id,
                                carname = cr.carname,
                                carImage = cr.carImage,
                                carModel = cr.model,
                                manifactureId = cr.manifactureId,
                                typeId = cr.typeId,
                                engine = cr.engine,
                                BHP = cr.BHP,
                                carTransmissionId = cr.carTransmissionId,
                                mileage = cr.mileage,
                                seat = cr.seat,
                                airBagDetails = cr.airBagDetails,
                                bootspace = cr.bootspace,
                                price = cr.price,
                                manufacturer = new Manufacturer
                                {
                                    id = mr.id,
                                    name = mr.name,
                                    contactPerson = mr.contactPerson,
                                    registeredOffice = mr.registeredOffice
                                },
                                carTransmissionType = new CarTransmissionType
                                {
                                    id = ct.id,
                                    name = ct.name
                                },
                                CarType = new CarType
                                {
                                    id = ctype.id,
                                    type = ctype.type
                                }
                            }).AsNoTracking().ToList();

            //_mapper.Map<List<CarDTO>>(cars);
            carDTO = response.Where(x => x.Id == carId).FirstOrDefault();
            if (carDTO == null)
            {
                throw new NotFoundException(nameof(carDTO), "Data not found", "");
            }
            return carDTO;
        }

        public async Task<ResponseStatus> DeleteCarById(CarDTO carDTO)
        {
            var car = _mapper.Map<Car>(carDTO);
            _dbContext.car.Remove(car);
            _dbContext.SaveChanges();
            ResponseStatus responseStatus = new ResponseStatus();
            responseStatus.status = "Ok";
            responseStatus.message = "Delete Succsessfully";
            responseStatus.statusCode = 1;
            return responseStatus;
        }
        public async Task<ResponseStatus> UpdateCar(CarDTO carDTO)
        {
            ResponseStatus responseStatus = new ResponseStatus();
            try
            {
                var car = _mapper.Map<Car>(carDTO);
                _dbContext.car.Attach(car);
                _dbContext.Entry(car).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                responseStatus.status = "Ok";
                responseStatus.message = "Update Succsessfully";
                responseStatus.statusCode = 1;
            }
            catch(Exception ex)
            {
                throw new NotFoundException(nameof(Car), "Data not found from database", ex.Message);
            }
            return responseStatus;
        }

    }
}

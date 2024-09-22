using Microsoft.AspNetCore.Mvc;
using StoragesApi.Dto;
using StoragesApi.Repositories;

namespace StoragesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController: ControllerBase
    {
        private IStorageRepository _storageRepository;

        public StorageController(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        [HttpPost("AddStorage")]
        public ActionResult AddStorage(StorageDto storageDto)
        {
            _storageRepository.AddStorage(storageDto);
            return Ok();
        }

        [HttpGet("GetStorages")]
        public ActionResult<IEnumerable<StorageDto>> GetStorages()
        {
            return Ok(_storageRepository.GetStorages());
        }

        [HttpGet("CheckStorage")]
        public ActionResult<bool> CheckStorage(int storageId)
        {
            return Ok(_storageRepository.CheckStorage(storageId));
        }
    }
}

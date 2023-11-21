using AutoMapper;
using Challenge.DTO;
using Challenge.Entity;
using Challenge.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SupplierController(ISupplierService supplierService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllSuppliers")]
        [Authorize(Roles = "Supplier")]
        public IActionResult GetAllSuppliers()
        {
            try
            {
                List<Supplier> suppliers = _supplierService.GetAllSuppliers();
                List<SupplierDTO> supplierDTOs = _mapper.Map<List<SupplierDTO>>(suppliers);
                return StatusCode(200, supplierDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddSupplier")]
        [Authorize(Roles = "Supplier")] 
        public IActionResult AddSupplier([FromBody] SupplierDTO supplierDTO)
        {
            try
            {
                Supplier supplier = _mapper.Map<Supplier>(supplierDTO);
                _supplierService.AddSupplier(supplier);
                return StatusCode(200, supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateSupplier")]
        [Authorize(Roles = "Supplier")]
        public IActionResult UpdateSupplier([FromBody] SupplierDTO supplierDTO)
        {
            try
            {
                Supplier supplier = _mapper.Map<Supplier>(supplierDTO);
                _supplierService.UpdateSupplier(supplier);
                return StatusCode(200, supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteSupplier/{supplierId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSupplier(int supplierId)
        {
            try
            {
                _supplierService.DeleteSupplier(supplierId);
                return StatusCode(200, new JsonResult($"Supplier with Id {supplierId} is Deleted"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

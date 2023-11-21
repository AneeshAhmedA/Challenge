using AutoMapper;
using Challenge.DTO;
using Challenge.Entity;

namespace Challenge.Profiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<SupplierDTO, Supplier>();
        }
    }
}

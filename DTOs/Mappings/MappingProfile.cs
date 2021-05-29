using AutoMapper;
using DesafioAPI.Models;

namespace DesafioAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Client, UserRegisterDTO>().ReverseMap();
            CreateMap<ClientAddress, AddressDTO>().ReverseMap();
            CreateMap<SupplierAddress, AddressDTO>().ReverseMap();
            CreateMap<Supplier, SupplierOutDTO>().ReverseMap();
            CreateMap<Supplier, SupplierCreateDTO>().ReverseMap();
            CreateMap<Supplier, SupplierEditDTO>().ReverseMap();
            CreateMap<Stock, StockOutDTO>().ReverseMap();
        }
    }
}
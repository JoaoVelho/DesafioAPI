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
            CreateMap<PurchaseItem, ItemDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseOutDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseCreateDTO>().ReverseMap();
            CreateMap<Selling, SellingOutDTO>().ReverseMap();
            CreateMap<Selling, SellingCreateDTO>().ReverseMap();
            CreateMap<SellingItem, ItemDTO>().ReverseMap();
        }
    }
}
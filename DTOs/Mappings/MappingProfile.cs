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

            CreateMap<Supplier, SupplierOutDTO>().ReverseMap();
            CreateMap<Supplier, SupplierCreateDTO>().ReverseMap();
            CreateMap<Supplier, SupplierEditDTO>().ReverseMap();
            CreateMap<SupplierAddress, AddressDTO>().ReverseMap();

            CreateMap<Stock, StockOutDTO>().ReverseMap();

            CreateMap<Purchase, PurchaseOutDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseCreateDTO>().ReverseMap();
            CreateMap<PurchaseItem, ItemDTO>().ReverseMap();

            CreateMap<Selling, SellingOutDTO>().ReverseMap();
            CreateMap<Selling, SellingCreateDTO>().ReverseMap();
            CreateMap<SellingItem, ItemDTO>().ReverseMap();

            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductEditDTO>().ReverseMap();
        }
    }
}
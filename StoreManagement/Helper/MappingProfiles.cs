
using AutoMapper;
using APIStoreManagement.Dto;
using APIStoreManagement.Models;
using System.Diagnostics.Metrics;
using APIStoreManagement.DTOs;
using System.Drawing;
using APIStoreManagement.Repository;

namespace APIStoreManagement.Helper

{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Costs, CostDto>();
            CreateMap<CostDto, Costs>();
            CreateMap<Clothing, ClothingDto>();
            CreateMap<ClothingDto, Clothing>();

            //CreateMap<Order, OrderDto>()
            //   .ForMember(dest => dest.PatternName,
            //              opt => opt.MapFrom(src => src.Clothing.Pattern.Name)) 
            //   .ForMember(dest => dest.SizeName,     
            //              opt => opt.MapFrom(src => src.Clothing.Size.SizeName));
            //CreateMap<OrderDto, Order>()
            //   .ForMember(dest => dest.Id, opt => opt.Ignore()).ForMember(dest => dest.Clothing, opt => opt.Ignore());

            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.PatternName, opt => opt.MapFrom(src => src.Clothing.Pattern.Name))
            .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Clothing.Size.SizeName))
            .ForMember(dest => dest.ClothingId, opt => opt.MapFrom(src => src.ClothingId))
            .ForMember(dest => dest.IsDelivered, opt => opt.MapFrom(src => src.IsDelivered));

            // مپ معکوس OrderDto به Order (برای ارسال اطلاعات به API)
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Clothing, opt => opt.Ignore()) // نادیده گرفتن آبجکت Clothing
                .ForMember(dest => dest.ClothingId, opt => opt.MapFrom(src => src.ClothingId)) // ClothingId را مپ میکنیم
                .ForMember(dest => dest.IsDelivered, opt => opt.MapFrom(src => src.IsDelivered));


            CreateMap<Sale, SalesDto>().ForMember(dest => dest.PatternName, opt => opt.MapFrom(src => src.Clothing.Pattern.Name))
            .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Clothing.Size.SizeName))
            .ForMember(dest => dest.SoldPrice, opt => opt.MapFrom(src => src.Soldprice)); 
            CreateMap<SalesDto, Sale>().ForMember(dest => dest.Soldprice, opt => opt.MapFrom(src => src.SoldPrice))
            .ForMember(dest => dest.Clothing, opt => opt.Ignore()); 




            CreateMap<Pattern, PatternDto>();
            CreateMap<PatternDto, Pattern>();
            CreateMap<Sizes, SizesDto>();
            CreateMap<SizesDto, Sizes>();

            CreateMap<OrderCreateDto, Order>();

            CreateMap<Inventory, InventoryDto>()
                            .ForMember(dest => dest.PatternName, opt => opt.MapFrom(src => src.Clothing.Pattern.Name))
                            .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Clothing.Size.SizeName));
            CreateMap<InventoryCreateUpdateDto, Inventory>();

            // مطمئن شوید که Mapping های مربوط به Clothing, Size و Pattern هم وجود دارند
            CreateMap<Clothing, ClothingDto>()
                .ForMember(dest => dest.PatternName, opt => opt.MapFrom(src => src.Pattern.Name))
                .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size.SizeName));
            CreateMap<ClothingRepository, Clothing>();
        }
    }
}

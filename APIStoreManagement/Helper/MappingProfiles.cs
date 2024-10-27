
using AutoMapper;
using APIStoreManagement.Dto;
using APIStoreManagement.Models;
using System.Diagnostics.Metrics;


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
            CreateMap<Inventory, InventoryDto>();
            CreateMap<InventoryDto, Inventory>();
            CreateMap<Orders, OrderDto>();
            CreateMap<OrderDto, Orders>();
            CreateMap<Sales, SalesDto>();
            CreateMap<SalesDto, Sales>();
            CreateMap<Pattern, Dto.PatternDto>();
            CreateMap<Dto.PatternDto, Pattern>();
            CreateMap<Sizes, SizesDto>();
            CreateMap<SizesDto, Sizes>();

        }
    }
}

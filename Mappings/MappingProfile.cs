using AutoMapper;
using WayFinder.Areas.Admin.Models;
using WayFinder.Models;

namespace WayFinder.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Resident, ResidentEditModel>();
        CreateMap<Resident, ResidentCreateModel>();
        
        CreateMap<Company, CompanyCreateModel>();
        CreateMap<Company, CompanyCreateModel>().ReverseMap();
        CreateMap<Company, CompanyEditModel>();
        CreateMap<Company, CompanyEditModel>().ReverseMap();
        
        CreateMap<Level, LevelCreateModel>();
        CreateMap<Level, LevelCreateModel>().ReverseMap();
        CreateMap<Level, LevelEditModel>();
        CreateMap<Level, LevelEditModel>().ReverseMap();
        
        CreateMap<Location, LocationCreateModel>();
        CreateMap<Location, LocationCreateModel>().ReverseMap();
        CreateMap<Location, LocationEditModel>();
        CreateMap<Location, LocationEditModel>().ReverseMap();
        
        CreateMap<Event, EventCreateModel>();
        CreateMap<Event, EventCreateModel>().ReverseMap();
        CreateMap<Event, EventEditModel>();
        CreateMap<Event, EventEditModel>().ReverseMap();
    }
}

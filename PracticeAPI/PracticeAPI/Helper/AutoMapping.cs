using AutoMapper;

namespace PracticeAPI.Helper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<PracticeAPI.Helper.Models.Employee,   PracticeAPI.BLL.Models.Employee>().ReverseMap();
            CreateMap<PracticeAPI.Helper.Models.Department, PracticeAPI.BLL.Models.Department>().ReverseMap();
            CreateMap<PracticeAPI.Helper.Models.Pagination, PracticeAPI.BLL.Models.Pagination>().ReverseMap();
            CreateMap<PracticeAPI.BLL.Models.Employee,   PracticeAPI.DLL.Models.Employee> ().ReverseMap();
            CreateMap<PracticeAPI.BLL.Models.Department, PracticeAPI.DLL.Models.Department> ().ReverseMap();
            CreateMap<PracticeAPI.BLL.Models.Pagination, PracticeAPI.DLL.Models.Pagination>().ReverseMap();
        }
    }
}

using AutoMapper;
using mod = ParameterControl.Models;
using ParameterControl.Policy.Entities;

namespace ParameterControl.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<List<mod.Policy.Policy>, List<PolicyModel>>()
                .ReverseMap();
        }
    }
}

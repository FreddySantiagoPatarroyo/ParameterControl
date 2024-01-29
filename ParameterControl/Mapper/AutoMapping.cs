using AutoMapper;
using mod = ParameterControl.Models;
using ParameterControl.Policy.Entities;

namespace ParameterControl.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<mod.Policy.Policy, PolicyModel>()
                .ReverseMap();
        }
    }
}

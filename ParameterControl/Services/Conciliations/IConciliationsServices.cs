﻿using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;

namespace ParameterControl.Services.Conciliations
{
    public interface IConciliationsServices
    {
        Task<List<string>> GetPolicies();
        Task<List<string>> GetRequired();
        Task<List<Conciliation>> GetConciliations();
        Task<Conciliation> GetConciliationsById(string id);
        Task<List<Conciliation>> GetFilterConciliations(FilterViewModel filterModel);
    }
}

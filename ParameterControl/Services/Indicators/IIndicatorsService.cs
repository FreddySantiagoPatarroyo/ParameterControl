﻿using ParameterControl.Models.Indicator;
using ParameterControl.Models.Filter;

namespace ParameterControl.Services.Indicators
{
    public interface IIndicatorsService
    {
        Task<List<Indicator>> GetIndicators();
        Task<Indicator> GetIndicatorsById(string id);
        Task<List<Indicator>> GetFilterIndicators(FilterViewModel filterModel);
    }
}
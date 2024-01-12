﻿using ParameterControl.Models.Indicator;
using ParameterControl.Models.Filter;

namespace ParameterControl.Services.Indicators
{
    public class IndicatorsService : IIndicatorsService
    {
        private List<Indicator> indicators = new List<Indicator>();
        public IndicatorsService()
        {
            indicators = new List<Indicator>()
            {
                new Indicator(){
                    Id = "1",
                    Name = "Indicador_001",
                    Description = "ejemplo",  
                    Formula = "FormulaEjemplo",  
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = false
                },
                new Indicator(){
                    Id = "2",
                    Name = "Indicador_002",
                    Description = "ejemplo",
                    Formula = "FormulaEjemplo",
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = true
                },
                new Indicator(){
                    Id = "3",
                    Name = "Indicador_003",
                    Description = "ejemplo",
                    Formula = "FormulaEjemplo",
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = true
                },
                new Indicator(){
                    Id = "4",
                    Name = "Indicador_004",
                    Description = "ejemplo",
                    Formula = "FormulaEjemplo",
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = false
                },
                new Indicator(){
                    Id = "5",
                    Name = "Indicador_005",
                    Description = "ejemplo",
                    Formula = "FormulaEjemplo",
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = true
                },
                new Indicator(){
                    Id = "6",
                    Name = "Indicador_006",
                    Description = "ejemplo",
                    Formula = "FormulaEjemplo",
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = false
                },

            };
        }

        public async Task<List<Indicator>> GetIndicators()
        {
            return indicators;
        }

        public async Task<Indicator> GetIndicatorsById(string id)
        {
            Indicator indicator = indicators.Find(indicator => indicator.Id == id);
            return indicator;
        }
               public async Task<List<Indicator>> GetFilterIndicators(FilterViewModel filterModel)
        {
            List<Indicator> IndicatorsFilter = new List<Indicator>();

            if ((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                IndicatorsFilter = indicators;
            }
            else
            {
                switch (filterModel.ColumValue)
                {
                    case "Code":
                        IndicatorsFilter = applyFilter(filterModel);
                        break;
                    case "Name":
                        IndicatorsFilter = applyFilter(filterModel);
                        break;
                    case "Description":
                        IndicatorsFilter = applyFilter(filterModel);
                        break;
                    case "Conciliation_":
                        IndicatorsFilter = applyFilter(filterModel);
                        break;
                    case "Required":
                        IndicatorsFilter = applyFilter(filterModel);
                        break;
                    default:
                        break;
                }
            }

            return IndicatorsFilter;
        }

        private List<Indicator> applyFilter(FilterViewModel filterModel)
        {
            var property = typeof(Indicator).GetProperty(filterModel.ColumValue);

            List<Indicator> IndicatorsFilter = new List<Indicator>();

            foreach (Indicator Indicator in indicators)
            {
                if (property.GetValue(Indicator).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                {
                    IndicatorsFilter.Add(Indicator);
                }
            }

            return IndicatorsFilter;
        }
    }
}

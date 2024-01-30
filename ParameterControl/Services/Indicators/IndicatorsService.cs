
﻿using ParameterControl.Models.Indicator;
using ParameterControl.Models.Filter;
using modIndicator = ParameterControl.Models.Indicator;

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
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Indicator(){
                    Id = "2",
                    Name = "Indicador_002",
                    Description = "ejemplo",
                    Formula = "FormulaEjemplo",
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Indicator(){
                    Id = "3",
                    Name = "Indicador_003",
                    Description = "ejemplo",
                    Formula = "FormulaEjemplo",
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Indicator(){
                    Id = "4",
                    Name = "Indicador_004",
                    Description = "ejemplo",
                    Formula = "FormulaEjemplo",
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Indicator(){
                    Id = "5",
                    Name = "Indicador_005",
                    Description = "ejemplo",
                    Formula = "FormulaEjemplo",
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Indicator(){
                    Id = "6",
                    Name = "Indicador_006",
                    Description = "ejemplo",
                    Formula = "FormulaEjemplo",
                    Scenery = "EscenarioEjemplo",
                    Parameter = "ParametroEjemplo",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },

            };
        }

        public async Task<List<Indicator>> GetIndicators()
        {
            return indicators;
        }

        public async Task<List<IndicatorViewModel>> GetindicatorsFormat(List<modIndicator.Indicator> indicators)
        {
            List<IndicatorViewModel> IndicatorsModel = new List<IndicatorViewModel>();

            foreach (modIndicator.Indicator indicator in indicators)
            {
                IndicatorViewModel indicatorModel = new IndicatorViewModel();

                indicatorModel.Id = indicator.Id;
                indicatorModel.Name = indicator.Name;
                indicatorModel.Description = indicator.Description;
                indicatorModel.Formula = indicator.Formula;
                indicatorModel.Scenery = indicator.Scenery;
                indicatorModel.Parameter = indicator.Parameter;
                indicatorModel.State = indicator.State;
                indicatorModel.StateFormat = indicator.State ? "Activo" : "Inactivo";
                indicatorModel.CreationDate = indicator.CreationDate;
                indicatorModel.UpdateDate = indicator.UpdateDate;
                indicatorModel.CreationDateFormat = indicator.CreationDate.ToString("dd/MM/yyyy");
                indicatorModel.UpdateDateFormat = indicator.UpdateDate.ToString("dd/MM/yyyy");

                IndicatorsModel.Add(indicatorModel);
            }

            return IndicatorsModel;
        }

        public async Task<Indicator> GetIndicatorsById(string id)
        {
            Indicator indicator = indicators.Find(indicator => indicator.Id == id);
            return indicator;
        }

        public async Task<List<IndicatorViewModel>> GetFilterIndicators(FilterViewModel filterModel)
        {
            List<modIndicator.Indicator> allIndicators = await GetIndicators();
            List<IndicatorViewModel> IndicatorsFilter = await GetindicatorsFormat(allIndicators);

            if (filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == "")
            {
                return IndicatorsFilter;
            }
            else
            {
                IndicatorsFilter = await applyFilter(filterModel, IndicatorsFilter);
            }

            return IndicatorsFilter;
        }

        private async Task<List<IndicatorViewModel>> applyFilter(FilterViewModel filterModel, List<IndicatorViewModel> allIndicators)
        {
            Console.WriteLine(filterModel.TypeRow.ToString());

            var property = typeof(IndicatorViewModel).GetProperty(filterModel.ColumValue);

            List<IndicatorViewModel> IndicatorsFilter = new List<IndicatorViewModel>();
            if (filterModel.TypeRow == "Select")
            {
                foreach (IndicatorViewModel indicator in allIndicators)
                {
                    if (property.GetValue(indicator).ToString().ToUpper() == filterModel.ValueFilter.ToUpper())
                    {
                        IndicatorsFilter.Add(indicator);
                    }
                }
            }
            else
            {
                foreach (IndicatorViewModel indicator in allIndicators)
                {
                    if (property.GetValue(indicator).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                    {
                        IndicatorsFilter.Add(indicator);
                    }
                }
            }

            return IndicatorsFilter;
        }
    }
}

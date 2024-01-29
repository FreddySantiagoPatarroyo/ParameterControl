using ParameterControl.Models.Filter;
using ParameterControl.Models.Indicator;
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
                    UserOwner = 1
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
                    UserOwner = 1
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
                    UserOwner = 1
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
                    UserOwner = 1
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
                    UserOwner = 1
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
                    UserOwner = 1
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
            List<Indicator> IndicatorsFilter = new List<Indicator>();

            if ((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                IndicatorsFilter = await GetIndicators();
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

            return await GetindicatorsFormat(IndicatorsFilter);
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

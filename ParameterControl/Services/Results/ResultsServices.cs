using ParameterControl.Models.Filter;
using ParameterControl.Models.Result;
using modResult = ParameterControl.Models.Result;

namespace ParameterControl.Services.Results
{
    public class ResultsServices : IResultsServices
    {
        private List<Result> results = new List<Result>();
        public ResultsServices()
        {
            results = new List<Result>()
            {
                new Result(){
                    Id = "1",
                    Conciliation = "CO_CONCILIACIÓN",
                    Scenery = "CO_ESCENARIO",
                    Status = "OK",
                    StartDate = DateTime.Parse("2024-01-10"),
                    EndDate = DateTime.Parse("2024-01-10"),
                    BeneValue = "2000",
                    IncoValue = "78888",
                    PQValue = "789",
                    ReinValue = "6444",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                 new Result(){
                    Id = "2",
                    Conciliation = "CO_CONCILIACIÓN_1",
                    Scenery = "CO_ESCENARIO",
                    Status = "OK",
                    StartDate = DateTime.Parse("2024-01-10"),
                    EndDate = DateTime.Parse("2024-01-10"),
                    BeneValue = "2000",
                    IncoValue = "78888",
                    PQValue = "789",
                    ReinValue = "6444",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                  new Result(){
                    Id = "3",
                    Conciliation = "CO_CONCILIACIÓN",
                    Scenery = "CO_ESCENARIO",
                    Status = "OK",
                    StartDate = DateTime.Parse("2024-01-10"),
                    EndDate = DateTime.Parse("2024-01-10"),
                    BeneValue = "2000",
                    IncoValue = "78888",
                    PQValue = "789",
                    ReinValue = "6444",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                   new Result(){
                    Id = "4",
                    Conciliation = "CO_CONCILIACIÓN_1",
                    Scenery = "CO_ESCENARIO",
                    Status = "OK",
                    StartDate = DateTime.Parse("2024-01-10"),
                    EndDate = DateTime.Parse("2024-01-10"),
                    BeneValue = "2000",
                    IncoValue = "78888",
                    PQValue = "789",
                    ReinValue = "6444",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },

            };
        }

        public async Task<List<Result>> GetResults()
        {
            return results;
        }

        public async Task<List<ResultViewModel>> GetResultsFormat(List<modResult.Result> results)
        {
            List<ResultViewModel> ResultsModel = new List<ResultViewModel>();

            foreach (modResult.Result result in results)
            {
                ResultViewModel resultModel = new ResultViewModel();

                resultModel.Id = result.Id;
                resultModel.Conciliation = result.Conciliation;
                resultModel.Scenery = result.Scenery;
                resultModel.Status = result.Status;
                resultModel.BeneValue = result.BeneValue;
                resultModel.IncoValue = result.IncoValue;
                resultModel.PQValue = result.PQValue;
                resultModel.ReinValue = result.ReinValue;
                resultModel.StartDate = result.StartDate;
                resultModel.EndDate = result.EndDate;
                resultModel.State = result.State;
                resultModel.StateFormat = result.State ? "Activo" : "Inactivo";
                resultModel.CreationDate = result.CreationDate;
                resultModel.UpdateDate = result.UpdateDate;

                ResultsModel.Add(resultModel);
            }

            return ResultsModel;
        }

        public async Task<Result> GetResultsById(string id)
        {
            Result result = results.Find(results => results.Id == id);
            return result;
        }

        public async Task<List<ResultViewModel>> GetFilterResults(FilterViewModel filterModel)
        {
            List<Result> ResultsFilter = new List<Result>();

            if ((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                ResultsFilter = results;
            }
            else
            {
                switch (filterModel.ColumValue)
                {
                    case "Conciliation":
                        ResultsFilter = applyFilter(filterModel);
                        break;
                    case "Name":
                        ResultsFilter = applyFilter(filterModel);
                        break;
                    case "Description":
                        ResultsFilter = applyFilter(filterModel);
                        break;
                    case "Conciliation_":
                        ResultsFilter = applyFilter(filterModel);
                        break;
                    case "Required":
                        ResultsFilter = applyFilter(filterModel);
                        break;
                    default:
                        break;
                }
            }

            return await GetResultsFormat(ResultsFilter);
        }

        private List<Result> applyFilter(FilterViewModel filterModel)
        {
            var property = typeof(Result).GetProperty(filterModel.ColumValue);

            List<Result> ResultsFilter = new List<Result>();

            foreach (Result Result in results)
            {
                if (property.GetValue(Result).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                {
                    ResultsFilter.Add(Result);
                }
            }

            return ResultsFilter;
        }


    }

}


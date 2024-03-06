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
                    UpdateDate = DateTime.Parse("2023-11-09")
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
                    UpdateDate = DateTime.Parse("2023-11-09")
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
                    UpdateDate = DateTime.Parse("2023-11-09")
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
                    UpdateDate = DateTime.Parse("2023-11-09")
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
                resultModel.CreationDateFormat = result.CreationDate.ToString("dd/MM/yyyy");
                resultModel.UpdateDateFormat = result.UpdateDate.ToString("dd/MM/yyyy");
                resultModel.StartDateFormat = result.StartDate.ToString("dd/MM/yyyy");
                resultModel.EndDateFormat = result.EndDate.ToString("dd/MM/yyyy");

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
            List<modResult.Result> allResults = await GetResults();
            List<ResultViewModel> resultsFilter = await GetResultsFormat(allResults);

            if (filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == "")
            {
                return resultsFilter;
            }
            else
            {
                resultsFilter = await applyFilter(filterModel, resultsFilter);
            }

            return resultsFilter;
        }

        private async Task<List<ResultViewModel>> applyFilter(FilterViewModel filterModel, List<ResultViewModel> allResults)
        {
            var property = typeof(ResultViewModel).GetProperty(filterModel.ColumValue);

            List<ResultViewModel> resultsFilter = new List<ResultViewModel>();
            if (filterModel.TypeRow == "Select")
            {
                foreach (ResultViewModel result in allResults)
                {
                    if (property.GetValue(result).ToString().ToUpper() == filterModel.ValueFilter.ToUpper())
                    {
                        resultsFilter.Add(result);
                    }
                }
            }
            else
            {
                foreach (ResultViewModel result in allResults)
                {
                    if (property.GetValue(result).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                    {
                        resultsFilter.Add(result);
                    }
                }
            }
            return resultsFilter;
        }
    }
}


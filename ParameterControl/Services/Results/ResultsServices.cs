using ParameterControl.Models.Result;
using ParameterControl.Models.Filter;



namespace ParameterControl.Services.Results
{
    public class ResultsServices: IResultsServices
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
                    State = true
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
                    State = false
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
                    State = true
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
                    State = false
                },

            };
        }

        public async Task<List<Result>> GetResults()
        {
            return results;
        }

        public async Task<Result> GetResultsById(string id)
        {
            Result result = results.Find(results => results.Id == id);
            return result;
        }
       
        public async Task<List<Result>> GetFilterResults(FilterViewModel filterModel)
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

            return ResultsFilter;
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


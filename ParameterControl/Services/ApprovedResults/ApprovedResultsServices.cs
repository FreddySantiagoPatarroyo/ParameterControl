using ParameterControl.Models.ApprovedResult;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Result;
using modApprovedResult = ParameterControl.Models.ApprovedResult;

namespace ParameterControl.Services.ApprovedResults
{
    public class ApprovedResultsServices: IApprovedResultsServices
    {
        private List<ApprovedResult> ApprovedResults = new List<ApprovedResult>();

        public ApprovedResultsServices() 
        {
            ApprovedResults = new List<ApprovedResult>()
            {
                new ApprovedResult(){
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
                    UserOwner = "User1"
                },
                 new ApprovedResult(){
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
                    UserOwner = "User1"
                },
                  new ApprovedResult(){
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
                    UserOwner = "User1"
                },
                   new ApprovedResult(){
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
                    UserOwner = "User1"
                },
            };
        }

        public async Task<List<ApprovedResult>> GetApprovedResults()
        {
            return ApprovedResults;
        }

        public async Task<List<ApprovedResultViewModel>> GetApprovedResultsFormat(List<modApprovedResult.ApprovedResult> approvedResults)
        {
            List<ApprovedResultViewModel> ResultsModel = new List<ApprovedResultViewModel>();

            foreach (modApprovedResult.ApprovedResult approvedResult in approvedResults)
            {
                ApprovedResultViewModel approvedResultModel = new ApprovedResultViewModel();
                approvedResultModel.Id = approvedResult.Id;
                approvedResultModel.Conciliation = approvedResult.Conciliation;
                approvedResultModel.Scenery = approvedResult.Scenery;
                approvedResultModel.Status = approvedResult.Status;
                approvedResultModel.BeneValue = approvedResult.BeneValue;
                approvedResultModel.IncoValue = approvedResult.IncoValue;
                approvedResultModel.PQValue = approvedResult.PQValue;
                approvedResultModel.ReinValue = approvedResult.ReinValue;
                approvedResultModel.StartDate = approvedResult.StartDate;
                approvedResultModel.EndDate = approvedResult.EndDate;
                approvedResultModel.State = approvedResult.State;
                approvedResultModel.StateFormat = approvedResult.State ? "Activo" : "Inactivo";
                approvedResultModel.CreationDate = approvedResult.CreationDate;
                approvedResultModel.UpdateDate = approvedResult.UpdateDate;
                approvedResultModel.CreationDateFormat = approvedResult.CreationDate.ToString("dd/MM/yyyy");
                approvedResultModel.UpdateDateFormat = approvedResult.UpdateDate.ToString("dd/MM/yyyy");
                approvedResultModel.StartDateFormat = approvedResult.StartDate.ToString("dd/MM/yyyy");
                approvedResultModel.EndDateFormat = approvedResult.EndDate.ToString("dd/MM/yyyy");

                ResultsModel.Add(approvedResultModel);
            }

            return ResultsModel;
        }

        public async Task<ApprovedResult> GetApprovedResultsById(string id)
        {
            ApprovedResult approvedResult = ApprovedResults.Find(approvedResults => approvedResults.Id == id);
            return approvedResult;
        }

        public async Task<List<ApprovedResultViewModel>> GetFilterApprovedResults(FilterViewModel filterModel)
        {
            List<modApprovedResult.ApprovedResult> allApprovedResults = await GetApprovedResults();
            List<ApprovedResultViewModel> approvedResultsFilter = await GetApprovedResultsFormat(allApprovedResults);

            if (filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == "")
            {
                return approvedResultsFilter;
            }
            else
            {
                approvedResultsFilter = await applyFilter(filterModel, approvedResultsFilter);
            }

            return approvedResultsFilter;
        }

        private async Task<List<ApprovedResultViewModel>> applyFilter(FilterViewModel filterModel, List<ApprovedResultViewModel> allApprovedResults)
        {
            var property = typeof(ApprovedResultViewModel).GetProperty(filterModel.ColumValue);

            List<ApprovedResultViewModel> approvedResultsFilter = new List<ApprovedResultViewModel>();
            if (filterModel.TypeRow == "Select")
            {
                foreach (ApprovedResultViewModel approvedResult in allApprovedResults)
                {
                    if (property.GetValue(approvedResult).ToString().ToUpper() == filterModel.ValueFilter.ToUpper())
                    {
                        approvedResultsFilter.Add(approvedResult);
                    }
                }
            }
            else
            {
                foreach (ApprovedResultViewModel approvedResult in allApprovedResults)
                {
                    if (property.GetValue(approvedResult).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                    {
                        approvedResultsFilter.Add(approvedResult);
                    }
                }
            }
            return approvedResultsFilter;
        }
    }
}

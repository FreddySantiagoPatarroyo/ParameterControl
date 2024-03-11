using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Result;
using ParameterControl.Policy.Entities;
using ParameterControl.Policy.Impl;
using ParameterControl.Policy.Interfaces;
using ParameterControl.Stage.Entities;
using ParameterControl.Stage.Impl;
using ParameterControl.Stage.Interfaces;
using modResult = ParameterControl.Models.Result;

namespace ParameterControl.Services.Results
{
    public class ResultsServices : IResultsServices
    {
        private List<Result> results = new List<Result>();
        private readonly IStageService _resultService;

        public ResultsServices(IConfiguration configuration)
        {
            _resultService = new StageService(configuration);
            results = new List<Result>()
            {
                new Result(){
                    Conciliation = "CO_CONCILIACIÓN",
                    Scenery = "CO_ESCENARIO",
                    StatusConciliation = "OK",
                    UploadDate = DateTime.Parse("2024-01-10"),
                    ValueBeneficiary = "2000",
                    ValueInconsistency = "78888",
                    ValuePqr = "789",
                    ValueRepetition = "6444"
                },
                 new Result(){
                    Conciliation = "CO_CONCILIACIÓN",
                    Scenery = "CO_ESCENARIO",
                    StatusConciliation = "OK",
                    UploadDate = DateTime.Parse("2024-01-10"),
                    ValueBeneficiary = "2000",
                    ValueInconsistency = "78888",
                    ValuePqr = "789",
                    ValueRepetition = "6444"
                },
                  new Result(){
                   Conciliation = "CO_CONCILIACIÓN",
                    Scenery = "CO_ESCENARIO",
                    StatusConciliation = "OK",
                    UploadDate = DateTime.Parse("2024-01-10"),
                    ValueBeneficiary = "2000",
                    ValueInconsistency = "78888",
                    ValuePqr = "789",
                    ValueRepetition = "6444"
                }
            };
        }

        public async Task<List<modResult.Result>> GetResults()
        {
            var collectionResults = await _resultService.SelectAllSummaryStage();
            var response = await MapperResults(collectionResults);
            return response;
        }

        public async Task<int> CountResults()
        {
            var collectionResults = await _resultService.SelectAllSummaryStage();
            return collectionResults.Count();
        }

        public async Task<List<modResult.Result>> GetResultsPagination(PaginationViewModel pagination)
        {
            try
            {
                var response = await _resultService.SelectPaginatorSummaryStage(pagination.Page, pagination.RecordsPage);
                var result = await MapperResults(response);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ResultViewModel>> GetResultsFormat(List<modResult.Result> results)
        {
            List<ResultViewModel> ResultsModel = new List<ResultViewModel>();

            foreach (modResult.Result result in results)
            {
                ResultViewModel resultModel = new ResultViewModel();
                resultModel.Conciliation = result.Conciliation;
                resultModel.Scenery = result.Scenery;
                resultModel.StatusConciliation = result.StatusConciliation;
                resultModel.ValueBeneficiary = result.ValueBeneficiary;
                resultModel.ValueInconsistency = result.ValueInconsistency;
                resultModel.ValuePqr = result.ValuePqr;
                resultModel.ValueRepetition = result.ValueRepetition;
                resultModel.UploadDate = result.UploadDate;
                resultModel.UploadDateFormat = result.UploadDate.ToString("dd/MM/yyyy");

                ResultsModel.Add(resultModel);
            }

            return ResultsModel;
        }

        //public async Task<Result> GetResultsById(string code)
        //{
        //    Result result = results.Find(results => results.code == code);
        //    return result;
        //}

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

        public List<ResultViewModel> GetFilterPagination(List<ResultViewModel> inicialResults, PaginationViewModel paginationViewModel, int totalData)
        {
            var limit = paginationViewModel.Page * paginationViewModel.RecordsPage;
            var index = limit - paginationViewModel.RecordsPage;
            var count = 0;

            if (limit > totalData)
            {
                count = totalData - index;
            }
            else
            {
                count = paginationViewModel.RecordsPage;
            }

            List<ResultViewModel> resultsFilterPagination = inicialResults.GetRange(index, count);

            return resultsFilterPagination;
        }

        private async Task<List<modResult.Result>> MapperResults(List<StageSummaryModel> stageSummaryModel)
        {
            return await Task.Run(() =>
            {
                List<modResult.Result> results = new List<modResult.Result>();
                if (stageSummaryModel.Count > 0)
                {
                    foreach (var stageSummary in stageSummaryModel)
                    {
                        results.Add(MapperToResult(stageSummary).Result);
                    }
                }
                return results;
            });
        }

        private async Task<modResult.Result> MapperToResult(StageSummaryModel stageSummaryModel)
        {
            return await Task.Run(() =>
            {
                modResult.Result model = new modResult.Result
                {
                    Conciliation = stageSummaryModel.ConciliarionCode,
                    Scenery = stageSummaryModel.StageCode,
                    StatusConciliation = stageSummaryModel.StatusConciliation,
                    UploadDate = stageSummaryModel.UploadDate,
                    ValueBeneficiary = stageSummaryModel.ValueBeneficiary,
                    ValueInconsistency = stageSummaryModel.ValueInconsistency,
                    ValuePqr = stageSummaryModel.ValuePqr,
                    ValueRepetition = stageSummaryModel.ValueRepetition
                };
                return model;
            });
        }
    }
}


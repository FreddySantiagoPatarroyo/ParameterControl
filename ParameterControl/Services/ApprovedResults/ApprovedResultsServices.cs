using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.ApprovedResult;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Result;
using ParameterControl.Stage.Entities;
using ParameterControl.Stage.Impl;
using ParameterControl.Stage.Interfaces;
using System.Configuration;
using modApprovedResult = ParameterControl.Models.ApprovedResult;

namespace ParameterControl.Services.ApprovedResults
{
    public class ApprovedResultsServices : IApprovedResultsServices
    {
        private List<ApprovedResult> ApprovedResults = new List<ApprovedResult>();
        private readonly IStageService _approvedResultService;

        public ApprovedResultsServices(IConfiguration configuration)
        {
            _approvedResultService = new StageService(configuration);
            ApprovedResults = new List<ApprovedResult>()
            {
                new ApprovedResult(){
                    Conciliation = "CO_CONCILIACIÓN",
                    Scenery = "CO_ESCENARIO",
                    StatusConciliation = "OK",
                    UploadDate = DateTime.Parse("2024-01-10"),
                    ValueBeneficiary = "2000",
                    ValueInconsistency = "78888",
                    ValuePqr = "789",
                    ValueRepetition = "6444"
                },
                 new ApprovedResult(){
                    Conciliation = "CO_CONCILIACIÓN",
                    Scenery = "CO_ESCENARIO",
                    StatusConciliation = "OK",
                    UploadDate = DateTime.Parse("2024-01-10"),
                    ValueBeneficiary = "2000",
                    ValueInconsistency = "78888",
                    ValuePqr = "789",
                    ValueRepetition = "6444"
                },
                  new ApprovedResult(){
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

        public async Task<List<ApprovedResult>> GetApprovedResults()
        {
            var collectionApprovedResults = await _approvedResultService.SelectAllSummaryStage();
            var response = await MapperApprovedResults(collectionApprovedResults);
            return response;
        }

        public async Task<int> CountApprovedResults()
        {
            var collectionApprovedResults = await _approvedResultService.SelectAllSummaryStage();
            return collectionApprovedResults.Count();
        }

        public async Task<List<modApprovedResult.ApprovedResult>> GetAppovedResultsPagination(PaginationViewModel pagination)
        {
            try
            {
                var response = await _approvedResultService.SelectPaginatorSummaryStage(pagination.Page, pagination.RecordsPage);
                var result = await MapperApprovedResults(response);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ApprovedResultViewModel>> GetApprovedResultsFormat(List<modApprovedResult.ApprovedResult> approvedResults)
        {
            List<ApprovedResultViewModel> ResultsModel = new List<ApprovedResultViewModel>();

            foreach (modApprovedResult.ApprovedResult approvedResult in approvedResults)
            {
                ApprovedResultViewModel approvedResultModel = new ApprovedResultViewModel();
                approvedResultModel.Conciliation = approvedResult.Conciliation;
                approvedResultModel.Scenery = approvedResult.Scenery;
                approvedResultModel.StatusConciliation = approvedResult.StatusConciliation;
                approvedResultModel.ValueBeneficiary = approvedResult.ValueBeneficiary;
                approvedResultModel.ValueInconsistency = approvedResult.ValueInconsistency;
                approvedResultModel.ValuePqr = approvedResult.ValuePqr;
                approvedResultModel.ValueRepetition = approvedResult.ValueRepetition;
                approvedResultModel.UploadDate = approvedResult.UploadDate;
                approvedResultModel.UploadDateFormat = approvedResult.UploadDate.ToString("dd/MM/yyyy");

                ResultsModel.Add(approvedResultModel);
            }

            return ResultsModel;
        }

        //public async Task<ApprovedResult> GetApprovedResultsById(string id)
        //{
        //    ApprovedResult approvedResult = ApprovedResults.Find(approvedResults => approvedResults.Id == id);
        //    return approvedResult;
        //}

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

        public List<ApprovedResultViewModel> GetFilterPagination(List<ApprovedResultViewModel> inicialApprovedResults, PaginationViewModel paginationViewModel, int totalData)
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

            List<ApprovedResultViewModel> approvedResultsFilterPagination = inicialApprovedResults.GetRange(index, count);

            return approvedResultsFilterPagination;
        }

        private async Task<List<modApprovedResult.ApprovedResult>> MapperApprovedResults(List<StageSummaryModel> stageSummaryModel)
        {
            return await Task.Run(() =>
            {
                List<modApprovedResult.ApprovedResult> results = new List<modApprovedResult.ApprovedResult>();
                if (stageSummaryModel.Count > 0)
                {
                    foreach (var stageSummary in stageSummaryModel)
                    {
                        results.Add(MapperToApprovedResult(stageSummary).Result);
                    }
                }
                return results;
            });
        }

        private async Task<modApprovedResult.ApprovedResult> MapperToApprovedResult(StageSummaryModel stageSummaryModel)
        {
            return await Task.Run(() =>
            {
                modApprovedResult.ApprovedResult model = new modApprovedResult.ApprovedResult
                {
                    Conciliation = stageSummaryModel.ConciliationCode,
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

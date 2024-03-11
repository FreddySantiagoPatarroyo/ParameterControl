using ParameterControl.Stage.Entities;

namespace ParameterControl.Stage.Interfaces
{
    public interface IStageService
    {
        Task<int> InsertStage(StageModel entity);
        Task<int> UpdateStage(StageModel entity);
        Task<int> DeleteStage(StageModel entity);
        Task<List<StageModel>> SelectAllStage();
        Task<StageModel> SelectByIdStage(StageModel entity);
        Task<List<StageModel>> SelectPaginatorStage(int page, int row);
        Task<int> SelectCountStage();
        Task<List<StageSummaryModel>> SelectAllSummaryStage();
        Task<List<StageSummaryModel>> SelectPaginatorSummaryStage(int page, int row);
    }
}

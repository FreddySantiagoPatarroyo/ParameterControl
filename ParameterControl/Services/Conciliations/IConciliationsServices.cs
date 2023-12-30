using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;

namespace ParameterControl.Services.Conciliations
{
    public interface IConciliationsServices
    {
        Task<List<Conciliation>> GetConciliations();
        Task<Conciliation> GetConciliationsById(string id);
    }
}

using ParameterControl.LoadControl.Entities;
using ParameterControl.LoadControl.Impl;
using ParameterControl.LoadControl.Interfaces;
using ParameterControl.Models.CrossConnection;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using modCrossConnection = ParameterControl.Models.CrossConnection;

namespace ParameterControl.Services.CrossConnections
{
    public class CrossConnectionsService : ICrossConnectionsService
    {
        private List<modCrossConnection.CrossConnection> crossConnections = new List<modCrossConnection.CrossConnection>();
        private ILoadControlService _loadControlService;
        public CrossConnectionsService(
            IConfiguration configuration
        )
        {
            _loadControlService = new LoadControlService(configuration);
            crossConnections = new List<modCrossConnection.CrossConnection>()
            {
                new modCrossConnection.CrossConnection(){
                    Table = "Tabla1",
                    Periodicity = "Periocidad1",
                    Status = "Pendiente",
                    Error = "Error1",
                    LastLoad = DateTime.Now,
                    LastExecution = DateTime.Now,
                    State = true
                },
                new modCrossConnection.CrossConnection(){
                    Table = "Tabla2",
                    Periodicity = "Periocidad2",
                    Status = "Ok",
                    Error = "Error2",
                    LastLoad = DateTime.Now,
                    LastExecution = DateTime.Now,
                    State = true
                }
            };
        }

        public async Task<List<modCrossConnection.CrossConnection>> GetCrossConnections()
        {
            var collectionCrossConnections = await _loadControlService.SelectAllLoadControl();
            var response = await MapperCrossConnections(collectionCrossConnections);
            return response;
        }

        public async Task<int> CountCrossConnections()
        {
            var collectionCrossConnections = await _loadControlService.SelectAllLoadControl();
            return collectionCrossConnections.Count();
        }

        public async Task<List<modCrossConnection.CrossConnection>> GetCrossConnectionsPagination(PaginationViewModel pagination)
        {
            try
            {
                var response = await _loadControlService.SelectPaginatorLoadControl(pagination.Page, pagination.RecordsPage);
                var result = await MapperCrossConnections(response);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<CrossConnectionViewModel>> GetCrossConnectionsFormat(List<modCrossConnection.CrossConnection> crossConnections)
        {
            List<CrossConnectionViewModel> crossConnectionsModel = new List<CrossConnectionViewModel>();

            foreach (modCrossConnection.CrossConnection crossConnection in crossConnections)
            {
                CrossConnectionViewModel crossConnectionModel = new CrossConnectionViewModel();
                crossConnectionModel.Code = crossConnection.Code;
                crossConnectionModel.Package = crossConnection.Package;
                crossConnectionModel.Table = crossConnection.Table;
                crossConnectionModel.Periodicity = crossConnection.Periodicity;
                crossConnectionModel.Status = crossConnection.Status;
                crossConnectionModel.Error = crossConnection.Error;
                crossConnectionModel.LastLoad = crossConnection.LastLoad;
                crossConnectionModel.LastExecution = crossConnection.LastExecution;
                crossConnectionModel.State = crossConnection.State;
                crossConnectionModel.StateFormat = crossConnection.State ? "Activo" : "Inactivo";
                crossConnectionModel.LastLoadFormat = crossConnection.LastLoad.ToString("dd/MM/yyyy");
                crossConnectionModel.LastExecutionFormat = crossConnection.LastExecution.ToString("dd/MM/yyyy");

                crossConnectionsModel.Add(crossConnectionModel);
            }

            return crossConnectionsModel;
        }

        public async Task<CrossConnectionViewModel> GetCrossConnectionFormat(modCrossConnection.CrossConnection crossConnection)
        {
            CrossConnectionViewModel crossConnectionModel = new CrossConnectionViewModel();
            crossConnectionModel.Code = crossConnection.Code;
            crossConnectionModel.Package = crossConnection.Package;
            crossConnectionModel.Table = crossConnection.Table;
            crossConnectionModel.Periodicity = crossConnection.Periodicity;
            crossConnectionModel.Status = crossConnection.Status;
            crossConnectionModel.Error = crossConnection.Error;
            crossConnectionModel.LastLoad = crossConnection.LastLoad;
            crossConnectionModel.LastExecution = crossConnection.LastExecution;
            crossConnectionModel.State = crossConnection.State;
            crossConnectionModel.StateFormat = crossConnection.State ? "Activo" : "Inactivo";
            crossConnectionModel.LastLoadFormat = crossConnection.LastLoad.ToString("dd/MM/yyyy");
            crossConnectionModel.LastExecutionFormat = crossConnection.LastExecution.ToString("dd/MM/yyyy");

            return crossConnectionModel;
        }

        public async Task<modCrossConnection.CrossConnection> GetCrossConnectionByCode(int code)
        {
            var response = await _loadControlService.SelectByIdLoadControl(new LoadControlModel { Code = code });
            var CrossConnection = await MapperToCrossConnection(response);
            return CrossConnection;
        }

        public async Task<List<CrossConnectionViewModel>> GetFilterCrossConnections(FilterViewModel filterModel)
        {
            List<modCrossConnection.CrossConnection> allCrossConnetions = await GetCrossConnections();
            List<CrossConnectionViewModel> crossConnetionsFilter = await GetCrossConnectionsFormat(allCrossConnetions);

            if (filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == "")
            {
                return crossConnetionsFilter;
            }
            else
            {
                crossConnetionsFilter = await applyFilter(filterModel, crossConnetionsFilter);
            }

            return crossConnetionsFilter;
        }

        private async Task<List<CrossConnectionViewModel>> applyFilter(FilterViewModel filterModel, List<CrossConnectionViewModel> allCrossConnetions)
        {
            var property = typeof(CrossConnectionViewModel).GetProperty(filterModel.ColumValue);

            List<CrossConnectionViewModel> crossConnetionsFilter = new List<CrossConnectionViewModel>();
            if (filterModel.TypeRow == "Select")
            {
                foreach (CrossConnectionViewModel crossConnection in allCrossConnetions)
                {
                    if (property.GetValue(crossConnection).ToString().ToUpper() == filterModel.ValueFilter.ToUpper())
                    {
                        crossConnetionsFilter.Add(crossConnection);
                    }
                }
            }
            else
            {
                foreach (CrossConnectionViewModel crossConnection in allCrossConnetions)
                {
                    if (property.GetValue(crossConnection).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                    {
                        crossConnetionsFilter.Add(crossConnection);
                    }
                }
            }

            return crossConnetionsFilter;
        }

        public List<CrossConnectionViewModel> GetFilterPagination(List<CrossConnectionViewModel> inicialCrossConnections, PaginationViewModel paginationViewModel, int totalData)
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

            List<CrossConnectionViewModel> crossConnectionsFilterPagination = inicialCrossConnections.GetRange(index, count);

            return crossConnectionsFilterPagination;
        }

        private async Task<List<CrossConnection>> MapperCrossConnections(List<LoadControlModel> loadControls)
        {
            return await Task.Run(() =>
            {
                List<CrossConnection> crossConnections = new List<CrossConnection>();
                if (loadControls.Count > 0)
                {
                    foreach (var crossConnection in loadControls)
                    {
                        crossConnections.Add(MapperToCrossConnection(crossConnection).Result);
                    }
                }
                return crossConnections;
            });
        }

        private async Task<CrossConnection> MapperToCrossConnection(LoadControlModel loadControl)
        {
            return await Task.Run(() =>
            {
                CrossConnection model = new CrossConnection
                {
                    Code = loadControl.Code,
                    Package = loadControl.Package,
                    Table = loadControl.Table,
                    Periodicity = loadControl.Periodicity,
                    Status = loadControl.Status,
                    Error = loadControl.Error,
                    LastLoad = loadControl.LastLoad,
                    LastExecution = loadControl.LastExecution,
                    State = loadControl.State,
                };
                return model;
            });
        }

        public async Task<string> ActiveCrossConnection(modCrossConnection.CrossConnection crossConnection)
        {
            var mapping = await MapperActiveCrossConnection(crossConnection);
            var response = await _loadControlService.UpdateLoadControl(mapping);

            return response.Equals(1) ? "Toma transversal activada correctamente" : "Error activando la toma transversal";
        }

        public async Task<string> DesactiveCrossConnection(modCrossConnection.CrossConnection crossConnection)
        {
            var mapping = await MapperDesctiveCrossConnection(crossConnection);
            var response = await _loadControlService.UpdateLoadControl(mapping);

            return response.Equals(1) ? "Toma transversal desactivada correctamente" : "Error desactivando la Toma transversal";
        }

        private async Task<LoadControlModel> MapperActiveCrossConnection(modCrossConnection.CrossConnection crossConnection)
        {
            return await Task.Run(() =>
            {
                LoadControlModel model = new LoadControlModel
                {
                    Code = crossConnection.Code,
                    Package = crossConnection.Package,
                    Table = crossConnection.Table,
                    Periodicity = crossConnection.Periodicity,
                    Status = crossConnection.Status,
                    Error = crossConnection.Error,
                    LastLoad = crossConnection.LastLoad,
                    LastExecution = crossConnection.LastExecution,
                    State = true,
                };
                return model;
            });
        }

        private async Task<LoadControlModel> MapperDesctiveCrossConnection(modCrossConnection.CrossConnection crossConnection)
        {
            return await Task.Run(() =>
            {
                LoadControlModel model = new LoadControlModel
                {
                    Code = crossConnection.Code,
                    Package = crossConnection.Package,
                    Table = crossConnection.Table,
                    Periodicity = crossConnection.Periodicity,
                    Status = crossConnection.Status,
                    Error = crossConnection.Error,
                    LastLoad = crossConnection.LastLoad,
                    LastExecution = crossConnection.LastExecution,
                    State = false,
                };
                return model;
            });
        }
    }
}

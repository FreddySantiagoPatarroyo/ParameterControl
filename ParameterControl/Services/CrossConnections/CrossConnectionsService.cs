using ParameterControl.Models.CrossConnection;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using ParameterControl.Policy.Entities;
using modCrossConnection = ParameterControl.Models.CrossConnection;

namespace ParameterControl.Services.CrossConnections
{
    public class CrossConnectionsService:ICrossConnectionsService
    {
        private List<modCrossConnection.CrossConnection> crossConnections = new List<modCrossConnection.CrossConnection>();
        public CrossConnectionsService() {
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
                },
                new modCrossConnection.CrossConnection(){
                    Table = "Tabla3",
                    Periodicity = "Periocidad2",
                    Status = "Error",
                    Error = "Error3",
                    LastLoad = DateTime.Now,
                    LastExecution = DateTime.Now,
                    State = true
                },
                new modCrossConnection.CrossConnection(){
                    Table = "Tabla4",
                    Periodicity = "Periocidad4",
                    Status = "Pendiente",
                    Error = "Error4",
                    LastLoad = DateTime.Now,
                    LastExecution = DateTime.Now,
                    State = true
                },
                new modCrossConnection.CrossConnection(){
                    Table = "Tabla5",
                    Periodicity = "Periocidad5",
                    Status = "Pendiente",
                    Error = "Error5",
                    LastLoad = DateTime.Now,
                    LastExecution = DateTime.Now,
                    State = true
                },
                new modCrossConnection.CrossConnection(){
                    Table = "Tabla6",
                    Periodicity = "Periocidad6",
                    Status = "Pendiente",
                    Error = "Error6",
                    LastLoad = DateTime.Now,
                    LastExecution = DateTime.Now,
                    State = true
                }
            };
        }

        public async Task<List<modCrossConnection.CrossConnection>> GetCrossConnections()
        {
            return crossConnections;
        }

        public async Task<List<CrossConnectionViewModel>> GetCrossConnectionsFormat(List<modCrossConnection.CrossConnection> crossConnections)
        {
            List<CrossConnectionViewModel> crossConnectionsModel = new List<CrossConnectionViewModel>();

            foreach (modCrossConnection.CrossConnection crossConnection in crossConnections)
            {
                CrossConnectionViewModel crossConnectionModel = new CrossConnectionViewModel();

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

        //public async Task<modCrossConnection.CrossConnection> GetCrossConnectionByCode(int code)
        //{
        //    modCrossConnection.CrossConnection crossConnection = crossConnections.Find(crossConnection => crossConnection.Code == code);
        //    return crossConnection;
        //}

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
            Console.WriteLine(filterModel.TypeRow.ToString());

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
    }
}

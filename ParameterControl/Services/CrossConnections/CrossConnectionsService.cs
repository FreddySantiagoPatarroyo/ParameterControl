using ParameterControl.Models.CrossConnection;
using ParameterControl.Models.Filter;
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
                    Code = 1,
                    Name = "Toma transversal 1",
                    Status = "Pendiente"
                },
                new modCrossConnection.CrossConnection(){
                    Code = 2,
                    Name = "Toma transversal 2",
                    Status = "Ok"
                },
                new modCrossConnection.CrossConnection(){
                    Code = 3,
                    Name = "Toma transversal 1",
                    Status = "Error"
                },
                new modCrossConnection.CrossConnection(){
                    Code = 4,
                     Name = "Toma transversal 1",
                    Status = "Pendiente"
                },
                new modCrossConnection.CrossConnection(){
                    Code = 5,
                     Name = "Toma transversal 1",
                    Status = "Pendiente"
                },
                new modCrossConnection.CrossConnection(){
                    Code = 6,
                     Name = "Toma transversal 1",
                    Status = "Pendiente"
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

                crossConnectionModel.Code = crossConnection.Code;
                crossConnectionModel.Name = crossConnection.Name;
                crossConnectionModel.Status = crossConnection.Status;

                crossConnectionsModel.Add(crossConnectionModel);
            }

            return crossConnectionsModel;
        }

        public async Task<CrossConnectionViewModel> GetCrossConnectionFormat(modCrossConnection.CrossConnection crossConnection)
        {

            CrossConnectionViewModel crossConnectionModel = new CrossConnectionViewModel();

            crossConnectionModel.Code = crossConnection.Code;
            crossConnectionModel.Name = crossConnection.Name;
            crossConnectionModel.Status = crossConnection.Status;

            return crossConnectionModel;
        }

        public async Task<modCrossConnection.CrossConnection> GetCrossConnectionByCode(int code)
        {
            modCrossConnection.CrossConnection crossConnection = crossConnections.Find(crossConnection => crossConnection.Code == code);
            return crossConnection;
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

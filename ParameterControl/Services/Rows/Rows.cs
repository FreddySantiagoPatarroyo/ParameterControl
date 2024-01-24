using ParameterControl.Models.Rows;

namespace ParameterControl.Services.Rows
{
    public class Rows
    {
        public List<Row> RowsPolicies()
        {
            List<Row> rows = new List<Row>()
            {
                 new Row()
                {
                    Value = "Code",
                    Name = "Codigo"
                },
                new Row()
                {
                    Value = "Name",
                    Name = "Nombre"
                },
                new Row()
                {
                    Value = "Description",
                    Name = "Descripcion"
                },
                new Row()
                {
                    Value = "Conciliation",
                    Name = "Conciliacion"
                },
                new Row()
                {
                    Value = "ControlType",
                    Name = "Tipo de control"
                },
                new Row()
                {
                    Value = "OperationType",
                    Name = "Tipo de operacion"
                },
                new Row()
                {
                    Value = "StateFormat",
                    Name = "Estado"
                },
                new Row()
                {
                    Value = "CreationDate",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDate",
                    Name = "Fecha Actualizacion"
                }
            };

            return rows;
        }

        public List<Row> RowsConciliations()
        {
            List<Row> rows = new List<Row>()
            {
                 new Row()
                {
                    Value = "Code",
                    Name = "Codigo"
                },
                new Row()
                {
                    Value = "Name",
                    Name = "Nombre"
                },
                new Row()
                {
                    Value = "Description",
                    Name = "Descripcion"
                },
                new Row()
                {
                    Value = "Conciliation_",
                    Name = "Conciliacion"
                },
                new Row()
                {
                    Value = "RequiredFormat",
                    Name = "Resultado Requerido"
                },
                new Row()
                {
                    Value = "StateFormat",
                    Name = "Estado"
                },
                new Row()
                {
                    Value = "CreationDate",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDate",
                    Name = "Fecha Actualizacion"
                }
            };

            return rows;
        }

        public List<Row> RowsParameters()
        {
            List<Row> rows = new List<Row>()
            {
                 new Row()
                {
                    Value = "Code",
                    Name = "Parametros"
                },
                new Row()
                {
                    Value = "Value",
                    Name = "Valor"
                },
                new Row()
                {
                    Value = "Description",
                    Name = "Descripcion"
                },
                new Row()
                {
                    Value = "ParameterType",
                    Name = "Tipo Parametro"
                },
                 new Row()
                {
                    Value = "StateFormat",
                    Name = "Estado"
                },
                new Row()
                {
                    Value = "CreationDate",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDate",
                    Name = "Fecha Actualizacion"
                }
            };

            return rows;
        }

        public List<Row> RowsScenarios()
        {
            List<Row> rows = new List<Row>()
            {
                 new Row()
                {
                    Value = "Code",
                    Name = "Codigo"
                },
                new Row()
                {
                    Value = "Name",
                    Name = "Nombre"
                },
                new Row()
                {
                    Value = "Impact",
                    Name = "Impacto"
                },
                new Row()
                {
                    Value = "Conciliation",
                    Name = "Conciliacion"
                },
                 new Row()
                {
                    Value = "StateFormat",
                    Name = "Estado"
                },
                new Row()
                {
                    Value = "CreationDate",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDate",
                    Name = "Fecha Actualizacion"
                }
            };

            return rows;
        }

        public List<Row> RowsUsers()
        {
            List<Row> rows = new List<Row>()
            {
                 new Row()
                {
                    Value = "Code",
                    Name = "Codigo Usuario"
                },
                new Row()
                {
                    Value = "User_",
                    Name = "Usuario"
                },
                new Row()
                {
                    Value = "Email",
                    Name = "Email"
                },
                new Row()
                {
                    Value = "Name",
                    Name = "Nombre Usuario"
                },
                  new Row()
                {
                    Value = "StateFormat",
                    Name = "Estado"
                },
                new Row()
                {
                    Value = "CreationDate",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDate",
                    Name = "Fecha Actualizacion"
                }
            };

            return rows;
        }

        public List<Row> RowsIndicators()
        {
            List<Row> rows = new List<Row>()
            {
                 new Row()
                {
                    Value = "Name",
                    Name = "Nombre"
                },
                new Row()
                {
                    Value = "Description",
                    Name = "Descripcion"
                },
                new Row()
                {
                    Value = "Formula",
                    Name = "Formula"
                },
                new Row()
                {
                    Value = "Scenery",
                    Name = "Escenario"
                },
                 new Row()
                {
                    Value = "StateFormat",
                    Name = "Estado"
                },
                new Row()
                {
                    Value = "CreationDate",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDate",
                    Name = "Fecha Actualizacion"
                }
            };

            return rows;
        }

        public List<Row> RowsResults()
        {
            List<Row> rows = new List<Row>()
            {
                 new Row()
                {
                    Value = "Conciliation",
                    Name = "Conciliaciones"
                },
                new Row()
                {
                    Value = "Scenery",
                    Name = "Escenario"
                },
                new Row()
                {
                    Value = "Status",
                    Name = "Estado"
                },
                new Row()
                {
                    Value = "StartDate",
                    Name = "Fecha Inicio"
                },
                new Row()
                {
                    Value = "EndDate",
                    Name = "Fecha Fin"
                },
                new Row()
                {
                    Value = "StartDate",
                    Name = "Fecha Inicio"
                },
                new Row()
                {
                    Value = "BeneValue",
                    Name = "Valor Bene"
                },
                new Row()
                {
                    Value = "IncoValue",
                    Name = "Valor Inco"
                },
                new Row()
                {
                    Value = "PQValue",
                    Name = "Valor PQ"
                },
                new Row()
                {
                    Value = "ReinValue",
                    Name = "Valor Rein"
                },
                 new Row()
                {
                    Value = "StateFormat",
                    Name = "Estado"
                },
                new Row()
                {
                    Value = "CreationDate",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDate",
                    Name = "Fecha Actualizacion"
                }
            };

            return rows;
        }
    }
}

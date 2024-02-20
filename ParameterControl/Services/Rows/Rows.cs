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
                    Value = "CodeFormat",
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
                    Value = "StateFormat",
                    Name = "Estado Activación"
                },
                new Row()
                {
                    Value = "CreationDateFormat",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDateFormat",
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
                    Value = "CodeFormat",
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
                    Value = "Policy",
                    Name = "Politica"
                },
                new Row()
                {
                    Value = "RequiredFormat",
                    Name = "Resultado Requerido"
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
                    Name = "Estado Activación"
                },
                new Row()
                {
                    Value = "CreationDateFormat",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDateFormat",
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
                    Value = "Parameter_",
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
                    Name = "Estado Activación"
                },
                new Row()
                {
                    Value = "CreationDateFormat",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDateFormat",
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
                    Value = "CodeFormat",
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
                    Value = "ConciliationCode",
                    Name = "Conciliacion"
                },
                 new Row()
                {
                    Value = "StateFormat",
                    Name = "Estado Activación"
                },
                new Row()
                {
                    Value = "CreationDateFormat",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDateFormat",
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
                    Value = "CodeFormat",
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
                    Name = "Estado Activación"
                },
                new Row()
                {
                    Value = "CreationDateFormat",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDateFormat",
                    Name = "Fecha Actualizacion"
                }
            };

            return rows;
        }

        public List<Row> RowsCrossConnection()
        {
            List<Row> rows = new List<Row>()
            {
                new Row()
                {
                    Value = "Package",
                    Name = "Paquete"
                },
                 new Row()
                {
                    Value = "Table",
                    Name = "Tabla"
                },
                new Row()
                {
                    Value = "Periodicity",
                    Name = "Periocidad"
                },
                new Row()
                {
                    Value = "Status",
                    Name = "Estado"
                }
                ,
                new Row()
                {
                    Value = "Error",
                    Name = "Error"
                },
                new Row()
                {
                    Value = "LastLoadFormat",
                    Name = "Ultima Carga"
                },
                new Row()
                {
                    Value = "LastExecutionFormat",
                    Name = "Ultima Ejecucion"
                },
                new Row()
                {
                    Value = "StateFormat",
                    Name = "Estado Activacion"
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
                    Name = "Estado Activación"
                },
                new Row()
                {
                    Value = "CreationDateFormat",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDateFormat",
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
                    Value = "ConciliationCode",
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
                    Value = "StartDateFormat",
                    Name = "Fecha Inicio"
                },
                new Row()
                {
                    Value = "EndDateFormat",
                    Name = "Fecha Fin"
                },
                new Row()
                {
                    Value = "BeneValue",
                    Name = "Valor Beneficio"
                },
                new Row()
                {
                    Value = "IncoValue",
                    Name = "Valor Inconsistencia"
                },
                new Row()
                {
                    Value = "PQValue",
                    Name = "Valor PQR"
                },
                new Row()
                {
                    Value = "ReinValue",
                    Name = "Valor Reincidencia"
                },
                 new Row()
                {
                    Value = "StateFormat",
                    Name = "Estado Activación"
                },
                new Row()
                {
                    Value = "CreationDateFormat",
                    Name = "Fecha Creacion"
                },
                 new Row()
                {
                    Value = "UpdateDateFormat",
                    Name = "Fecha Actualizacion"
                }
            };

            return rows;
        }
    }
}

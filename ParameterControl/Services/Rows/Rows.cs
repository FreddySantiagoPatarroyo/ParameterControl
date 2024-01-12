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
                    Value = "Required",
                    Name = "Resultado Requerido"
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
                    Value = "Parameters_",
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
                    Value = "Query",
                    Name = "Query"
                },
                  new Row()
                {
                    Value = "Parameter",
                    Name = "Parametro"
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
                    Value = "CodeUser",
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
                    Value = "NameUser",
                    Name = "Nombre Usuario"
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

            };

            return rows;
        }
    }
}

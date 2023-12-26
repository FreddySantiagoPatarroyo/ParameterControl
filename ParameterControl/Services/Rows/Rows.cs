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
                    Value = "Result",
                    Name = "Req Resultado"
                }
            };

            return rows;
        }
    }
}

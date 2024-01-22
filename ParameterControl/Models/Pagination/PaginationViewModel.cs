namespace ParameterControl.Models.Pagination
{
    public class PaginationViewModel
    {
        public int Page { get; set; } = 1;
        private int recordsPage { get; set; } = 10;
        private readonly int MaxRecordPage = 50;
        public int RecordsPage
        { 
            get 
            { 
                return recordsPage;
            } 
            set 
            {
                recordsPage = (value > MaxRecordPage) ? MaxRecordPage : value;
            } 
        }

        public int IgnoreRecords => recordsPage * (Page - 1);
    }
}

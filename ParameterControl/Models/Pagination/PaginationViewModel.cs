namespace ParameterControl.Models.Pagination
{
    public class PaginationViewModel
    {
        public int Page { get; set; }
        private int recordPage { get; set; } = 10;
        private readonly int MaxRecordPage = 50;
        public int RecordPage { 
            get 
            { 
                return recordPage;
            } 
            set 
            { 
                recordPage = (value > MaxRecordPage) ? MaxRecordPage : value;
            } 
        }

        public int IgnoreRecords => recordPage * (Page - 1);
    }
}

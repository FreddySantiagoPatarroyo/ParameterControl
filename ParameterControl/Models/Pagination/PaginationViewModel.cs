namespace ParameterControl.Models.Pagination
{
    public class PaginationViewModel
    {
        public int Page { get; set; } = 1;
        private int RecordPageSize { get; set; } = 20;
        private readonly int MaxRecordPage = 50;
        public int RecordsPage
        {
            get
            {
                return RecordPageSize;
            }
            set
            {
                RecordPageSize = (value > MaxRecordPage) ? MaxRecordPage : value;
            }
        }

        public int IgnoreRecords => RecordPageSize * (Page - 1);
    }
}

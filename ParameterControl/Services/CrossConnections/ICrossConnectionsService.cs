﻿using ParameterControl.Models.CrossConnection;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;

namespace ParameterControl.Services.CrossConnections
{
    public interface ICrossConnectionsService
    {
        Task<int> CountCrossConnections();
        Task<CrossConnection> GetCrossConnectionByCode(int code);

        //Task<CrossConnection> GetCrossConnectionByCode(int code);
        Task<CrossConnectionViewModel> GetCrossConnectionFormat(CrossConnection crossConnection);
        Task<List<CrossConnection>> GetCrossConnections();
        Task<List<CrossConnectionViewModel>> GetCrossConnectionsFormat(List<CrossConnection> crossConnections);
        Task<List<CrossConnection>> GetCrossConnectionsPagination(PaginationViewModel pagination);
        Task<List<CrossConnectionViewModel>> GetFilterCrossConnections(FilterViewModel filterModel);
        List<CrossConnectionViewModel> GetFilterPagination(List<CrossConnectionViewModel> inicialCrossConnections, PaginationViewModel paginationViewModel, int totalData);
    }
}

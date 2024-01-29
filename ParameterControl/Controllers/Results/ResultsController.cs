﻿using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Result;
using ParameterControl.Models.Filter;
using ParameterControl.Services.Results;
using ParameterControl.Services.Rows;
using modResult = ParameterControl.Models.Result;
using Newtonsoft.Json;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Users;
using System.Reflection;


namespace ParameterControl.Controllers.Results
{
    public class ResultsController: Controller
    {
        public TableResultViewModel TableResults = new TableResultViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IResultsServices resultsServices;
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;

        public ResultsController(
            ILogger<HomeController> logger,
            IResultsServices resultsServices,
            Rows rows,
            AuthenticatedUser authenticatedUser
        )
        {
            this._logger = logger;
            this.resultsServices = resultsServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult> Results()
        {
            List<modResult.Result> results = await resultsServices.GetResults();

            TableResults.Data = await resultsServices.GetResultsFormat(results);

            TableResults.Rows = rows.RowsResults();

            TableResults.IsCreate = false;
            TableResults.IsActivate = true;
            TableResults.IsEdit = false;
            TableResults.IsInactivate = true;

            ViewBag.ApplyFilter = false;

            return View("Results", TableResults);
        }


        [HttpGet]
        public async Task<ActionResult> ResultsFilter(string filterColunm = "", string filterValue = "")
        {
            _logger.LogInformation(filterColunm);
            _logger.LogInformation(filterValue);

            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Results");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue
            };

            List<ResultViewModel> resultsFilter = await resultsServices.GetFilterResults(filter);

            TableResults.Data = resultsFilter;

            TableResults.Rows = rows.RowsResults();

            TableResults.IsCreate = true;
            TableResults.IsActivate = true;
            TableResults.IsEdit = true;
            TableResults.IsInactivate = true;

            ViewBag.ApplyFilter = true;

            return View("ResultsFilter", TableResults);
        }

        //[HttpGet]
        //public async Task<ActionResult> Create()
        //{

        //    Result result = new Result();

        //    return View("Actions/CreateResult", result);
        //}

        //[HttpGet]
        //public async Task<ActionResult> Edit(string id)
        //{
        //    Result result = await resultsServices.GetResultsById(id);

        //    return View("Actions/EditResult", result);
        //}

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            Result result = await resultsServices.GetResultsById(id);

            return View("Actions/ViewResult", result);
        }

        [HttpGet]
        public async Task<ActionResult> Active(string id)
        {
            modResult.Result result = await resultsServices.GetResultsById(id);

            return View("Actions/ActiveResult", result);
        }

        [HttpPost]
        public async Task<ActionResult> ActiveResult([FromBody] string id)
        {
            try
            {
                modResult.Result request = await resultsServices.GetResultsById(id);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = true;
                _logger.LogInformation($"Inicia método ResultController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se activo el resultado de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ResultController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar el resultado", state = "Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            Result result = await resultsServices.GetResultsById(id);

            return View("Actions/DesactiveResult", result);
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveResult([FromBody] string id)
        {
            try
            {
                modResult.Result request = await resultsServices.GetResultsById(id);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = false;
                _logger.LogInformation($"Inicia método ResultController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se desactivo el resultado de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ResultController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar el resultado", state = "Error" });
            }
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsResults()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterResults(FilterViewModel filter)
        {

            Console.WriteLine(filter.ColumValue);

            Console.WriteLine(filter.ValueFilter);

            return RedirectToAction("ResultsFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter
            });
        }
    }
}

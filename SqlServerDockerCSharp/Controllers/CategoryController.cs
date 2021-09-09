using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlServerDockerCSharp.Models;
using SqlServerDockerCSharp.Service;
using NLog;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SqlServerDockerCSharp.Controllers
{

    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        // POST: api/values
        //https://localhost:5001/api/product

        [HttpPost]
        public ActionResult Post()
        {


            bool status = false;
            List<Category> data = new();

            string mode = Request.Form["mode"];

            var CategoryId = Request.Form["CategoryId"];
            var CategoryDescription = Request.Form["CategoryDescription"];

            string code;
            CategoryCrud categoryCrud = new();

            switch (mode)
            {
                case "create":
                    try
                    {
                        CategoryCrud.Create(CategoryDescription);
                        // if direct it will output string lol
                        code = ((int)ReturnCode.CREATE_SUCCESS).ToString();
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;
                    }
                    break;
                case "read":
                    try
                    {
                        data = CategoryCrud.Read();
                        code = ((int)ReturnCode.READ_SUCCESS).ToString();
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;
                    }
                    break;
                case "update":
                    try
                    {
                        CategoryCrud.Update(CategoryId, CategoryDescription);
                        code = ((int)ReturnCode.UPDATE_SUCCESS).ToString();
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;
                    }
                    break;
                case "delete":
                    try
                    {
                        CategoryCrud.Delete(CategoryId);
                        code = ((int)ReturnCode.DELETE_SUCCESS).ToString();
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;
                    }
                    break;
                default:
                    code = ((int)ReturnCode.ACCESS_DENIED_NO_MODE).ToString();
                    status = false;
                    break;
            }
            int code_return;
            bool success = int.TryParse(code, out code_return);
            if (!success)
            {
                // server error
                code_return = 500;
                logger.Warn("somebody try to hack");
            }
            return Ok(new { status, code = code_return, data = data });
        }
    }
}
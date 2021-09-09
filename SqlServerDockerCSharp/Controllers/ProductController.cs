using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SqlServerDockerCSharp.Models;
using SqlServerDockerCSharp.Service;
using Microsoft.Extensions.Logging;
using NLog;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SqlServerDockerCSharp.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ProductController()
        {
        }
        // POST: api/values
        //https://localhost:5001/api/product

        [HttpPost]
        public ActionResult Post()
        {

            bool status = false;
            List<Product> data = new();

            string mode = Request.Form["mode"];

            var ProductId = Request.Form["ProductId"];
            var CategoryId = Request.Form["CategoryId"];
            var ProductCode = Request.Form["ProductCode"];
            var ProductDescription = Request.Form["ProductDescription"];
            var UnitPrice = Request.Form["UnitPrice"];

            string code;

            switch (mode)
            {
                case "create":
                    try
                    {
                        ProductCrud.Create(CategoryId,ProductCode,ProductDescription,UnitPrice);
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
                        data = ProductCrud.Read();
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
                        ProductCrud.Update(ProductId,CategoryId, ProductCode, ProductDescription, UnitPrice);
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
                        ProductCrud.Delete(ProductId);
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

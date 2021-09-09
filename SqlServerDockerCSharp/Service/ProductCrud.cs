using System;
using System.Collections.Generic;
using System.Linq;
using SqlServerDockerCSharp.Models;
using System.IO;

namespace SqlServerDockerCSharp.Service
{
 
    public class ProductCrud
    {
        public static void Create(String CategoryId,String ProductCode, String ProductDescription, String UnitPrice)
        {
            
            var db = new SolarContext();
            using var transaction = db.Database.BeginTransaction();
            try
            {
                Product product = new();
                // this is possible category id is not int  so injection can be happening here .
                if (!int.TryParse(CategoryId, out int goodCategoryId))
                {
                    throw new Exception("SQL INJECTION CATEGORYID");
                }
                product.CategoryId = goodCategoryId;
                product.ProductCode = ProductCode;
                product.ProductDescription = ProductDescription;

                if (!decimal.TryParse(UnitPrice, out decimal goodUnitPrice))
                {
                    throw new Exception("SQL INJECTION PRICE");
                }
                product.UnitPrice = goodUnitPrice;
                db.Products.Add(product);
                int affectedRows  = db.SaveChanges();
                // we need to know if something wrong
                if (affectedRows == 0)
                {
                    // logs some error 
                }
                transaction.Commit();

            }catch(Exception ex)
            {
                // var logWriter = new StreamWriter(Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), "~/log.txt"));
                //  logWriter.WriteLine("Log message :" + ex.Message);
                //  logWriter.Dispose();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
        public static List<Product> Read()
        {
            List<Product> products = new();
            try
            {
                var db = new SolarContext();
                // we prefer to try catch in normal way but we unsure it will work . need time
                products = db.Products.ToList();
            }
            catch (Exception ex)
            {
              //  var logWriter = new StreamWriter(Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), "~/log.txt"));
              //  logWriter.WriteLine("Log message :" + ex.Message);
               // logWriter.Dispose();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return products;
        }
        public static void Update(String ProductId, String CategoryId, String ProductCode, String ProductDescription, String UnitPrice)
        {
            var db = new SolarContext();
            using var transaction = db.Database.BeginTransaction();
            try
            {
                // the proper way is another level. check if record existed first before update . if yes then update if not nooooo

                Product product = new();
                // this is possible category id is not int  so injection can be happening here .
                if (!int.TryParse(ProductId, out int goodProductId))
                {
                    throw new Exception("SQL INJECTION product id");
                }
                if (!int.TryParse(CategoryId, out int goodCategoryId))
                {
                    throw new Exception("SQL INJECTION category id");
                }
                product.ProductId = goodProductId;
                product.CategoryId = goodCategoryId;
                product.ProductCode = ProductCode;
                product.ProductDescription = ProductDescription;
                bool successPrice = decimal.TryParse(UnitPrice, out decimal goodUnitPrice);
                if (!successPrice)
                {
                    throw new Exception("SQL INJECTION PRICE");
                }
                product.UnitPrice = goodUnitPrice;
                db.Update(product);
                int affectedRows =  db.SaveChanges();
                // we need to know if something wrong
                if(affectedRows == 0)
                {
                    // logs some error 
                }
                transaction.Commit();

            }
            catch (Exception ex)
            {
              //  var logWriter = new StreamWriter(Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), "~/log.txt"));
              //  logWriter.WriteLine("Log message :" + ex.Message);
              //  logWriter.Dispose();

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public static void Delete(String ProductId)
        {
            var db = new SolarContext();
            using var transaction = db.Database.BeginTransaction();
            try
            {
                Product product = new();
                // this is possible category id is not int  so injection can be happening here .
                bool success = int.TryParse(ProductId, out int goodProductId);
                if (!success)
                {
                    throw new Exception("SQL INJECTION");
                }
                product.ProductId = goodProductId;
                db.Remove(product);
                int affectedRows = db.SaveChanges();
                // we need to know if something wrong
                if (affectedRows == 0)
                {
                    // logs some error 
                }
                transaction.Commit();

            }
            catch (Exception ex)
            {
                //   var logWriter = new StreamWriter(Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), "~/log.txt"));
                //    logWriter.WriteLine("Log message :" + ex.Message);
                //    logWriter.Dispose();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using SqlServerDockerCSharp.Models;
using System.IO;
using NLog;

namespace SqlServerDockerCSharp.Service
{
 
    public class ProductCrud
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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
                    logger.Warn("Possible SQL Injection");
                    throw new Exception("SQL INJECTION CATEGORYID");
                }
                product.CategoryId = goodCategoryId;
                product.ProductCode = ProductCode;
                product.ProductDescription = ProductDescription;

                if (!decimal.TryParse(UnitPrice, out decimal goodUnitPrice))
                {
                    logger.Warn("Possible SQL Injection");
                    throw new Exception("SQL INJECTION PRICE");
                }
                product.UnitPrice = goodUnitPrice;
                db.Products.Add(product);
                int affectedRows  = db.SaveChanges();
                // we need to know if something wrong
                if (affectedRows == 0)
                {
                    // logs some error
                    logger.Debug("0 affected row created : product");

                }
                else
                {
                    logger.Info("1 affected row created : product");

                }
                transaction.Commit();

            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
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
                    logger.Warn("Possible SQL Injection");
                    throw new Exception("SQL INJECTION product id");
                }
                if (!int.TryParse(CategoryId, out int goodCategoryId))
                {
                    logger.Warn("Possible SQL Injection");
                    throw new Exception("SQL INJECTION category id");
                }
                product.ProductId = goodProductId;
                product.CategoryId = goodCategoryId;
                product.ProductCode = ProductCode;
                product.ProductDescription = ProductDescription;
                bool successPrice = decimal.TryParse(UnitPrice, out decimal goodUnitPrice);
                if (!successPrice)
                {
                    logger.Warn("Possible SQL Injection");
                    throw new Exception("SQL INJECTION PRICE");
                }
                product.UnitPrice = goodUnitPrice;
                db.Update(product);
                int affectedRows =  db.SaveChanges();
                // we need to know if something wrong
                if(affectedRows == 0)
                {
                    // logs some error
                    logger.Debug("0 affected row updated : product");
                }
                else
                {
                    logger.Info("0 affected row updated : product");

                }
                transaction.Commit();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
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
                    logger.Debug("0 affected row deleted : product");
                }
                else
                {
                    logger.Info("1 affected row deleted : product");
                }
                transaction.Commit();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}

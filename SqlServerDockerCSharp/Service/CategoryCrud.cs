using System;
using System.Collections.Generic;
using System.Linq;
using SqlServerDockerCSharp.Models;
using System.IO;

namespace SqlServerDockerCSharp.Service
{
  
    public class CategoryCrud
    {
        public static void Create(String CategoryDescription)
        {

            var db = new SolarContext();
            using var transaction = db.Database.BeginTransaction();
            try
            {
                Category Category = new();
                Category.CategoryDescription = CategoryDescription;
                db.Categorys.Add(Category);
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
              //  var logWriter = new StreamWriter(Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), "~/log.txt"));
             //   logWriter.WriteLine("Log message :" + ex.Message);
              //  logWriter.Dispose();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
        public static List<Category> Read()
        {
            List<Category> Categorys = new();
            try
            {
                var db = new SolarContext();
                // we prefer to try catch in normal way but we unsure it will work . need time
                Categorys = db.Categorys.ToList();
            }
            catch (Exception ex)
            {
                var logWriter = new StreamWriter(Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), "~/log.txt"));
                logWriter.WriteLine("Log message :" + ex.Message);
                logWriter.Dispose();
            }
            return Categorys;
        }
        public void Update(String CategoryId, String CategoryDescription)
        {
            var db = new SolarContext();
            using var transaction = db.Database.BeginTransaction();
            try
            {
                Category Category = new();
                // this is possible category id is not int  so injection can be happening here .
                int goodCategoryId = 0;
                bool success = int.TryParse(CategoryId, out goodCategoryId);
                if (!success)
                {
                    throw new Exception("SQL INJECTION");
                }
                Category.CategoryId = goodCategoryId;
                Category.CategoryDescription = CategoryDescription;
                db.Update(Category);
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
              //  var logWriter = new StreamWriter(Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), "~/log.txt"));
             //   logWriter.WriteLine("Log message :" + ex.Message);
               // logWriter.Dispose();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public static void Delete(String CategoryId)
        {
            var db = new SolarContext();
            using var transaction = db.Database.BeginTransaction();
            try
            {
                Category Category = new();
                // this is possible category id is not int  so injection can be happening here .
                int goodCategoryId = 0;
                bool success = int.TryParse(CategoryId, out goodCategoryId);
                if (!success)
                {
                    throw new Exception("SQL INJECTION");
                }
                Category.CategoryId = goodCategoryId;
                db.Remove(Category);
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
             //   logWriter.WriteLine("Log message :" + ex.Message);
               // logWriter.Dispose();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}

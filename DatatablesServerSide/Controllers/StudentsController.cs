using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DatatablesServerSide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatatablesServerSide.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View(Data.StudentContext.StudentList);// Send students to view
        }

        
        public JsonResult GetFilteredItems()
        {
            System.Threading.Thread.Sleep(2000);//Used to display loading message in demonstration, remove this line in production
            int draw = Convert.ToInt32(Request.Query["draw"]);

            // Data to be skipped , 
            // if 0 first "length" records will be fetched
            // if 1 second "length" of records will be fethced ...
            int start = Convert.ToInt32(Request.Query["start"]);

            // Records count to be fetched after skip
            int length = Convert.ToInt32(Request.Query["length"]);
            
            // Getting Sort Column Name
            int sortColumnIdx = Convert.ToInt32(Request.Query["order[0][column]"]);
            string sortColumnName = Request.Query["columns[" + sortColumnIdx + "][name]"];
            
            // Sort Column Direction  
            string sortColumnDirection = Request.Query["order[0][dir]"];
            
            // Search Value
            string searchValue = Request.Query["search[value]"].FirstOrDefault()?.Trim();

            // Total count matching search criteria 
            int recordsFilteredCount =
                    Data.StudentContext.StudentList
                    .Where(a => a.Lastname.Contains(searchValue) || a.Firstname.Contains(searchValue))
                    .Count();

            // Total Records Count
            int recordsTotalCount = Data.StudentContext.StudentList.Count();

            // Filtered & Sorted & Paged data to be sent from server to view
            List<Student> filteredData = null;
            if (sortColumnDirection == "asc")
            {
                filteredData =
                    Data.StudentContext.StudentList
                    .Where(a => a.Lastname.Contains(searchValue) || a.Firstname.Contains(searchValue))
                    .OrderBy(x => x.GetType().GetProperty(sortColumnName).GetValue(x))//Sort by sortColumn
                    .Skip(start)
                    .Take(length)
                    .ToList<Student>();
            }
            else
            {
                filteredData =
                   Data.StudentContext.StudentList
                   .Where(a => a.Lastname.Contains(searchValue) || a.Firstname.Contains(searchValue))
                   .OrderByDescending(x => x.GetType().GetProperty(sortColumnName).GetValue(x))
                   .Skip(start)
                   .Take(length)
                   .ToList<Student>();
            }
            
            return Json(
                        new {
                              data = filteredData,
                              draw = Request.Query["draw"],
                              recordsFiltered = recordsFilteredCount,
                              recordsTotal = recordsTotalCount
                            }
                    );
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            Student s = Data.StudentContext.StudentList.FirstOrDefault(a => a.Id == id);
            return View(s);
        }


        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
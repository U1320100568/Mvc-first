using Information.Helpers;
using Information.Models;
using Information.Service;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Information.Controllers
{
    public class ExcelController : Controller
    {
        
        private static string ClassName { get; set; }

        ExportService<Member> m_export = new ExportService<Member>();
        ExportService<Infor> i_export = new ExportService<Infor>();
        ExportService<LogRecord> l_export = new ExportService<LogRecord>();
        ExportService<Feature> f_export = new ExportService<Feature>();
        
        
        [HttpGet]
        public void Export(int? id)
        {
            /*
            ClassName = "Member";
            List<string> removeColumns = new List<string>{ "", "", "" };
            ExportService<Member> m_export = new ExportService<Member>();
            return this.File(m_export.Export(removeColumns).ToArray(), "application/vnd.ms-excel", ClassName + ".xlsx");
            */
            
            
            /*
            ClassName = "Member";
            ExportService<Member> m_export = new ExportService<Member>();
            ViewBag.ExportColumns = m_export.EntityPropertyName()
                       .Select(column => new SelectListItem()
                       {
                           Value = column.Key,
                           Text = column.Value,
                           Selected = true
                       })
                       .ToList();

            return View();
            */
            /*/**/
           /*
            switch (id)
            {
                
                
                case 1:
                    
                    ClassName = "Member";
                    
                                        ViewBag.ExportColumns = m_export.EntityPropertyName()
                                            .Select(column => new SelectListItem() {
                                                Value = column.Key  ,
                                                Text = column.Value,
                                                Selected = true
                                            })  
                                            .ToList();
                                        return View();

                                     

                    break;

                case 2:
                    ClassName = "Infor";
                    ViewBag.ExportColumns = i_export.EntityPropertyName()
                        .Select(column => new SelectListItem()
                        {
                            Value = column.Key,
                            Text = column.Value,
                            Selected = true
                        })
                        .ToList();
                    //return View();
                    break;
                case 3:
                    ClassName = "LogRecord";
                    ViewBag.ExportColumns = l_export.EntityPropertyName()
                         .Select(column => new SelectListItem()
                         {
                             Value = column.Key,
                             Text = column.Value,
                             Selected = true
                         })
                         .ToList();
                    //return View();
                    break;
                case 4:
                    ClassName = "Feature";
                    ViewBag.ExportColumns = f_export.EntityPropertyName()
                        .Select(column => new SelectListItem()
                        {
                            Value = column.Key,
                            Text = column.Value,
                            Selected = true
                        })
                        .ToList();
                    //return View();
                    break;
                default:
                    //return Content("<script>history.back()</script>");
                    break;
            }
            */

        }

        
        [HttpPost]
        public ActionResult Export(string selectedColumns)//
        {
            //無作用ing

            ClassName = "Member";
            List<string> removeColumns = new List<string> { "", "", "" };
            ExportService<Member> m_export = new ExportService<Member>();
            //return this.File(m_export.Export(removeColumns).ToArray(), "application/vnd.ms-excel", ClassName + ".xlsx");
            return Content("Heelo");
            
        }

        public ActionResult JsonTest(string selectedColumns,string className)
        {
            //return Json(new { s = selectedColumns });
            return Content(selectedColumns+"  "+ className);
            //return RedirectToAction("Index", "Members");
            
        }

        
        public ActionResult ExportFile(string selectedColumns ,string className)//
        {
            
            var removeColumns = selectedColumns.Split(',').ToList();

            switch (className)
            {
                case "Member":
                    return this.File(m_export.Export(removeColumns).ToArray(),"application/vnd.ms-excel", className + ".xlsx");
                case "Infor":
                    return this.File(i_export.Export(removeColumns).ToArray(), "application/vnd.ms-excel", className + ".xlsx");
                case "LogRecord":
                    return this.File(l_export.Export(removeColumns).ToArray(), "application/vnd.ms-excel", className + ".xlsx");
                case "Feature":
                    return this.File(f_export.Export(removeColumns).ToArray(), "application/vnd.ms-excel", className + ".xlsx");
                default:
                    return Content("<script>history.back()</scrpit>");
            }
            /**/
                
        }

        /**/
        
        
    }
}
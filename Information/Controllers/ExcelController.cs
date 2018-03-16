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
        [Authorize]
        public ActionResult Export(int? id)//
        {

            string className;
            switch (id)
            {
                
                case 1:
                    className = "Member";
                    ExportService<Member> m_export = new ExportService<Member>();
                    return this.File(m_export.Export(className).ToArray(), "application/vnd.ms-excel", className + ".xlsx");
                case 2:
                    className = "Infor";
                    ExportService<Infor> i_export = new ExportService<Infor>();
                    return this.File(i_export.Export(className).ToArray(), "application/vnd.ms-excel", className + ".xlsx");
                case 3:
                    className = "LogRecord";
                    ExportService<LogRecord> l_export = new ExportService<LogRecord>();
                    return this.File(l_export.Export(className).ToArray(), "application/vnd.ms-excel", className + ".xlsx");
                case 4:
                    className = "Feature";
                    ExportService<Feature> f_export = new ExportService<Feature>();
                    return this.File(f_export.Export(className).ToArray(), "application/vnd.ms-excel", className + ".xlsx");
                default:
                    return Content("<script>history.back()</script>");
            }
            
            
            
        }

    }
}
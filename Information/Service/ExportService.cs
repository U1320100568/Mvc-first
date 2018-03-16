using Information.Helpers;
using Information.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Information.Service
{
    public class ExportService<TEntity>
        where TEntity : class
    {
       

        public MemoryStream Export(string className)
        {
            //建立工作簿
            IWorkbook workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet(className);

            //建立列
            IRow tempIRow = sheet.CreateRow(0);
            //取得要處理的第一列
            tempIRow = sheet.GetRow(0);
            
           
            //每個迴圈都要產生一個資料列
            switch (className)
            {
                
                case "Member":
                    CrudRepository<Member> memberRepo = new CrudRepository<Member>();
                    ICollection<Member> m_List = memberRepo.GetAll().ToList();

                    sheet.GetRow(0).CreateCell(0).SetCellValue("索引鍵");
                    sheet.GetRow(0).CreateCell(1).SetCellValue("帳號");
                    sheet.GetRow(0).CreateCell(2).SetCellValue("密碼");
                    int i = 1;
                    foreach (Member item in m_List)
                    {
                        sheet.CreateRow(i);
                        sheet.GetRow(i).CreateCell(0).SetCellValue(item.ID );
                        sheet.GetRow(i).CreateCell(1).SetCellValue(item.Name);
                        sheet.GetRow(i).CreateCell(2).SetCellValue(item.Password);
                        i++;
                    }
                    break;

                case "Infor":
                    CrudRepository<Infor> inforRepo = new CrudRepository<Infor>();
                    ICollection<Infor> f_List = inforRepo.GetAll().ToList();

                    sheet.GetRow(0).CreateCell(0).SetCellValue("索引鍵");
                    sheet.GetRow(0).CreateCell(1).SetCellValue("發布人");
                    sheet.GetRow(0).CreateCell(2).SetCellValue("時間");
                    sheet.GetRow(0).CreateCell(3).SetCellValue("內容");
                    i = 1;
                    foreach (Infor item in f_List)
                    {
                        sheet.CreateRow(i);
                        sheet.GetRow(i).CreateCell(0).SetCellValue(item.ID);
                        sheet.GetRow(i).CreateCell(1).SetCellValue(item.Publisher);
                        sheet.GetRow(i).CreateCell(2).SetCellValue(item.ReleaseTime.ToString());
                        sheet.GetRow(i).CreateCell(3).SetCellValue(item.Content);
                        i++;
                    }
                    break;

                case "LogRecord":
                    CrudRepository<LogRecord> logRecRepo = new CrudRepository<LogRecord>();
                    ICollection<LogRecord> l_List = logRecRepo.GetAll().ToList();

                    sheet.GetRow(0).CreateCell(0).SetCellValue("索引鍵");
                    sheet.GetRow(0).CreateCell(1).SetCellValue("會員");
                    sheet.GetRow(0).CreateCell(2).SetCellValue("登入時間");
                    sheet.GetRow(0).CreateCell(3).SetCellValue("登出時間");
                    i = 1;
                    foreach (LogRecord item in l_List)
                    {
                        sheet.CreateRow(i);
                        sheet.GetRow(i).CreateCell(0).SetCellValue(item.ID);
                        sheet.GetRow(i).CreateCell(1).SetCellValue(WebSiteHelper.GetUserNameById(item.MemberId));
                        sheet.GetRow(i).CreateCell(2).SetCellValue(item.LoginTime.ToString());
                        sheet.GetRow(i).CreateCell(3).SetCellValue(item.LogoutTime.ToString());
                        i++;
                    }
                    break;

                case "Feature":
                    CrudRepository<Feature> featureRepo = new CrudRepository<Feature>();
                    ICollection<Feature> f_list = featureRepo.GetAll().ToList();

                    sheet.GetRow(0).CreateCell(0).SetCellValue("索引鍵");
                    sheet.GetRow(0).CreateCell(1).SetCellValue("會員");
                    sheet.GetRow(0).CreateCell(2).SetCellValue("公告功能");
                    sheet.GetRow(0).CreateCell(3).SetCellValue("登入紀錄功能");
                    i = 1;
                    foreach (Feature item in f_list)
                    {
                        sheet.CreateRow(i);
                        sheet.GetRow(i).CreateCell(0).SetCellValue(item.ID);
                        sheet.GetRow(i).CreateCell(1).SetCellValue(WebSiteHelper.GetUserNameById(item.MemberId));
                        sheet.GetRow(i).CreateCell(2).SetCellValue(item.FeatInfor);
                        sheet.GetRow(i).CreateCell(3).SetCellValue(item.FeatLogRec);
                        i++;
                    }
                    break;

                default:
                    break;
            }
                
            
            
            
            /*
            FileStream fs = new FileStream(Path.Combine(@"C:/Users/roy.lai/Downloads/", "output.xlsx"), FileMode.Create, FileAccess.Write);
            workbook.Write(fs);
            fs.Close();
            */
            /*
            MemoryStream MS = new MemoryStream();
            workbook.Write(MS);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("conntent-disposition", "attachment;filename = Output.xlsx");
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.speadsheetml.sheet";
            System.Web.HttpContext.Current.Response.BinaryWrite(MS.ToArray());
            workbook = null;
            MS.Close();
            MS.Dispose();
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
            */
            MemoryStream MS = new MemoryStream();
            workbook.Write(MS);
            MS.Close();
            //使用File回傳
            return MS;

        }
    }
}
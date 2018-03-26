using Information.Helpers;
using Information.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Information.Service
{
    public class ExportService<TEntity>
        where TEntity : class
    {
       

        public MemoryStream Export(List<string> removeColumns)//
        {
            //建立工作簿
            IWorkbook workbook = new XSSFWorkbook();
            
            string className = typeof(TEntity).Name;
            ISheet sheet = workbook.CreateSheet(className);
            
            //建立列
            IRow tempIRow = sheet.CreateRow(0);
            //取得要處理的第一列
            tempIRow = sheet.GetRow(0);


            //每個迴圈都要產生一個資料列
            int i = 1, j = 0;//i: row, j: column
            
            Dictionary<string, string> tableHead = EntityPropertyName();
            foreach (var item in tableHead)
            {
                if (!removeColumns.Exists(r => r == item.Key)) { continue; }
                sheet.GetRow(0).CreateCell(j).SetCellValue(item.Value);  //輸出表頭
                j++;
            }

            CrudRepository<TEntity> repository = new CrudRepository<TEntity>();
            var tableContent = repository.GetAll(); //資料庫提取資料

            foreach (var row in tableContent)
            {
                j = 0;
                sheet.CreateRow(i);
                IEnumerable props = row.GetType().GetProperties();
                
                foreach (PropertyInfo prop in props)       //取出屬性個數
                {
                    if (!removeColumns.Exists(r => r == prop.Name)) { continue; }
                    sheet.GetRow(i).CreateCell(j).SetCellValue(prop.GetValue(row).ToString());  //輸出內容，使用Reflection.PropertyInfo
                    j++;
                }
                i++;
            }
           
            
            
            MemoryStream MS = new MemoryStream();
            workbook.Write(MS);
            MS.Close();
            //使用File回傳
            return MS;

        }

        

        //取出屬性名稱對照的中文
        public Dictionary<string,string> EntityPropertyName()
        {
            Type t = typeof(TEntity);

            Dictionary<string,string> listTableName = new Dictionary<string,string>();
            foreach (var pro in t.GetProperties())
            {
                var proKeyName = ExportDataHelper.CustomerExportColumns()
                    .FirstOrDefault(p => p.Key == pro.Name);
                listTableName.Add(proKeyName.Key,proKeyName.Value);
                
            }
            

            return listTableName;
        }

        public List<SelectListItem> GetSelectList()
        {
            List<SelectListItem> selectList = EntityPropertyName()
                                                .Select(column => new SelectListItem()
                                                {
                                                    Value = column.Key,
                                                    Text = column.Value,
                                                    Selected = true
                                                })
                                                .ToList();
            return selectList;
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Information.Helpers
{
    public class ExportDataHelper
    {
        //處理匯出資料欄位
        public  static Dictionary<string,string> CustomerExportColumns()
        {
            var result = new Dictionary<string, string>();

            result.Add("ID", "索引值");
            result.Add("Name", "帳號");
            result.Add("Password", "密碼");
            result.Add("Title", "標題");
            result.Add("Publisher", "公告者");
            result.Add("ReleaseTime", "公告時間");
            result.Add("Content", "內容");
            result.Add("MemberId", "會員索引值");
            result.Add("LoginTime", "登入時間");
            result.Add("LogoutTime", "登出時間");
            result.Add("FeatInfor", "公告功能");
            result.Add("FeatLogRec", "登入時間功能");
            return result;
        }
    }
}
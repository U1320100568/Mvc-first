using Information.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace Information.Service
{
    public class CrudRepository<TEntity>
        where TEntity : class
    {
        private DbContext Db { get; set; }
        

        public CrudRepository() : this(new AppDbContext())
        {
        }
        public CrudRepository(DbContext db)
        {
            if(db == null)
            {
                throw new ArgumentNullException("db");
            }
            Db = db;
        }
        public CrudRepository(ObjectContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }
            Db = new DbContext(db, true);
        }

        public void Create(TEntity entity)
        {
            
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                Db.Set<TEntity>().Add(entity);
                SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                Db.Entry(entity).State = EntityState.Modified;
                SaveChanges();
            }
        }

        //用SQL Commeamd更新資料欄位 (可複選)
        public void UpdateBySql(TEntity entity, List<string> updatedProps)
        {
            Type t = typeof(TEntity);

            string tableName = GetTableName(DataBaseType.MS_SQL_SERVER);
            List<string> paras = new List<string>();

            string queryString = "UPDATE " + tableName + " SET ";
            int index = 0;
            foreach (var updatedProp in updatedProps)
            {
                PropertyInfo prop = t.GetProperty(updatedProp);
                queryString += prop.Name + " = "+string.Format("@{0}",index++);//'" + prop.GetValue(entity) + "'
                if (updatedProp != updatedProps.Last()) { queryString += " , "; }
                paras.Add(prop.GetValue(entity).ToString()); //將要更新的變數存到list
            }
            queryString += " WHERE ID = " + t.GetProperty("ID").GetValue(entity);

            SqlQuery(queryString, paras);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                Db.Entry(entity).State = EntityState.Deleted;
                SaveChanges();
            }
        }

        //將 SqlCommand 參數化
        public void SqlQuery(string queryString ,List<string> paraList)
        {   
            //建立SqlParameter物件
            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para;
            int index = 0;
            foreach (var item in paraList) {
                var paraName = string.Format("@{0}", index++);
                para= new SqlParameter(paraName, item);
                paras.Add(para);
            }
            //執行 SQL Command
            Db.Database.ExecuteSqlCommand(queryString,paras.ToArray());
        }

        //測試SQL Injection
        public TEntity SqlQueryTest(string queryString)
        {
            var result = Db.Set<TEntity>().SqlQuery(queryString).FirstOrDefault();
            return result;
        }
        

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Set<TEntity>().FirstOrDefault(predicate);
        }


        public IQueryable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().AsQueryable();
        }

        //取得該類別的Table Name
        public string GetTableName(DataBaseType dbType = DataBaseType.MS_SQL_SERVER) {
            ObjectContext objectContext = ((IObjectContextAdapter)Db).ObjectContext;
            ObjectSet<TEntity> objectSet = objectContext.CreateObjectSet<TEntity>();

            //string sql = objectSet.ToTraceString();
            string sql = Db.Set<TEntity>().ToString();
            string matchWords = string.Empty;

            switch(dbType)
            {
                case DataBaseType.MS_SQL_SERVER:
                    matchWords = "FROM (?<table>.*) AS";
                    break;
                case DataBaseType.Oracle:
                    matchWords = "FROM \"(?<schema>.*)\".\"(?<table>.*)\"\\s";
                    break;
            }
            Regex regex = new Regex(matchWords);
            Match match = regex.Match(sql);
            string table = match.Groups["table"].Value;
            return table;
        }
        

        public void SaveChanges()
        {
            Db.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Db != null)
                {
                    Db.Dispose();
                    Db = null;
                }
            }
        }

        public enum DataBaseType //練習列舉
        {
            MS_SQL_SERVER = 0,
            Oracle = 1
        }
    }
}
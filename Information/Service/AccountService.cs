using Information.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Information.Service
{
    public class AccountService
    {
        private CrudRepository<LogRecord> logRecRepo;
        private CrudRepository<Feature> featRepo;
        private CrudRepository<Member> memberRepo;

        public AccountService()
        {
            logRecRepo = new CrudRepository<LogRecord>();
            featRepo = new CrudRepository<Feature>();
            memberRepo = new CrudRepository<Member>();
        }

        public Member Login(Member entity)
        {
            Member member;
            //驗證登錄
            if ((String.IsNullOrEmpty(entity.Name)) || (String.IsNullOrEmpty(entity.Password)))
            {
                member = null;
            }
            /*
            if (!memberService.GetAll().Any(m => m.Name == entity.Name))
            {
                member = null;
            }
            */
            member = memberRepo.Get(m => m.Name == entity.Name);
            if(member != null)
            {
                if (member.Password != entity.Password)
                {
                    member = null;
                }
            
                //紀錄登入時間
                LogRecord logRecord = new LogRecord();
                logRecord.LoginTime = DateTime.Now;
                logRecord.MemberId = member.ID;
                logRecord.LogoutTime = DateTime.Now.AddMinutes(30);
                logRecRepo.Create(logRecord);
            }
            

            return member;
        }

        public void Logout(string name)
        {
            Member member = memberRepo.Get(m => m.Name == name);
            //紀錄登出時間
            LogRecord logRecord = logRecRepo.GetAll().Where(m => m.MemberId == member.ID)
                                    .OrderByDescending(m => m.LoginTime)
                                    .FirstOrDefault();
            logRecord.LogoutTime = DateTime.Now;
            logRecRepo.Update(logRecord);
        }

        public void CreateAccount(int ID)
        {
            //創建使用者的功能權限
            Feature feature = new Feature
            {
                MemberId = ID,
                FeatInfor = true,
                FeatLogRec = true
            };
            featRepo.Create(feature);
            
        }
        
    }
}
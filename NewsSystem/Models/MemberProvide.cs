﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Models
{
    /// <summary>
    /// Members表操作类
    /// </summary>
    public class MemberProvide : IProvider<Member>
    {
        private NewsDBEntities1 db = new NewsDBEntities1();
        public int Delete(Member t)
        {
            if (t == null) return 0;
            var model = db.Member.ToList().FirstOrDefault(item => item.Id == t.Id);
            if (model == null) return 0;
            db.Member.Remove(model);
            int count = db.SaveChanges();
            return count;
        }

        public int Insert(Member t)
        {
            if (t == null) return 0;
            db.Member.Add(t);
            int count = db.SaveChanges();
            return count;
        }

        public List<Member> Select()
        {
            return db.Member.ToList();
        }

        public int Update(Member t)
        {
            if (t == null) return 0;
            var model = db.Member.ToList().FirstOrDefault(item => item.Id == t.Id);
            if (model == null) return 0;
            model.Password = t.Password;
            int count = db.SaveChanges();
            return count;
        }
    }
}
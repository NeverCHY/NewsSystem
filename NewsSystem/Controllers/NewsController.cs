using NewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            List<News> news = new NewsProvider().Select();
            return View(news);
        }

        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 用户验证
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckUser()
        {
            string username = Request["user"].ToString();
            string password = Request["pass"].ToString();
            MemberProvide memberProvider = new MemberProvide();
            var model = memberProvider.Select().FirstOrDefault(t => t.Name == username && t.Password == password);
            if(model!=null)
            {
                return Content("登录成功");
            }
            else
            {
                return Content("登录失败");
            }
            
        }
        /// <summary>
        /// 删除一条新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteNews(int id)
        {
            var newsProvider = new NewsProvider();
            var model = newsProvider.Select().FirstOrDefault(item => item.Id == id);
            if(model!=null)
            {
                newsProvider.Delete(model);
            }
           
            return RedirectToAction("Index");
        }
        
        public ActionResult EditNews(int id)
        {
            var newsProvider = new NewsProvider();
            var model = newsProvider.Select().FirstOrDefault(item => item.Id == id);
            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }
        /// <summary>
        /// 提交新闻
        /// </summary>
        /// <returns></returns>
        public ActionResult EditNewsAction()
        {
            string id = Request["id"].ToString();
            string title = Request["title"].ToString();
            string desc = Request["desc"].ToString();
           
            if(id==null||int.TryParse(id,out int result)==false)
            {
                return Content("修改失败");
            }
            else if(string.IsNullOrEmpty(title)==true)
            {
                return Content("修改失败");
            }
            else if(string.IsNullOrEmpty(desc)==true)
            {
                return Content("修改失败");
            }
            var provider = new NewsProvider();
            var model = provider.Select().FirstOrDefault(t => t.Id == result);
            if (model != null)
            {
                model.Title = title;
                model.Text = desc;
                var count = provider.Update(model);
                if(count>0)
                return Content("登录成功");
                else
                    return Content("登录失败");
            }
            else
            {
                return Content("登录失败");
            }
        }

        public ActionResult AddNewsAction()
        {
            string title = Request["title"].ToString();
            string desc = Request["desc"].ToString();
            if (string.IsNullOrEmpty(title) == true|| string.IsNullOrEmpty(desc) == true)
            {
                return Content("添加失败");
            }
            var model = new News();
            model.Title = title;
            model.Text = desc;
            model.InsertDate = DateTime.Now;
            model.MemberId = 1;
            model.MemberName = "admin";
            var count = new NewsProvider().Insert(model);
            if (count>0)
            {
              
                    return Content("添加成功");
                
            }
            else
            {
                return Content("添加失败");
            }
        }
        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult UserManagement()
        {
            return View(new MemberProvide().Select());
        }

        public ActionResult DeleteMember(int id)
           
        {
            var provider = new MemberProvide();
            var model = provider.Select().FirstOrDefault(item => item.Id == id);
            if (model != null)
            {
                provider.Delete(model);
            }

            return RedirectToAction("UserManagement");
        }

    }
}
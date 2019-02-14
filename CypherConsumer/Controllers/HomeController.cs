using CypherConsumer.ApiHelper;
using CypherConsumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CypherConsumer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetTodos()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTodo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTodo(TodoList item)
        {
            var newItem = ApiConnection.PostDataToApi<TodoList>(item, URL.IISUrl, "todo/PostTodoItem");
            return RedirectToAction("GetTodos");
        }

        [HttpGet]
        public ActionResult EditTodo(int id)
        {
            TodoList item = new TodoList();
            item.Id = id;
            item = ApiConnection.GetDataFromApi<TodoList>(URL.IISUrl, "todo/GetTodoItem/"+id);
            return View(item);
        }

        [HttpPost]
        public ActionResult EditTodo(TodoList item)
        {
            item = ApiConnection.PostDataToApi<TodoList>(item, URL.IISUrl, "todo/PutTodoItem/" + item.Id);
            return RedirectToAction("GetTodos");
        }

        //[HttpDelete]
        public ActionResult DeleteTodo(int id)
        {
            var item = new TodoList();
            item.Id = id;
            item = ApiConnection.PostDataToApi<TodoList>(item, URL.IISUrl, "todo/DeleteTodoItem");
            return RedirectToAction("GetTodos");
        }
    }
}
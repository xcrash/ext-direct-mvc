using System;
using System.Web.Mvc;
using Ext.Direct.Mvc;

namespace TestApplication.Controllers {

    [HandleError]
    [DirectIgnore]
    public class HomeController : Controller {

        public ActionResult Index() {
            return View();
        }
    }
}

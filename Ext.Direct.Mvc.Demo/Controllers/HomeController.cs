using System;
using System.Web.Mvc;
using Ext.Direct.Mvc;

namespace Ext.Direct.Mvc.Demo.Controllers {

    [HandleError]
    [DirectIgnore]
    public class HomeController : Controller {

        public ActionResult Index() {
            return View();
        }
    }
}

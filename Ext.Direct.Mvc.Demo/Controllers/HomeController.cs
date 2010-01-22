using System;
using System.Web.Mvc;

namespace Ext.Direct.Mvc.Demo.Controllers {

    public class HomeController : Controller {

        [DirectIgnore]
        [HandleError]
        public ActionResult Index() {
            return View();
        }
    }
}

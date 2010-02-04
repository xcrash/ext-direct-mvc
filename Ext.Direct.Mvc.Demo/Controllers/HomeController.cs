using System;
using System.Web.Mvc;

namespace Ext.Direct.Mvc.Demo.Controllers {

    [DirectIgnore]
    public class HomeController : Controller {

        [HandleError]
        public ActionResult Index() {
            return View();
        }
    }
}

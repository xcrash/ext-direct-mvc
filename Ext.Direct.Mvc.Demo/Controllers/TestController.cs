using System;
using System.Web;
using System.IO;
using System.Collections;
using System.Web.Mvc;
using Ext.Direct.Mvc.Demo.Code;
using Newtonsoft.Json.Converters;

namespace Ext.Direct.Mvc.Demo.Controllers {

    public class TestController : Controller {

        public DirectResult SayHello() {
            return this.Direct("Hello, world!");
        }

        public DirectResult EchoDate(DateTime date) {
            return this.Direct(date);
        }

        public DirectResult AddNumbers(int a, int b) {
            return this.Direct(a + b);
        }

        public DirectResult EchoPerson(Person person) {
            return this.Direct(person, new JavaScriptDateTimeConverter(), new StringEnumConverter());
        }

        public DirectResult TestException() {
            var e = new DirectException("This statement is the original exception message.");
            e.Data.Add("stringInfo", "Additional string information.");
            e.Data["intInfo"] = -903;
            e.Data["dateTimeInfo"] = DateTime.Now;
            throw e;

            return this.Direct("This line is never reached.");
        }

        [ActionName("LoadPerson")] // Action alias
        public DirectResult LoadForm() {
            var person = new Person {
                Name = "John Smith",
                Email = "john.smith@example.com",
                Gender = Gender.Male,
                Birthday = new DateTime(1969, 12, 31),
                Salary = 55000
            };
            return this.Direct(new {
                success = true,
                data = person
            });
        }

        [FormHandler]
        [ActionName("SavePerson")] // Action alias
        public DirectResult SaveForm(Person p) {
            return this.Direct(new {
                success = true,
                data = p
            }, new StringEnumConverter());
        }

        [FormHandler]
        public DirectResult UploadFiles(string firstName, string lastName) {
            var files = Request.Files;

            foreach (string file in files) {
                HttpPostedFileBase hpf = files[file];
                if (hpf.ContentLength == 0)
                    continue;
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploaded");
                string savedFileName = Path.Combine(folderPath, Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);
            }

            return this.Direct(new {
                success = true,
                data = new {
                    FirstName = firstName,
                    LastName = lastName
                }
            });
        }

        public DirectResult GetTree(string nodeId) {
            var array = new ArrayList();
            if (nodeId == "root") {
                for (int i = 0; i <= 5; i++) {
                    array.Add(new {
                        id = "n" + i,
                        text = "Node " + i,
                        leaf = false
                    });
                }
            } else if (nodeId.Length == 2) {
                var num = nodeId.Substring(1);
                for (int i = 0; i <= 5; i++) {
                    array.Add(new {
                        id = nodeId + i,
                        text = "Node " + num + i,
                        leaf = true
                    });
                }
            }
            return this.Direct(array.ToArray());
        }
    }
}

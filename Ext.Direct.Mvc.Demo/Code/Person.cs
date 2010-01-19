using System;

namespace Ext.Direct.Mvc.Demo.Code {

    public enum Gender {
        Male = 1,
        Female = 2
    }

    public class Person {
        public string Name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public float Salary { get; set; }
    }
}

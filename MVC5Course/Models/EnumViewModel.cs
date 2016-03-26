using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public enum status
    {
        A,
        B,
        C
    };

    public class EnumViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public status status { get; set; }
    }
}
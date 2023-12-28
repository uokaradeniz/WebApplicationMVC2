using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationMVC2.Models
{
    public class DropDownViewModel
    {
        public int SelectedItemId { get; set; }
        public List<SelectListItem> templates { get; set; }

        public DropDownViewModel()
        {
            templates = new List<SelectListItem>();
        }
    }
}
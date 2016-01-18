using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELand.Models.PropertySteps
{
    public class Step2
    {
        public HttpPostedFileBase MainImage { get; set; }
        public HttpPostedFileBase[] GalleryImages { get; set; }
    }
}
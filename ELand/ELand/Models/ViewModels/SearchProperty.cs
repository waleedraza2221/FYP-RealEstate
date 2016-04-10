using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELand.Models.ViewModels
{
    public class SearchProperty
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int PropertyStatus { get; set; }

        public int PropertyType { get; set; }
        public string Price { get; set; }

        public string BedRoom { get; set; }

        public string BathRoom { get; set; }

    }
}
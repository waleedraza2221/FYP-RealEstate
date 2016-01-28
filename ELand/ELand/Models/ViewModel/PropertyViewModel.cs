﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELand.Models.ViewModel
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MainImage { get; set; }
        public List<string> GalleryImages { get; set; }
        public string Area { get; set; }
        public string Price { get; set; }
        public int AreaID { get; set; }
        public int TypeID { get; set; }
        public int PurposeID { get; set; }
        public int Bed { get; set; }
        public int Bath { get; set; }
        public int Kitchen { get; set; }
        public int Garage { get; set; }
        public int Pool { get; set; }
        public bool Air_Conditioning { get; set; }
        public bool Fridge { get; set; }
        public bool Kitchen_With_Island { get; set; }
        public bool ADSL_Cable { get; set; }
        public bool Grill { get; set; }
        public bool Exotic_Garden { get; set; }
        public bool WiFi { get; set; }
        public bool Full_HD_TV { get; set; }
        public bool Guest_House { get; set; }
        public bool HiFi_Audio { get; set; }
        public bool Digital_Antenna { get; set; }
        public string UserId { get; set; }
      
    }
}
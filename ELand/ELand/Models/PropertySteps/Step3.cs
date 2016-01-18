using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELand.Models.PropertySteps
{
    public class Step3
    {
        public string Price { get; set; }
        public string Area { get; set; }
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
    }
}
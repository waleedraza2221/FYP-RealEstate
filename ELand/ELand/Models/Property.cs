using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ELand.Models
{
    public class Property
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
            [Required]
        public string Address { get; set; }
            [Required]
        public string Latitude { get; set; }
            [Required]
        public string Longitude { get; set; }
        public string MainImage { get; set; }
        public string GalleryImages { get; set; }
            [Required]
        public string Area { get; set; }
            [Required]
        public string Price { get; set; }

            public string GlobalId { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        [Required]
        public int AreaID { get; set; }
        [ForeignKey("AreaID")]

        public virtual AreaUnit AreaUnit { get; set; }
        [Required]
        public int TypeID { get; set; }
        [ForeignKey("TypeID")]
        public virtual PType PType { get; set; }
        [Required]
        public int PurposeID { get; set; }
        [ForeignKey("PurposeID")]
        public virtual Purpose Purpose { get; set; }

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
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [Required]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }
        


    }
}
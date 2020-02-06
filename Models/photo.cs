using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace galleryprojectb.Models
{
    public class photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required")]

        public int GalleryID{ get; set; }

        public string ImageName { get; set; }

        [DisplayName("Photo")]
        public string Image { get; set; }

       
    }
}
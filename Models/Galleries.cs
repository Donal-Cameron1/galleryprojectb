using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace galleryprojectb.Models
{
    public class Galleries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GalleryID { get; set; }
        public string GalleryName { get; set; }

        [DisplayName("Creator")]
        public string GalleryCreator { get; set; }
    }
}
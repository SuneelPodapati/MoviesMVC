using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMDB.Models
{
    [MetadataType(typeof(ProducerMetaData))]
    public partial class Producer
    {
        
    }

    public class ProducerMetaData
    {
        
        public int ProducerId { get; set; }
        [Display(Name = "Producer Name")]
        [Required]
        public string ProducerName { get; set; }

        [Display(Name = "Gender")]
        [Required]
        public string Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Display(Name = "Bio")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        public string Bio { get; set; }
    }
}
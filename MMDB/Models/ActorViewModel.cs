using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMDB.Models
{
    [MetadataType(typeof(ActorMetaData))]
    public partial class Actor
    {

    }

    public class ActorMetaData
    {

        public int ActorId { get; set; }
        [Display(Name = "Actor Name")]
        [Required]
        public string ActorName { get; set; }

        [Display(Name = "Gender")]
        [Required]
        [UIHint("Gender")]
        public string Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Display(Name = "Bio")]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }

        [Display(Name ="Movies")]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
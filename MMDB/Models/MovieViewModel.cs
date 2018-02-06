using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMDB.Models
{

    public class MovieViewModel
    {
        public Movie Movie { get; set; }
        [Display(Name = "Actors")]
        public IEnumerable<SelectListItem> AllActors { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> AllProducers { get; set; } = new List<SelectListItem>();
        public List<int> SelectedMovieActorsIds { get; set; } = new List<int>();

    }






    [MetadataType(typeof(MovieMetaData))]
    public partial class Movie
    {

    }

    public class MovieMetaData
    {
        public int MovieId { get; set; }

        [Required]
        [Display(Name = "Movie Name")]
        public string MovieName { get; set; }

        [Required]
        [Display(Name = "Year Of Release")]
        public int YearOfRelease { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Plot")]
        [DataType(DataType.MultilineText)]
        public string Plot { get; set; }

        [Display(Name = "Poster")]
        [UIHint("Poster")]
        public string Poster { get; set; }

        [Required]
        [Display(Name = "Producer")]
        public int ProducerId { get; set; }

        [Display(Name = "Producer")]
        public virtual Producer Producer { get; set; }

        [Display(Name = "Actors")]
        [UIHint("Actors")]
        public virtual ICollection<Actor> Actors { get; set; }
    }
}
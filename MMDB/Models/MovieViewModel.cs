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
        public IEnumerable<SelectListItem> AllActors { get; set; }

        private List<int> _selectedActors;
        public List<int> SelectedActors
        {
            get
            {
                if (_selectedActors == null)
                {
                    _selectedActors = Movie.Actors.Select(m => m.ActorId).ToList();
                }
                return _selectedActors;
            }
            set { _selectedActors = value; }
        }
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
        public string Plot { get; set; }

        [Display(Name = "Poster")]
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
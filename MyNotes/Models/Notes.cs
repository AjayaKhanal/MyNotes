using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MyNotes.Models
{
    public class Notes
    {
        public int NoteId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

    }
}

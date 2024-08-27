using Microsoft.AspNetCore.Mvc;
using MyNotes.Models;
using System.Collections;
using System.Diagnostics;

namespace MyNotes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static List<Notes> notesList = new List<Notes>();


        //Add data to the list
        public List<Notes> NotesList { get => notesList; set => notesList = value; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Main page
        public IActionResult Index(string? searchkey)
        {
            List<Notes> filteredNotes;

            if (searchkey != null)
            {
                filteredNotes = NotesList.Where(
                    note => note.Title.Contains(searchkey, StringComparison.OrdinalIgnoreCase) ||
                        note.Description.Contains(searchkey, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
            }
            else
            {
                filteredNotes = notesList;
            }
            return View(filteredNotes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Add note page
        [HttpGet]
        public IActionResult AddNotes()
        {
            return View();
        }

        // Create new note
        [HttpPost]
        public IActionResult AddNotes(Notes newNote)
        {
            //generate id for note
            newNote.NoteId = notesList.Count > 0 ? notesList.Max(n => n.NoteId) + 1 : 1;

            notesList.Add(newNote);

            return RedirectToAction("Index");
        }

        //Edit Note page
        [HttpGet]
        public IActionResult EditNotes(int? noteId)
        {
            if (noteId != null)
            {
                var note = NotesList.FirstOrDefault(i => i.NoteId == noteId);

                if (note != null)
                {
                    return View(note);
                }
            }
            return RedirectToAction("Index");
        }

        //Edit note
        [HttpPost]
        public IActionResult EditNotes(Notes notes)
        {
            var note = NotesList.FirstOrDefault(i => i.NoteId == notes.NoteId);

            if (note != null)
            {
                note.Title = notes.Title;
                note.Description = notes.Description;
            }

            return RedirectToAction("Index");
        }

        //Delete Note Page
        [HttpGet]
        public IActionResult DeleteNote(int? noteId)
        {
            if (noteId != null)
            {
                var note = NotesList.FirstOrDefault(i => i.NoteId == noteId);

                if (note != null) 
                {
                    return View(note);
                }
            }
            return RedirectToAction("Index");
        }

        //Delete Note
        [HttpPost]
        public IActionResult DeleteNote(Notes notes)
        {
            var note = NotesList.FirstOrDefault(i=>i.NoteId == notes.NoteId);

            if (note != null)
            {
                NotesList.Remove(note);
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
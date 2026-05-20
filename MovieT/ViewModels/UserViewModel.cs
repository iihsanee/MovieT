using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MovieT.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Gebruikersnaam { get; set; } = string.Empty;
        public string Wachtwoord { get; set; } = string.Empty;
        public string BevestigWachtwoord { get; set; } = string.Empty;
        public List<WatchingListViewModel> WatchingList { get; set; } = new();
        public List<WatchedListViewModel> WatchedList { get; set; } = new();

       
    }
}
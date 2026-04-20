using System.Collections.Generic;
using MovieT.ViewModels;

namespace MovieT.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<WatchingListViewModel> WatchingList { get; set; } = new List<WatchingListViewModel>();
        public List<WatchedListViewModel> WatchedList { get; set; } = new List<WatchedListViewModel>();
    }
}

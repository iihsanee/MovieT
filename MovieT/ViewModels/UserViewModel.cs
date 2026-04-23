using System.Collections.Generic;
namespace MovieT.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; } = string.Empty;
        public List<WatchingListViewModel> WatchingList { get; set; } = new List<WatchingListViewModel>();
        public List<WatchedListViewModel> WatchedList { get; set; } = new List<WatchedListViewModel>();
    }
}
using Instagram.Web.Entities;
using System.Collections.Generic;

namespace Instagram.Web.Areas.Client.Models.User
{
    public class SearchViewModel:BaseViewModel
    {
        public ICollection<RegularUser> Results { get; set; }
    }
}

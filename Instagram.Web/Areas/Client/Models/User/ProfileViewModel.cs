using Instagram.Web.Entities;
using System.Collections.Generic;

namespace Instagram.Web.Areas.Client.Models.User
{
    public class ProfileViewModel:BaseViewModel
    {
        public RegularUser User { get; set; }
        public ICollection<RegularUser> Followers { get; set; }
        public ICollection<RegularUser> Follows { get; set; }
        public ICollection<Entities.Post> Posts { get; set; }
    }
}

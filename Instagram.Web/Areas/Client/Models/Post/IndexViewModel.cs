using System.Collections.Generic;

namespace Instagram.Web.Areas.Client.Models.Post
{
    public class IndexViewModel:BaseViewModel
    {
        public ICollection<Entities.Post> Posts { get; set; }
    }
}

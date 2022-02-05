using System.Collections.Generic;

namespace Instagram.Web.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int PublisherId { get; set; }
        public RegularUser Publisher { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public ICollection<PostLikes> PostLikes { get; set; }
    }
}

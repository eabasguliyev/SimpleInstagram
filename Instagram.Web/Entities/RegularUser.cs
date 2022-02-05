using System.Collections.Generic;

namespace Instagram.Web.Entities
{
    public class RegularUser: User
    {
        public string Description { get; set; }
        public string ProfileImageUrl { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<PostLikes> PostLikes { get; set; }
        public ICollection<Friendship> Follows { get; set; }
        public ICollection<Friendship> Followers { get; set; }
    }
}

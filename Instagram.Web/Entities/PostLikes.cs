namespace Instagram.Web.Entities
{
    public class PostLikes
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public RegularUser User { get; set; }
    }
}

namespace Instagram.Web.Entities
{
    public enum FriendshipRequestEnum
    {
        Pending, Approved, Cancelled
    }
    public class Friendship
    {
        public int Id { get; set; }
        public int FromId { get; set; }
        public RegularUser From { get; set; }
        public int ToId { get; set; }
        public RegularUser To { get; set; }
        public FriendshipRequestEnum Status { get; set; }
    }
}

namespace My_Forum_Web.Models
{
    public class ForumMsg
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public System.DateTime Date_Added { get; set; }
        public int UserId { get; set; }
        public virtual MyUser User { get; set; }
    }
}
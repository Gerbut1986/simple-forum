namespace My_Forum_Web.Models
{
    public class MyUser
    {
        public int Id { get; set; }
        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public System.DateTime Date_Register { get; set; }
        public virtual System.Collections.Generic.ICollection<ForumMsg> ForumMsgs { get; set; }
    }
}
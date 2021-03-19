namespace My_Forum_Web.Models
{
    using System.Data.Entity;

    public class MyContext : DbContext
    {
        public MyContext() : base("DefaultConnection") { }

        public virtual DbSet<MyUser> Users { get; set; }
        public virtual DbSet<ForumMsg> ForumMsgs { get; set; }
    }
}
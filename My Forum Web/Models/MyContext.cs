namespace My_Forum_Web.Models
{
    using System.Data.Entity;

    public class MyContext : DbContext
    {
        public MyContext() : base("connection") { }

        public virtual DbSet<MyUser> Users { get; set; }
        public virtual DbSet<ForumMsg> ForumMsgs { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Domain.Entities;


namespace Azmoon.Persistence.Contexts
{
    public class DataBaseContext : IdentityDbContext<User, Role, string>, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


           // modelBuilder.Entity<UserLogin>().HasKey(p => new { p.ProviderKey, p.LoginProvider });
            //modelBuilder.Entity<UserRole>().HasKey(p => new { p.UserId, p.RoleId });
           // modelBuilder.Entity<UserToken>().HasKey(p => new { p.UserId, p.LoginProvider });
            modelBuilder.Entity<User>().Ignore(p => p.NormalizedEmail);
            modelBuilder.Entity<Answer>().Property(p => p.UserId).IsRequired();



            modelBuilder.Entity<Answer>()
  .HasOne(p => p.Question)
  .WithMany(p => p.Answers)
  .HasConstraintName("FK_Question_AnswerQuestions_onetomany")
  .OnDelete(DeleteBehavior.NoAction);




            modelBuilder.Entity<Quiz>()
                .HasOne(p=>p.Passworddd)
                .WithOne(p=>p.Quiz)
                .HasForeignKey<Password>(c => c.QuizId);

            modelBuilder.Entity<Quiz>()
          .HasOne(p => p.QuizFilter)
          .WithOne(p => p.Quiz)
          .HasForeignKey<QuizFilter>(c => c.QuizId);
            //add defulte role and user
            initUser(modelBuilder);

            // modelBuilder.TicketSeed();
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Question> Qestions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<persons> Persons { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<QuizStartTemp>QuizStartTemps  { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WorkPlace> WorkPlaces { get; set; }
        public virtual DbSet<QuizFilter> QuizFilters { get; set; }
        private void initUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Ignore(p => p.Email);
            modelBuilder.Entity<User>().Ignore(p => p.EmailConfirmed);
            modelBuilder.Entity<User>().Ignore(p => p.NormalizedEmail);


            modelBuilder.Entity<User>().Property(p => p.FirstName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<User>().Property(p => p.LastName).HasColumnType("nvarchar(50)");

            modelBuilder.Entity<User>().Property(p => p.Phone).HasColumnType("nvarchar(11)");
            modelBuilder.Entity<User>().Property(p => p.UserName).HasColumnType("nvarchar(10)");



        }
    }
}

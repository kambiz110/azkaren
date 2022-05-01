using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azmoon.Domain.Entities;

namespace Azmoon.Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {
        DbSet<Domain.Entities.Role> Roles  { get; set; }
        DbSet<Domain.Entities.User> Users { get; set; }
        DbSet<Domain.Entities.Attachment> Attachments { get; set; }
        DbSet<Domain.Entities.Question> Qestions { get; set; }
        DbSet<Domain.Entities.Answer> Answers { get; set; }
        DbSet<Domain.Entities.Quiz> Quizzes { get; set; }
        DbSet<Domain.Entities.Result> Results { get; set; }
        DbSet<Domain.Entities.Group> Groups { get; set; }
        DbSet<Domain.Entities.persons> Persons { get; set; }
        DbSet<Domain.Entities.Password> Passwords { get; set; }
        DbSet<Domain.Entities.WorkPlace> WorkPlaces { get; set; }
        DbSet<Domain.Entities.QuizFilter> QuizFilters { get; set; }
        DbSet<Domain.Entities.QuizStartTemp> QuizStartTemps { get; set; }
        DbSet<Domain.Entities.GroupUser> GroupUsers { get; set; }
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }
}

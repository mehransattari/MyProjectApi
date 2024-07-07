
using Entittes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Common.Utilities;
using System.Reflection;

namespace Data.Context;

public class ApplicationDbContext :DbContext //IdentityDbContext<User, Role, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = typeof(IEntity).Assembly;

        //برای ثبت اتومات مدل ها
        //  public DbSet<Role> Roles { get; set; }
        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);

        // برای ثبت اتوامتیک مپ ها 
        //بجای نوشتن تک تک مثل کد زیر 
        // modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);

        //حذف مدل ها در صورت ارتباط داشتن با مدل دیگر اجازه ندهد حذف کند
        modelBuilder.AddRestrictDeleteBehaviorConvention();

        //هرجا از گیود استفاده کنیم بجاش سکیونشال گیود بزاریم
        //چون سکونشال باعث میشه این گیود های تولید شده بر اساس زمان مشخص وبه شکل تصاعدی بالا برندو ایندکس بالایی دارند
        // modelBuilder.Entity<Post>().Property(x=>x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
        modelBuilder.AddSequentialGuidForIdConvention();

        //جمع کردن نام های اتوماتیک مدل ها
        modelBuilder.AddPluralizingTableNameConvention();
    }

    public override int SaveChanges()
    {
        _cleanString();

        return base.SaveChanges();  
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        _cleanString();

        return base.SaveChanges(acceptAllChangesOnSuccess); 
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        _cleanString();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken); 
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _cleanString();

        return base.SaveChangesAsync(cancellationToken);    
    }

    /// <summary>
    /// برای اینکه اعداد و رشته ها استاندارد باشند
    /// </summary>
    private void _cleanString()
    {
        var changedEntities = ChangeTracker.Entries()
                                           .Where(x => x.State == EntityState.Added ||
                                                       x.State == EntityState.Modified);

        foreach (var item in changedEntities)
        {
            if (item.Entity == null)
            {
                continue;
            }

            var properties = item.Entity.GetType()
                                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var propName = property.Name;

                if(propName == null)
                {
                    continue;
                }

                var val = property.GetValue(item.Entity, null) as string;

                if (val.HasValue())
                {
                    var newVal = val.Fa2En().FixPersianChars();

                    if (newVal == val)
                    {
                        continue;
                    }

                    property.SetValue(item.Entity, newVal, null);
                }
            }
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace web_app_asp_net_mvc_database_first.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TimetableEntities : DbContext
    {
        public TimetableEntities()
            : base("name=TimetableEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<TeacherImages> TeacherImages { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }
    }
}
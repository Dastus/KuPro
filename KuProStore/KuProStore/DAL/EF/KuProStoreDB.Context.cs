﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KuProStore.DAL.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StoreDBcontext : DbContext
    {
        public StoreDBcontext()
            : base("name=StoreDBcontext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ImageEntity> Images { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<RegionEntity> Regions { get; set; }
        public virtual DbSet<TownEntity> Towns { get; set; }
        public virtual DbSet<UserEntity> UserEntities { get; set; }
    }
}
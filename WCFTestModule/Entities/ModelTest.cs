namespace WCFTestModule.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelTest : DbContext
    {
        public ModelTest()
            : base("name=ModelTest")
        {
        }

        public virtual DbSet<test> test { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

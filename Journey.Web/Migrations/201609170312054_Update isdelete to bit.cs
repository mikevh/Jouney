namespace Journey.Web.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Updateisdeletetobit : DbMigration
    {
        public override void Up()
        {
            this.DeleteDefaultContraint("Meetings", "IsDeleted");
            AlterColumn("dbo.Meetings", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meetings", "IsDeleted", c => c.Int(nullable: false));
        }
    }
}

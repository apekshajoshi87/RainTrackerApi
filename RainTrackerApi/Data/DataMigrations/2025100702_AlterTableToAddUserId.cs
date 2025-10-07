using FluentMigrator;

namespace RainTrackerApi.Data.DataMigrations
{
    [Migration(2025100702)]
    public class _2025100702_AlterTableToAddUserId : Migration
    {
        public override void Up()
        {
            if (Schema.Table("rain").Exists() && !Schema.Table("rain").Column("userid").Exists())
            {
                Alter.Table("rain").AddColumn("userid").AsString().NotNullable();
            }
        }

        public override void Down()
        {
            if (Schema.Table("rain").Column("userid").Exists())
            {
                Delete.Column("userid").FromTable("rain");
            }
        }
    }
}

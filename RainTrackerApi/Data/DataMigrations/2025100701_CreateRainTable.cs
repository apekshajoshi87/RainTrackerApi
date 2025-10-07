using FluentMigrator;

namespace RainTrackerApi.Data.DataMigrations
{
    [Migration(2025100701)]
    public class CreateRainTable : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("rain").Exists())
            {
                Create.Table("rain")
                    .WithColumn("rainid").AsInt32().PrimaryKey().Identity()
                    .WithColumn("datetimestamp").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                    .WithColumn("itrained").AsBoolean().NotNullable();
            }
        }

        public override void Down() 
        {
            if (Schema.Table("rain").Exists())
            {
                Delete.Table("rain");
            }
        }
    }
}

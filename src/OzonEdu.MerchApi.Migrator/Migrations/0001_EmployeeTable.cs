using FluentMigrator;
using FluentMigrator.Postgres;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(1)]
    public class EmployeeTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists employees(
                    id SERIAL PRIMARY KEY,
                    status_id INT NOT NULL,
                    first_name TEXT NOT NULL CHECK (last_name ~ '^[A-Za-zА-Яа-я]'),
                    last_name TEXT NOT NULL CHECK (last_name ~ '^[A-Za-zА-Яа-я]'),
                    middle_name TEXT CHECK (middle_name ~ '^[A-Za-zА-Яа-я]'),
                    email TEXT NOT NULL UNIQUE CHECK(email ~ '^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+[.][A-Za-z]+$'));"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists employees;");
        }
    }
}
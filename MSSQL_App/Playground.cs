using System;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace MSSQL_App
{
    public class Playground
    {
        private void Test1()
        {
            var sql1 = @"--Add column GUID to each table--
EXEC sp_MSforeachtable '
if not exists (select * from sys.columns 
				where object_id = object_id(''?'')
				and name = ''diagram_id'')
alter table ? ADD GUID uniqueidentifier NOT NULL DEFAULT NEWID()'

--Create temporary table with Primary Keys (in case of different PK Columns in each table)--
SELECT  i.name AS IndexName,
        OBJECT_NAME(ic.OBJECT_ID) AS TableName,
        COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName
into #tempPK
FROM    sys.indexes AS i INNER JOIN 
        sys.index_columns AS ic ON  i.OBJECT_ID = ic.OBJECT_ID
									AND i.index_id = ic.index_id
WHERE   i.is_primary_key = 1

--Create temporary table with Foreign Keys where referenced table column is Primary Key--
SELECT	f.name AS ForeignKey, OBJECT_NAME(f.parent_object_id) AS TableName,
		COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName,
		OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName,
		COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferenceColumnName
into #temp
FROM	sys.foreign_keys AS f INNER JOIN 
		sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id
		JOIN #tempPK as pk ON pk.TableName = OBJECT_NAME (f.referenced_object_id) 
							AND COL_NAME(fc.referenced_object_id, fc.referenced_column_id) = pk.ColumnName
--Loop through #temp--
BEGIN
declare 
@tableParent varchar(max),
@tableReferenced varchar(max),
@columnValues varchar(max),
@columnID varchar(max),
@foreignKey varchar(max),
@newColumnName varchar(max)

DECLARE FKs CURSOR LOCAL FOR (select TableName, ReferenceTableName, ColumnName, ReferenceColumnName, ForeignKey from #temp)
declare @sql nvarchar(max),
@sql2 nvarchar(max),
@sql4 nvarchar(max),
@sql3 nvarchar(max),
@sql6 nvarchar(max)

OPEN FKs
FETCH NEXT FROM FKs into @tableParent, @tableReferenced, @columnValues, @columnID, @foreignKey
WHILE @@FETCH_STATUS = 0
BEGIN
	set @newColumnName = @columnValues+'_'+@tableReferenced+'_'+@tableParent+'_GUID'

	--Add column to Parent table (ex. Values_Table_1_Table_2_GUID)--
	set @sql6 = 'ALTER TABLE '+@tableParent+'
		ADD  '+@newColumnName +' uniqueidentifier NOT NULL DEFAULT NEWID()'

	--Set values to Parent table column (ex. Values_Table_1_Table_2_GUID) equal to Referenced GUID column values--
	set @sql4 = 'update '+@tableParent+' set '+@newColumnName+'='+@tableReferenced+'.GUID
		from '+@tableReferenced+ ' join '+@tableParent+' on '+@tableParent+'.'
		+@columnValues+'='+@tableReferenced+'.'+@columnID

	--Add Foreign Key from GUID to Parent table column (ex. Values_Table_1_Table_2_GUID)--
	set @sql = 'ALTER TABLE '+@tableReferenced+'
		ADD CONSTRAINT '+@tableReferenced+@foreignKey+'_GUID UNIQUE (GUID)'

	set @sql2 = 'ALTER TABLE '+@tableParent+'
		ADD FOREIGN KEY ('+@newColumnName+')
		REFERENCES '+@tableReferenced+'(GUID)'

	--Drop Foreign Key from ID to Values--
	set @sql3 = 'ALTER TABLE '+@tableParent+'
		DROP CONSTRAINT '+@foreignKey

	EXEC sp_executeSQL @sql6
	EXEC sp_executeSQL @sql4
	EXEC sp_executeSQL @sql
	EXEC sp_executeSQL @sql2
	EXEC sp_executeSQL @sql3

    FETCH NEXT FROM FKs into @tableParent, @tableReferenced, @columnValues, @columnID, @foreignKey
END

CLOSE FKs
DEALLOCATE FKs
drop table #temp
END

--Drop Primary Key columns and set GUID column to be new Primary Key--
EXEC sp_MSforeachtable '
declare @sql nvarchar(max),
@sql2 nvarchar(max)
if not exists (select * from sys.columns 
				where object_id = object_id(''?'')
				and name = ''diagram_id'')
BEGIN
SELECT	@sql2 = ''alter table ? Drop column '' + ColumnName  + '';'',
		@sql = ''ALTER TABLE ? DROP CONSTRAINT '' + IndexName + '';''
			FROM #tempPK
			WHERE OBJECT_ID(TableName) = OBJECT_ID(''?'');
EXEC sp_executeSQL @sql
EXEC sp_executeSQL @sql2
alter table? ADD PRIMARY KEY(GUID)
END
'
drop table #tempPK ";
        }

        private void Test2()
        {
            using var connection = new SqlConnection(Constants.ConnectionString);

            var command3 = new SqlCommand();
            command3.Connection  = connection;
            command3.CommandText = "sp_executesql";
            command3.CommandType = CommandType.StoredProcedure;
            var parameter3 = new SqlParameter();
            parameter3.ParameterName = "@sql";
            parameter3.SqlDbType     = SqlDbType.NVarChar;
            parameter3.Value         = "select * from Jobs where Salary > @salary";
            var parameter3_1 = new SqlParameter();
            parameter3_1.ParameterName = "@salary";
            parameter3_1.Value         = 1;
            var parameter3_2 = new SqlParameter();
            parameter3_2.ParameterName = "@parameters";
            parameter3_2.SqlDbType     = SqlDbType.NVarChar;
            parameter3_2.Value         = "@salary int";
            command3.Parameters.Add(parameter3);
            command3.Parameters.Add(parameter3_2);
            command3.Parameters.Add(parameter3_1);

            var pizza4      = new SqlParameter("parameter", "Pizza");
            var queryString = "select * from Food where Title = @parameter";
        }

        private void Test3()
        {
            using var connection = new SqlConnection(Constants.ConnectionString);

            var command4 = new SqlCommand();
            command4.Connection  = connection;
            command4.CommandText = "GetSalarySum";
            command4.CommandType = CommandType.StoredProcedure;

            var parameter4_in = new SqlParameter();
            parameter4_in.ParameterName = "@MinSalary";
            parameter4_in.Value         = 1;
            command4.Parameters.Add(parameter4_in);
            command4.Parameters.Add("@AllSalary", SqlDbType.Int).Direction = ParameterDirection.Output;


            connection.Open();
            command4.ExecuteNonQuery();
            Console.WriteLine(Convert.ToInt32(command4.Parameters["@AllSalary"].Value));

            var command5 = new SqlCommand();
            command5.Connection  = connection;
            command5.CommandText = "select @AllSalary = SUM(Salary) from Jobs where Salary >@MinSalary";
            command5.CommandType = CommandType.Text;

            var parameter5_in = new SqlParameter();
            parameter5_in.ParameterName = "@MinSalary";
            parameter5_in.Value         = 1;
            command5.Parameters.Add(parameter5_in);
            command5.Parameters.Add("@AllSalary", SqlDbType.Int).Direction = ParameterDirection.Output;


            connection.Open();
            command5.ExecuteNonQuery();
            Console.WriteLine(Convert.ToInt32(command5.Parameters["@AllSalary"].Value));
        }

        public static SqlDataAdapter CreateSqlDataAdapter(SqlConnection connection)
        {
            // Assumes that connection is a valid SqlConnection object
            var adapter = new SqlDataAdapter();
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            // Create the commands.
            adapter.SelectCommand = new SqlCommand(
                "SELECT Title, Salary FROM Jobs", connection);
            adapter.InsertCommand = new SqlCommand(
                "INSERT INTO Jobs (Title, Salary) " +
                "VALUES (@Title, @Salary)", connection);
            adapter.UpdateCommand = new SqlCommand(
                "UPDATE Jobs SET Title = @Title, Salary = @Salary " +
                "WHERE Title = @oldTitle", connection);
            adapter.DeleteCommand = new SqlCommand(
                "DELETE FROM Jobs WHERE Title = @Title", connection);

            // Create the parameters.
            adapter.InsertCommand.Parameters.Add("@Title",
                SqlDbType.VarChar, 40, "Title");
            adapter.InsertCommand.Parameters.Add("@Salary",
                SqlDbType.VarChar, 40, "Salary");

            adapter.UpdateCommand.Parameters.Add("@Title",
                SqlDbType.VarChar, 40, "Title");
            adapter.UpdateCommand.Parameters.Add("@Salary",
                SqlDbType.VarChar, 40, "Salary");
            adapter.UpdateCommand.Parameters.Add("@oldTitle",
                    SqlDbType.VarChar, 40, "Title").SourceVersion =
                DataRowVersion.Original;

            adapter.DeleteCommand.Parameters.Add("@Title",
                    SqlDbType.Char, 5, "Title").SourceVersion =
                DataRowVersion.Original;

            return adapter;
        }
    }
}
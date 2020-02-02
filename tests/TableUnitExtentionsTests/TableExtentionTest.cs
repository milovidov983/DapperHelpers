using DapperHelpers;
using DapperHelpers.Models;
using System;
using System.Linq;
using Xunit;

namespace TableUnitExtentionsTests {
	public class Users {
		public const string TableName = "Users";

		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime CreatedAt { get; set; }

	}

	public class Database {
		public readonly Table<Users> UsersTable = TableExtentions.Create<Users>(Users.TableName);
	}


	public class TableExtentionTest {
		private readonly Database db;

		public TableExtentionTest() {
			db = new Database();
		}

		[Fact]
		public void CreateTable_AllFieldsArePresentInResultingObject() {
			var usersTable = TableExtentions.Create<Users>(Users.TableName);

			var actual = usersTable.Fields.OrderBy(x => x).ToArray();

			var expected = typeof(Users).GetProperties().Select(prop => prop.Name).OrderBy(x => x).ToArray();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void IncludeField_TheFieldIsIncludedInResult() {
			var actual = db.UsersTable.Clear().Include(f => f.Email).Fields.OrderBy(x => x).ToArray();

			var expected = new[] { nameof(Users.Email) };

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ExcludeField_TheFieldIsExcludedInResult() {
			var actual = db.UsersTable.Exclude(f => f.Email).Fields.OrderBy(x => x).ToArray();

			var expected = typeof(Users).GetProperties()
				.Select(prop => prop.Name)
				.Where(x=>x != nameof(Users.Email))
				.OrderBy(x => x)
				.ToArray();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetField_ReturnedFieldWithTableName() {
			var actual = db.UsersTable.Field(f => f.Email);

			var expected = $"\"{Users.TableName}\".\"{nameof(Users.Email)}\"";

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetFieldShort_ReturnedFieldWithoutTableName() {
			var actual = db.UsersTable.FieldShort(f => f.Email);

			var expected = $"\"{nameof(Users.Email)}\"";

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetFieldName_ReturnedFieldName() {
			var actual = db.UsersTable.FieldName(f => f.Email);

			var expected = nameof(Users.Email);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Select_ReturnedCommaseparatedFieldsWithTableName() {
			var actual = db.UsersTable.Select();

			var expected = @"""Users"".""Id"",""Users"".""Name"",""Users"".""Email"",""Users"".""CreatedAt""";

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SelectInsert_ReturnedInsertSqlNotationFields() {
			var actual = db.UsersTable.Insert();

			var expected = @"@Id,@Name,@Email,@CreatedAt";

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void InsertFromSelect_ReturnedInsertSqlNotationFields() {
			var actual = db.UsersTable.SelectInsert();

			var expected = @"""Id"",""Name"",""Email"",""CreatedAt""";

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Update_ReturnedUpdateSqlNotationFields() {
			var actual = db.UsersTable.Update();

			var expected = @"""Id""=@Id,""Name""=@Name,""Email""=@Email,""CreatedAt""=@CreatedAt";

			Assert.Equal(expected, actual);
		}
	}
}
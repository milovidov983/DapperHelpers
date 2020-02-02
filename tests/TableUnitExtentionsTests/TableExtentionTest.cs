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

		}

		[Fact]
		public void ExcludeField_TheFieldIsExcludedInResult() {

		}

		[Fact]
		public void GetField_ReturnedFieldWithTableName() {

		}

		[Fact]
		public void GetFieldShort_ReturnedFieldWithoutTableName() {

		}

		[Fact]
		public void GetFieldName_ReturnedFieldName() {

		}

		[Fact]
		public void Select_ReturnedCommaseparatedFields() {

		}

		[Fact]
		public void SelectInsert_ReturnedSqlInsertNotationFields() {

		}

		[Fact]
		public void InsertOneField_ReturnedSqlInsertNotationOneField() {

		}

		[Fact]
		public void Update_ReturnedSqlUpdateNotationFields() {

		}
	}
}
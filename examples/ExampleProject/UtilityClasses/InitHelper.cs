using Dapper;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExampleProject.UtilityClasses {
	public class InitHelper {
		public static async Task InitExampleApplicetion() {
			Console.WriteLine("Init example application...");
			using var db = new Database();

			Console.WriteLine("Connect to database...");
			db.ActiveConnection.Open();

			Console.WriteLine("Create example table...");
			var initSqlScript = File.ReadAllText(@"InitScript.sql");

			await db.ActiveConnection.ExecuteAsync(initSqlScript);
			Console.WriteLine("Initialization successful");
		}
	}
}
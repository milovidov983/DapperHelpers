using Dapper;
using DapperHelpers;
using ExampleProject.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleProject {
	public class SelectExample : Context {

		public override async Task ExecuteImpl(Database database) {
			Console.WriteLine("SelectExample. Result SQL:");

			var sql = database.UsersTable
				.Query(x => $@"
	select {x.Select()} 
	from {x.Name}
							"
			);
			Console.WriteLine(sql);

			var resultUser = await database.ActiveConnection.QueryAsync<User>(sql);

			Console.WriteLine("Users selected from the database:");
			Console.WriteLine(JsonConvert.SerializeObject(resultUser));
			Console.WriteLine("\nSelectExample complete");
		}
	}
}
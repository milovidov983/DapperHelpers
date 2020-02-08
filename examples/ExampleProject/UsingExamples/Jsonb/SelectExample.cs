using Dapper;
using DapperHelpers;
using ExampleProject.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleProject.Jsonb {
	public class SelectExample : Context {

		public override async Task ExecuteImpl(Database database) {
			Console.WriteLine("Jsonb SelectExample. Result SQL:");

			var sql = database.UsersJsonbTable
				.Query(x => $@"
								select {x.Select()} 
								from {x.Name}
							"
			);
			Console.WriteLine(sql);

			var resultUser = await database.ActiveConnection.QueryAsync<UserJsonb>(sql);

			Console.WriteLine("Users selected from the database:");
			Console.WriteLine(JsonConvert.SerializeObject(resultUser));
			Console.WriteLine("\nJsonb SelectExample complete");
		}
	}
}
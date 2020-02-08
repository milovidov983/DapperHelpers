using Dapper;
using DapperHelpers;
using ExampleProject.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using DapperHelpers.Extentions;

namespace ExampleProject.Jsonb {
	public class SelectExample : Context {

		public override async Task ExecuteImpl(Database database) {
			Console.WriteLine("Jsonb SelectExample. Result SQL:");

			var sql = database.UsersJsonbTable
				.Query(x => $@"
								select {x.Select()} 
								from {x.Name}
								where {x.Field(f => f.Data).JsonbStr(p=>p.Name)} = @{nameof(UserData.Name)}
							"
			);
			Console.WriteLine(sql);
			var parameters = new { Name = "Bob" };
			var resultUser = await database.ActiveConnection.QueryFirstOrDefaultAsync<UserJsonb>(sql, parameters);

			Console.WriteLine("Users selected from the database:");
			Console.WriteLine(JsonConvert.SerializeObject(resultUser));
			Console.WriteLine("\nJsonb SelectExample complete");
		}
	}
}
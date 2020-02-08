using Dapper;
using DapperHelpers;
using ExampleProject.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ExampleProject.Jsonb {
	public class UpdateExample : Context {

		public override async Task ExecuteImpl(Database database) {
			Console.WriteLine("Jsonb UpdateExample. Result SQL:");

			var sqlSelect = database.UsersJsonbTable
							.Query(x => $@"
								select {x.Select()} 
								from {x.Name}
							"
			);

			var persistedUser = await database.ActiveConnection.QueryFirstAsync<UserJsonb>(sqlSelect);

			var sqlUpdate = database.UsersJsonbTable
				.Exclude(f=>f.Id)
				.Query(x => $@"
								update {x.Name} 
								set {x.Update()}
								where {x.Field(f => f.Id)} = @{nameof(User.Id)}
							"
			);

			persistedUser.Data.Name = "new name";
			persistedUser.Data.Email = "other@example.com";


			Console.WriteLine(sqlUpdate);

			await database.ActiveConnection.ExecuteAsync(sqlUpdate, persistedUser);
			Console.WriteLine("Jsonb UpdateExample complete");



			var sql = database.UsersJsonbTable
							.Query(x => $@"
								select {x.Select()} 
								from {x.Name}
							"
			);

			var resultUser = await database.ActiveConnection.QueryAsync<UserJsonb>(sql);

			Console.WriteLine("After update users selected from the database:");
			Console.WriteLine(JsonConvert.SerializeObject(resultUser));
		}
	}
}
using Dapper;
using DapperHelpers;
using ExampleProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject {
	public class UpdateExample : Context {
		private readonly User newUser = new User {
			Id = 1,
			Name = "new name",
			Email = "other@example.com",
		};

		public override async Task ExecuteImpl(Database database) {
			Console.WriteLine("UpdateExample. Result SQL:");

			var sql = database.UsersTable
				.Exclude(f=>f.RegisteredAt)
				.Query(x => $@"
	update {x.Name} 
	set {x.Update()}
	where {x.Field(f=>f.Id)} = @{nameof(User.Id)}
							"
			);

			Console.WriteLine(sql);

			await database.ActiveConnection.ExecuteAsync(sql, newUser);

			Console.WriteLine("UpdateExample complete");
		}
	}
}
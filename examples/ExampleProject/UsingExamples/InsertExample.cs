using Dapper;
using DapperHelpers;
using ExampleProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject {
	public class InsertExample : Context {
		private readonly User[] exampleUsers = new[] {
			new User {
				Name = "Bob",
				Email = "bob@example.com",
				RegisteredAt = DateTime.UtcNow
			},
			new User {
				Name = "Alisa",
				Email = "alisa@example.com",
				RegisteredAt = DateTime.UtcNow
			}
		};


		public override async Task ExecuteImpl(Database database) {
			Console.WriteLine("InsertExample. Result SQL:");

			var sql = database.UsersTable
				.Exclude(f=>f.Id)
				.Query(x => $@"
	insert into {x.Name} 
	({x.SelectInsert()})
	values
	({x.Insert()})
							"
			);

			Console.WriteLine(sql);

			await database.ActiveConnection.ExecuteAsync(sql, exampleUsers);

			Console.WriteLine("InsertExample complete");
		}
	}
}
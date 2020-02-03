using Dapper;
using DapperHelpers;
using ExampleProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject {
	public class InsertExample : Context {

		public override async Task ExecuteImpl(Database database) {
			Console.WriteLine("InsertExample. Result SQL:");

			var sql = database.UsersTable
				.Exclude(f=>f.Id)
				.Query(x => $@"
								insert into {x.Name} 
								({x.SelectInsert()})
								values
								({x.Insert()})
								returning {x.FieldShort(f=>f.Id)}
							"
			);

			Console.WriteLine(sql);

			var user = new User {
				Name = "Bob",
				Email = "bob@example.com",
				RegisteredAt = DateTime.UtcNow
			};

			var id = await database.ActiveConnection.QuerySingleAsync<int>(sql, user);

			Console.WriteLine($"User inserted with id: {id}");

			Console.WriteLine("InsertExample complete");
		}
	}
}
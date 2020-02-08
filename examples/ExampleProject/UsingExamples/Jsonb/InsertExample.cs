using Dapper;
using DapperHelpers;
using ExampleProject.Models;
using System;
using System.Threading.Tasks;

namespace ExampleProject.Jsonb {
	public class InsertExample : Context {

		public override async Task ExecuteImpl(Database database) {
			Console.WriteLine("Jsonb InsertExample. Result SQL:");

			var sql = database.UsersJsonbTable
				.Exclude(f => f.Id)
				.Query(x => $@"
								insert into {x.Name} 
								({x.SelectInsert()})
								values
								({x.Insert()})
								returning {x.FieldShort(f => f.Id)}
							"
			);

			Console.WriteLine(sql);

			var user = new UserJsonb {
				Data = new UserData {
					Name = "Bob",
					Email = "bob@example.com",
					RegisteredAt = DateTime.UtcNow
				}
			};

			var id = await database.ActiveConnection.QuerySingleAsync<int>(sql, user);

			Console.WriteLine($"User inserted with id: {id}");

			Console.WriteLine("Jsonb InsertExample complete");
		}
	}
}
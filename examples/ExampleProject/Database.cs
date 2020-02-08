using DapperHelpers;
using DapperHelpers.Models;
using ExampleProject.Models;
using Npgsql;
using System;
using System.Data;

namespace ExampleProject {
	public class Database : IDisposable {
		public readonly Table<User> UsersTable = TableExtentions.Create<User>(User.TableName);
		public readonly Table<UserJsonb> UsersJsonbTable = TableExtentions.Create<UserJsonb>(UserJsonb.TableName);

		static Database() {
			/// Here we explain to Dapper how to deal 
			/// with our objects that we want to store jsonb.
			/// 
			/// So it is necessary to do with each object 
			/// of the highest level that we want to store in jsonb format.
			JsonTypeHandler.AddType<UserData>(mutator, Settings.JSS);


			/// A postgres-specific handler for Dapper custom type handler
			static void mutator(IDbDataParameter p, object v) {
				(p as NpgsqlParameter).NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
			}
		}

		public Database() {
			ActiveConnection = GetConnection();
		}

		public IDbConnection ActiveConnection { get; private set; }

		public IDbConnection GetConnection() {
			return new NpgsqlConnection(Settings.Instance.GetConnectionString());
		}

		public void Dispose() {
			ActiveConnection?.Dispose();
		}
	}
}
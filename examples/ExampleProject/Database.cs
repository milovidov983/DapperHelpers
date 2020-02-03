using DapperHelpers;
using DapperHelpers.Models;
using ExampleProject.Models;
using Npgsql;
using System;
using System.Data;

namespace ExampleProject {
	public class Database : IDisposable {
		public readonly Table<User> UsersTable = TableExtentions.Create<User>(User.TableName);

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

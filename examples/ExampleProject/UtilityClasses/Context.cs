using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject {
	public abstract class Context {
		public async Task Execute() {
			using var db = new Database();

			db.ActiveConnection.Open();

			using var tnx = db.ActiveConnection.BeginTransaction();

			try {
				await ExecuteImpl(db);
				tnx.Commit();
				return;
			} catch (Exception ex) {
				tnx?.Rollback();
				throw ex;
			}
		}
		public abstract Task ExecuteImpl(Database database);
	}
}

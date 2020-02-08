using ExampleProject.UtilityClasses;
using System;
using System.Threading.Tasks;

namespace ExampleProject {
	class Program {
		static async Task Main(string[] args) {
			await InitHelper.InitExampleApplicetion();

			var insertCommand = new InsertExample();
			await insertCommand.Execute();

			var selectCommand = new SelectExample();
			await selectCommand.Execute();

			var updateCommand = new UpdateExample();
			await updateCommand.Execute();

			await selectCommand.Execute();


			Console.WriteLine("--- Work with Jsonb ---");


			var JsonbInsertCommand = new Jsonb.InsertExample();
			await JsonbInsertCommand.Execute();

			var JsonbSlectCommand = new Jsonb.SelectExample();
			await JsonbSlectCommand.Execute();

			var JsonbUpdateCommand = new Jsonb.UpdateExample();
			await JsonbUpdateCommand.Execute();

			Console.WriteLine("Application completed successfully");
		}
	}
}

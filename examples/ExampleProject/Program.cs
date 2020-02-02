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



			Console.WriteLine("Application completed successfully");
		}
	}
}

using System;

namespace ExampleProject.Models {
	public class UserJsonb {
		public const string TableName = "UsersJsonb";

		public int Id { get; set; }
		public UserData Data { get; set; }
	}

	public class UserData {
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime RegisteredAt { get; set; }
	}
}

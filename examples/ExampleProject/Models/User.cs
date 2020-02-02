using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleProject.Models {
	public class User {
		public const string TableName = "Users";

		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime RegisteredAt { get; set; }
	}
}

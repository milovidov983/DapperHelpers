using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleProject {
	public class Settings {
		public static readonly Settings Instance = new Settings();

		static Settings() {
			var builder = new ConfigurationBuilder()
					.AddJsonFile("settings.json")
					.Build();

			builder.Bind(Instance);
		}

		public string ConnectionString { get; set; }
		public string Password { get; set; }
		public string GetConnectionString() => string.Format(ConnectionString, Password);
	}
}
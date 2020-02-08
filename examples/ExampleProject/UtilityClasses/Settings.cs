using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ExampleProject {
	public class Settings {
		public static readonly Settings Instance = new Settings();
		public static readonly JsonSerializerSettings JSS;
		static Settings() {
			var builder = new ConfigurationBuilder()
					.AddJsonFile("settings.json")
					.Build();

			builder.Bind(Instance);

			JSS = new JsonSerializerSettings {
				NullValueHandling = NullValueHandling.Ignore,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			};
			JSS.ContractResolver = new CamelCasePropertyNamesContractResolver();
			JSS.Converters.Add(new StringEnumConverter() {
				NamingStrategy = new CamelCaseNamingStrategy()
			});
		}

		public string ConnectionString { get; set; }
		public string Password { get; set; }
		public string GetConnectionString() => string.Format(ConnectionString, Password);
	}
}
using Dapper;
using Newtonsoft.Json;
using System;
using System.Data;

namespace DapperHelpers {
	public class JsonTypeHandler {
		public static void AddType<T>() {
			SqlMapper.AddTypeHandler(typeof(T), new JsonTypeHandler<T>());
		}

		public static void AddType<T>(Action<IDbDataParameter, object> mutator) {
			SqlMapper.AddTypeHandler(typeof(T), new JsonTypeHandler<T>() {
				ParameterMutator = mutator
			});
		}

		public static void AddType<T>(
		  Action<IDbDataParameter, object> mutator,
		  JsonSerializerSettings jss) {

			SqlMapper.AddTypeHandler(typeof(T), new JsonTypeHandler<T>(jss) {
				ParameterMutator = mutator
			});
		}
	}

	public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T> {
		public Action<IDbDataParameter, object> ParameterMutator = ((parameter, value) => { });
		private readonly JsonSerializerSettings jss;

		public JsonTypeHandler()
		  : this(new JsonSerializerSettings()) {
		}

		public JsonTypeHandler(JsonSerializerSettings jss) {
			this.jss = jss;
		}

		public override T Parse(object value) {
			if (value == null) {
				return default;
			}
			return JsonConvert.DeserializeObject<T>(value.ToString());
		}

		public override void SetValue(IDbDataParameter parameter, T value) {
			parameter.Value = JsonConvert.SerializeObject(value, jss);
			this.ParameterMutator(parameter, value);
		}
	}
}

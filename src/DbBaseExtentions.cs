using System;

namespace DapperHelpers {
	public static class DbBaseExtentions {
		public static string Query<T>(this T db, Func<T, string> fn) {
			return fn(db);
		}

		public static P Prepare<T, P>(this T input, Func<T, P> fn) {
			return fn(input);
		}
	}
}

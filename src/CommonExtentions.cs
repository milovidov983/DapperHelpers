using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DapperHelpers {
	internal static class CommonExtentions {
		public static string JoinStr(this IEnumerable<string> strs, string separator) {
			return string.Join(separator, strs);
		}

		public static string GetProperty<T, P>(this Expression<Func<T, P>> exp) {
			var body = (MemberExpression)exp.Body;
			return body?.Member?.Name;
		}
	}
}

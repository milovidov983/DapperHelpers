using DapperHelpers.Models;
using System;
using System.Linq.Expressions;

namespace DapperHelpers.Extentions {
	public static class JsonbExtentions {
		private static Func<string, string> converter;

		static JsonbExtentions() {
			SetPropertyNameConverter(PropertyNameConverters.Default);
		}

		public static void SetPropertyNameConverter(PropertyNameConverters propertyConverter) {
			if (propertyConverter != PropertyNameConverters.Default) {
				if (propertyConverter != PropertyNameConverters.CamelCase) {
					throw new NotImplementedException(converter.ToString());
				}
				converter = new Func<string, string>(ToCamelCase);
			} else {
				converter = new Func<string, string>(ToDefault);
			}
		}

		private static string ToCamelCase(string input) {
			if (input.Length == 1) {
				return input.ToLower();
			}
			return char.ToLowerInvariant(input[0]).ToString() + input.Substring(1);
		}

		private static string ToDefault(string input) {
			return input;
		}

		/// <summary>
		/// Returns a JSON object field by key ( '{"a": {"b":"foo"}}'::json->'a' )
		/// </summary>
		public static SqlField<P> Jsonb<T, P>(
		  this SqlField<T> field,
		  Expression<Func<T, P>> exp) {
			return new SqlField<P>($"{field.Value}->'{converter(exp.GetProperty())}'");
		}

		/// <summary>
		/// Returns a JSON element in type text ( '{"a":1,"b":2}'::json->>'b' )
		/// </summary>
		public static SqlField<P> JsonbStr<T, P>(
		  this SqlField<T> field,
		  Expression<Func<T, P>> exp) {
			return new SqlField<P>($"{field.Value}->>'{converter(exp.GetProperty())}'");
		}

		/// <summary>
		/// Cast field to type
		/// </summary>
		public static SqlField<T> JsonbCast<T>(this SqlField<T> field, string typeName) {
			return new SqlField<T>($"cast({field.Value} as {typeName})");
		}

		/// <summary>
		/// Cast field to type int
		/// </summary>
		public static SqlField<T> ToInt<T>(this SqlField<T> field) {
			return field.JsonbCast("int");
		}
	}
}

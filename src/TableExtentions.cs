using DapperHelpers.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DapperHelpers {
	public static class TableExtentions {
		/// <summary>
		/// Create table
		/// </summary>
		/// <typeparam name="T">
		/// Type is a table mapping.
		/// </typeparam>
		/// <param name="table">
		/// Table name
		/// </param>
		/// <returns></returns>
		public static Table<T> Create<T>(string table) {
			return new Table<T>(table, TypeExtensions.GetProperties(typeof(T)).Select(x => x.Name));
		}

		public static Table<T> Include<T, P>(this Table<T> tbl, Expression<Func<T, P>> expr) {
			return tbl.Include(expr.GetProperty());
		}

		public static Table<T> Exclude<T, P>(this Table<T> tbl, Expression<Func<T, P>> expr) {
			return tbl.Exclude(expr.GetProperty());
		}

		public static SqlField<P> Field<T, P>(this Table<T> tbl, Expression<Func<T, P>> exp) {
			return new SqlField<P>($"{tbl.Name}.\"{exp.GetProperty()}\"");
		}

		public static SqlField<P> FieldShort<T, P>(
		  this Table<T> tbl,
		  Expression<Func<T, P>> exp) {
			return new SqlField<P>($"\"{exp.GetProperty()}\"");
		}

		public static string FieldName<T, P>(this Table<T> tbl, Expression<Func<T, P>> exp) {
			return $"{exp.GetProperty()}";
		}

		public static string Select<T>(this Table<T> tbl) {
			return tbl.Fields.Select(x => $"{tbl.Name}.\"{x}\"").JoinStr(",");
		}

		public static string SelectInsert<T>(this Table<T> tbl) {
			return tbl.Fields.Select(x => $"\"{x}\"").JoinStr(",");
		}

		public static string Insert<T>(this Table<T> tbl) {
			return tbl.Fields.Select(x => $"@{x}").JoinStr(",");
		}

		public static string Update<T>(this Table<T> tbl) {
			return tbl.Fields.Select(x => $"\"{x}\"=@{x}").JoinStr(",");
		}
	}
}

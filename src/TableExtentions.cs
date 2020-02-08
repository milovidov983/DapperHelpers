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

		/// <summary>
		/// Include field to query
		/// </summary>
		/// <typeparam name="T">Table</typeparam>
		/// <typeparam name="P">Property</typeparam>
		/// <param name="expr">Lambda function to select a field </param>
		/// <returns>Source table</returns>
		public static Table<T> Include<T, P>(this Table<T> tbl, Expression<Func<T, P>> expr) {
			return tbl.Include(expr.GetProperty());
		}

		/// <summary>
		/// Exclude field to query
		/// </summary>
		/// <typeparam name="T">Table</typeparam>
		/// <typeparam name="P">Property</typeparam>
		/// <param name="expr">Lambda function to select a field </param>
		/// <returns>Source table</returns>
		public static Table<T> Exclude<T, P>(this Table<T> tbl, Expression<Func<T, P>> expr) {
			return tbl.Exclude(expr.GetProperty());
		}

		/// <summary>
		/// Use the full name field in the current location of the request.
		/// </summary>
		/// <typeparam name="T">Table</typeparam>
		/// <typeparam name="P">Property</typeparam>
		/// <param name="expr">Lambda function to select a field </param>
		public static SqlField<P> Field<T, P>(this Table<T> tbl, Expression<Func<T, P>> exp) {
			return new SqlField<P>($"{tbl.Name}.\"{exp.GetProperty()}\"");
		}

		/// <summary>
		/// Use the short name field in the current location of the request.
		/// </summary>
		/// <typeparam name="T">Table</typeparam>
		/// <typeparam name="P">Property</typeparam>
		/// <param name="expr">Lambda function to select a field </param>
		public static SqlField<P> FieldShort<T, P>(
		  this Table<T> tbl,
		  Expression<Func<T, P>> exp) {
			return new SqlField<P>($"\"{exp.GetProperty()}\"");
		}

		/// <summary>
		/// Use field name without quotes
		/// </summary>
		/// <typeparam name="T">Table</typeparam>
		/// <typeparam name="P">Property</typeparam>
		/// <param name="expr">Lambda function to select a field </param>
		/// <returns>Source table</returns>
		public static string FieldName<T, P>(this Table<T> tbl, Expression<Func<T, P>> exp) {
			return $"{exp.GetProperty()}";
		}

		/// <summary>
		/// List all selected table fields with a comma for a block SELECT
		/// </summary>
		/// <typeparam name="T">Table</typeparam>
		public static string Select<T>(this Table<T> tbl) {
			return tbl.Fields.Select(x => $"{tbl.Name}.\"{x}\"").JoinStr(",");
		}

		/// <summary>
		/// List all selected table fields with a comma for a block INSERT
		/// </summary>
		/// <typeparam name="T">Table</typeparam>
		public static string SelectInsert<T>(this Table<T> tbl) {
			return tbl.Fields.Select(x => $"\"{x}\"").JoinStr(",");
		}

		/// <summary>
		/// List all parameters for the insert block (..VALUES (@field1, @field2, ...)
		/// for each field at the beginning of the name the notation dapper @
		/// </summary>
		/// <typeparam name="T">Table</typeparam>
		public static string Insert<T>(this Table<T> tbl) {
			return tbl.Fields.Select(x => $"@{x}").JoinStr(",");
		}

		/// <summary>
		/// List all fields for update block (.. SET field1 = @field1, field2= @field2, ...)
		/// </summary>
		/// <typeparam name="T">Table</typeparam>
		public static string Update<T>(this Table<T> tbl) {
			return tbl.Fields.Select(x => $"\"{x}\"=@{x}").JoinStr(",");
		}
	}
}

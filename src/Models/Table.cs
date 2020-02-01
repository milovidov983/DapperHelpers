using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DapperHelpers.src.Models {
	/// <summary>
	/// An object describes a table in a database.
	/// This main class is used for convenient access to table columns in the database by column names.
	/// </summary>
	/// <typeparam name="T">
	/// The type of application that is the table mapping.
	/// It must contain all the same fields as the table, 
	/// and have the same types, with the exception of those objects that 
	/// are stored in the database as is or in json\jsonb format.
	/// </typeparam>
	public class Table<T> {
		private readonly string name;

		/// <summary>
		/// Table column names
		/// </summary>
		public ImmutableList<string> Fields { get; private set; }

		/// <summary>
		/// Table name
		/// </summary>
		public string Name { get => string.Format($"\"{name}\"");}

		/// <summary>
		/// Create table
		/// </summary>
		public Table(string name, IEnumerable<string> fields) {
			Fields = fields.ToImmutableList();
			this.name = name;
		}

		/// <summary>
		/// Remove all columns from the query
		/// </summary>
		public Table<T> Clear() {
			return new Table<T>(name, Enumerable.Empty<string>());
		}

		/// <summary>
		/// Include a specific column in the query
		/// </summary>
		public Table<T> Include(string field) {
			return new Table<T>(name, Fields.Add(field));
		}

		/// <summary>
		/// Exclude a specific column in the query
		/// </summary>
		public Table<T> Exclude(string field) {
			return new Table<T>(name, Fields.Remove(field));
		}
	}
}

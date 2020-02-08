namespace DapperHelpers.Models {
	public class SqlField<T> {
		public string Value { get; private set; }

		public SqlField(string value) {
			this.Value = value;
		}

		public static implicit operator string(SqlField<T> sp) {
			return sp.Value;
		}

		public override string ToString() {
			return Value;
		}
	}
}

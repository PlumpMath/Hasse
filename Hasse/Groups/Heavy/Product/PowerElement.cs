using System.Linq;
using System.Text;

namespace Hasse.Groups.Heavy.Product {
	public class PowerElement<T> : GroupElement<PowerElement<T>> where T : GroupElement<T> {
		private T[] Elements {
			get;
			set;
		}

		public PowerElement(T[] elements) {
			Elements = elements;
		}

		protected override PowerElement<T> Multiply(PowerElement<T> other) {
			var res = new T[Elements.Length];
			for(int i = 0; i < Elements.Length; i++)
				res[i] = Elements[i] * other.Elements[i];
			return new PowerElement<T>(res);
		}

		public override int Compare(PowerElement<T> other) {
			return Elements.Select((t, i) => t.Compare(other.Elements[i]))
				.FirstOrDefault(result => result != 0);
		}

		public override bool Equals(PowerElement<T> obj) {
			return !Elements.Where((t, i) => !t.Equals(obj.Elements[i])).Any();
		}

		public override int GetHashCode() {
			int toret = 0;
			foreach(T element in Elements) {
				toret *= 31;
				toret += element.GetHashCode();
			}
			return toret;
		}

		public override string ToString() {
			var sb = new StringBuilder();
			switch(Elements.Length) {
				case 0:
					return "{}";
				case 1:
					sb.Append(Elements[0]);
					break;
				default:
					sb.Append('{');
					sb.Append(Elements[0]);
					for(int i = 1; i < Elements.Length; i++) {
						sb.Append(", ");
						sb.Append(Elements[i]);
					}
					sb.Append('}');
					break;
			}
			return sb.ToString();
		}

		public override GroupElement<PowerElement<T>> Inverse(){
			var toret = new T[Elements.Length];
			for(int i = 0; i < toret.Length; i++)
				toret[i] = Elements[i].Inverse().Explode();
			return new PowerElement<T>(toret);
		}
	}
}

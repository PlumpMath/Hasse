using System;

namespace Hasse.Groups.Heavy.Product{
	public class PowerGroup<T> : Group<PowerElement<T>> where T : GroupElement<T>{
		public Group<T> Group{get; private set;}
		public new int Power{get; private set;}

		public PowerGroup(Group<T> group, int power) : base(MyMath.Pow(group.Order, power)){
			Group = group;
			Power = power;
			elements = new PowerElement<T>[Order];
			for(int index = 0; index < Order; index++){
				int curr = index;
				T[] values = new T[Power];
				for(int i = 0; i < Power; i++){
					values[i] = Group[curr % Group.Order];
					curr /= Group.Order;
				}
				elements[index] = new PowerElement<T>(values);
			}
		}

		private PowerElement<T>[] elements;

		public override PowerElement<T> GetElement(int index){
			return elements[index];
		}

		public override string ToString(){
			return string.Format("{0}^{1}", Group, Power);
		}
	}
}
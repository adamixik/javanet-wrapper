using System;
using System.IO;
using System.Reflection;
using java.lang.reflect;
using System.Collections.Generic;

namespace JavaNet
{
	public interface IJavaMethod
	{
		object Call(params object[] args);
		T Call<T>(params object[] args);
	}
	internal class JavaMethod : IJavaMethod
	{
		Method method = null;
		object obj = null;
		public JavaMethod(object obj, Method m)
		{
			method = m;
			this.obj = obj;
			m.setAccessible(true);
		}
		public object Call(params object[] args)
		{
			foreach (java.lang.Class cl in method.getParameterTypes()) {
				//Console.WriteLine ("param: " + cl.getName ());
			}
			foreach (object obj2 in args) {
				//Console.WriteLine ("a " + obj2.GetType ().Name);
			}
			for (int i = 0; i < args.Length; i++) {
				if (args [i].GetType ().Name == "JavaObject") {
					JavaObject o = (JavaObject)args [i];
					args [i] = o._GetInternalObject ();
				}
			}
			if (method.getParameterTypes ().Length == 1 && method.getParameterTypes () [0].getName ().StartsWith ("[L"))
				args = new object[] { args };
			return method.invoke (obj, args);
		}

		public T Call<T>(params object[] args)
		{
			object obj = this.Call (args);
			if (typeof(T) == typeof(int)) {
				java.lang.Integer i = (java.lang.Integer)obj;
				return (T)((object)i.intValue ());
			}
			return (T)this.Call (args);
		}
		public override string ToString ()
		{
			return string.Format ("[JavaMethod]." + method.getName());
		}
	}
}

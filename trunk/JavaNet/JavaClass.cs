using System;
using System.IO;
using System.Reflection;
using java.lang.reflect;
using System.Collections.Generic;

namespace JavaNet
{
	public interface IJavaClass
	{
		IJavaMethod GetMethod (string name, params object[] parameterTypes);
		IJavaObject New();
		IJavaField GetField(string name);
	}
	internal class JavaClass : IJavaClass
	{
		java.lang.Class clazz = null;
		public JavaClass(string name)
		{
			clazz = java.lang.Class.forName(name, true, java.lang.ClassLoader.getSystemClassLoader());
		}
		public IJavaMethod GetMethod(string name, params object[] parameterTypes)
		{
			foreach (Method m in clazz.getMethods()) {
				if (m.getName () == name && m.getParameterTypes().Length == parameterTypes.Length) {
					bool ok = true;

					for (int i = 0; i < parameterTypes.Length; i++) {
						java.lang.Class cl = null;
						//Console.WriteLine ("type: " + parameterTypes[i].GetType().Name);
						if (parameterTypes [i].GetType ().Name == "JavaClass") {
							JavaClass clz = (JavaClass)parameterTypes [i];
							cl = clz._GetInternalClass ();
						} else {
							System.Type t = (System.Type)parameterTypes [i];
							cl = (java.lang.Class)t;
						}
						if (!m.getParameterTypes () [i].Equals (cl))
							ok = false;
					}
					if (ok == true) {
						return new JavaMethod (null, m);
					}
				}
			}
			return null;
		}
		public IJavaObject New()
		{
			return new JavaObject (clazz.newInstance (), clazz);
		}
		public IJavaField GetField(string name)
		{
			return new JavaField (name, clazz);
		}

		public override string ToString ()
		{
			return string.Format ("[JavaClass]." + clazz.getName());
		}
		public java.lang.Class _GetInternalClass()
		{
			return clazz;
		}
	}
}

/*
JavaNet.Sample Project
Copyright (C) 2014 adamix

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.IO;
using System.Reflection;
using java.lang.reflect;
using System.Collections.Generic;

namespace JavaNet
{
	public interface IJavaObject
	{
		IJavaMethod GetMethod(string name, params object[] parameterTypes);
		IJavaField GetField(string name);
		T GetInterface<T>();
	}
	internal class JavaObject : IJavaObject
	{
		java.lang.Class clazz = null;
		object obj = null;
		public JavaObject(object obj, java.lang.Class clazz)
		{
			this.clazz = clazz;
			this.obj = obj;
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
						return new JavaMethod (obj, m);
					}
				}
			}
			return null;
		}
		public IJavaField GetField(string name)
		{
			return new JavaField (name, clazz, obj);
		}

		public T GetInterface<T>()
		{
			foreach (System.Type t in obj.GetType().GetInterfaces()) {
				if (typeof(T) == t)
					return (T)obj;
			}
			return default(T);
		}

		public override int GetHashCode ()
		{
			return GetMethod ("hashCode").Call<int> ();
		}
		public override string ToString ()
		{
			return string.Format ("[JavaObject]." + clazz.getName());
		}

		public java.lang.Class _GetInternalClass()
		{
			return clazz;
		}
		public object _GetInternalObject()
		{
			return obj;
		}
	}
}


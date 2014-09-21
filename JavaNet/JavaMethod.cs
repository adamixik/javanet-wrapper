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

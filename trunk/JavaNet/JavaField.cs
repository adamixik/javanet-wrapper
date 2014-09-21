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
	public interface IJavaField
	{
		object Get();
		void Set(object obj);
	}
	internal class JavaField : IJavaField
	{
		Field field = null;
		object obj = null;
		public JavaField(string name, java.lang.Class clazz, object obj = null)
		{
			field = clazz.getField(name);
			this.obj = obj;
		}

		public object Get()
		{
			return field.get (obj);
		}
		public void Set(object obj)
		{
			string type = obj.GetType ().Name;
			if (obj.GetType () == typeof(int)) {
				field.setInt (this.obj, (int)obj);
			} else if (obj.GetType () == typeof(float)) {
				field.setFloat (this.obj, (float)obj);
			} else if (obj.GetType () == typeof(double)) {
				field.setDouble (this.obj, (double)obj);
			} else if (obj.GetType () == typeof(bool)) {
				field.setBoolean (this.obj, (bool)obj);
			} else if (obj.GetType () == typeof(char)) {
				field.setChar (this.obj, (char)obj);
			} else if (obj.GetType () == typeof(byte)) {
				field.setByte (this.obj, (byte)obj);
			} else if (obj.GetType () == typeof(short)) {
				field.setShort (this.obj, (short)obj);
			} else if (obj.GetType () == typeof(long)) {
				field.setLong (this.obj, (long)obj);
			} else {
				field.set (this.obj, obj);
			}
		}
		public override string ToString ()
		{
			return string.Format ("[JavaField]." + field.getName());
		}
	}
}

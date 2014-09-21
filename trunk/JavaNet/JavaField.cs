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

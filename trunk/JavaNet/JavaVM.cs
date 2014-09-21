using System;
using System.IO;
using System.Reflection;
using java.lang.reflect;
using System.Collections.Generic;

namespace JavaNet
{
	public class JavaVM
	{
		private static JavaVM vm = null;
		private static IJavaClass ecj = null;
		private static IJavaMethod ecjMain = null;
		private static List<string> refs = null;
		private static List<string> libs = null;
		private static string myClassPath = null;
		private static string psep = ":";
		private JavaVM(List<string> stubs, bool rebuildStubs, string classPath)
		{
			Directory.CreateDirectory ("javanet/classes");
			Directory.CreateDirectory ("javanet/jars");

			var ecjstream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("JavaNet.ecj-4.5M2.jar");
			var ecjfilestream = new FileStream ("javanet/ecj.jar", FileMode.Create, FileAccess.Write);
			ecjstream.CopyTo (ecjfilestream);
			ecjstream.Dispose ();
			ecjfilestream.Flush ();
			ecjfilestream.Dispose ();

			string rootDir = Path.GetDirectoryName (Assembly.GetEntryAssembly ().Location);
			refs = new List<string> ();
			libs = new List<string> ();
			libs.Add(Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(System.String)).Location));
			BuildStub(Assembly.GetExecutingAssembly().Location);
			BuildStub (Assembly.GetEntryAssembly ().Location);
			foreach (AssemblyName an in Assembly.GetEntryAssembly().GetReferencedAssemblies()) {
				Assembly a = Assembly.Load (an);
				if (a.Location.StartsWith (rootDir)) {
					if (rebuildStubs) {
						BuildStub (a.Location);
					}
				}
			}
			classPath = classPath + psep + "javanet/ecj.jar" + psep + "javanet/classes";// + Assembly.GetEntryAssembly ().Location + ".jar";
			foreach (string file in Directory.EnumerateFiles("javanet/jars/")) {
				classPath += psep + "javanet/jars/" + Path.GetFileName(file);
			}
			//Console.WriteLine (classPath);
			System.Collections.Hashtable props = new System.Collections.Hashtable ();
			props["java.class.path"] = classPath;
			ikvm.runtime.Startup.setProperties (props);

			ecj = GetClass ("org.eclipse.jdt.internal.compiler.batch.Main");
			ecjMain = ecj.GetMethod ("main", typeof(string[]));
		
			myClassPath = classPath;
		}
		public static JavaVM Create(List<string> stubs = null, bool rebuildStubs = false, string classPath = null)
		{
			if (classPath == null) {
				classPath = ".";
			}
			if (stubs == null) {
				stubs = new List<string> ();
			}

			if (vm == null)
				vm = new JavaVM (stubs, rebuildStubs, classPath);

			return vm;
		}
		public IJavaClass GetClass(string name)
		{
			return new JavaClass (name);
		}
		public void BuildStub(string assemblyPath)
		{
			IKVM.IKVMStub.Build (assemblyPath, "javanet/jars/" + Path.GetFileName(assemblyPath) + ".jar", true, libs, refs);
		}
		public void BuildJavaFile (string path, string outPath = "javanet/classes")
		{
			ecjMain.Call(new string[] { "-classpath", myClassPath, "-d", outPath, "-warn:none", "-g:none", path});
		}
		public void AddClassPath(string path)
		{
			myClassPath += psep + path;
			System.Collections.Hashtable props = new System.Collections.Hashtable ();
			props["java.class.path"] = myClassPath;
			ikvm.runtime.Startup.setProperties (props);
		}

		public override string ToString ()
		{
			return string.Format ("[JavaNet.VM]");
		}
	}

}
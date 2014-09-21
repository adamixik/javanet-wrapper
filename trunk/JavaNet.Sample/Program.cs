using System;
using JavaNet;

namespace JavaNet.Sample
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Initialize the VM
			var VM = JavaVM.Create (null, true, null);

			// Build Sample.java file with bundled ECJ compiler
			VM.BuildJavaFile ("Sample.java");

			// Load the class
			var sample = VM.GetClass ("Sample");

			// Call main(String[]) method
			sample.GetMethod ("main", typeof(string[])).Call (new string[] { "some", "arguments" });

			// Call static method
			sample.GetMethod ("HelloStatic").Call ();

			// Get instance of the Sample class
			var instance = sample.New ();

			// Get the "number"(integer type) field
			var number = instance.GetField ("number");

			// Set the number field to 15
			number.Set (15);

			// And finally call the test(String arg, String[] arr) method
			instance.GetMethod ("test", typeof(string), typeof(string[])).Call ("some argument", new string[] {
				"some array of",
				"arguments"
			});
		}
	}
}

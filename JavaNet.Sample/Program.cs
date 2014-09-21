using System;
using JavaNet;

namespace JavaNet.Sample
{
	public interface IHelloWorld
	{
		void Hello();
		string GetWorld();
	}

	public class HelloWorld : IHelloWorld
	{
		public void Hello()
		{
			Console.WriteLine ("Hello from .Net!");
		}

		public string GetWorld()
		{
			return ".NetWorld";
		}
	}

	class MainClass
	{
		public static void Main (string[] args)
		{
			// Initialize the VM
			var VM = JavaVM.Create (null, true, null);

			// Build sample java files with bundled ECJ compiler
			VM.BuildJavaFile ("Sample.java");
			VM.BuildJavaFile ("HelloWorld_impl.java");

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


			// Okay, now test interfaces
			var helloWorldJavaField = instance.GetField ("helloWorld");

			// Create the .Net instance and push it to java
			IHelloWorld helloWorld = new HelloWorld ();
			helloWorldJavaField.Set (helloWorld);

			// Call the testHelloWorld method
			instance.GetMethod ("testHelloWorld").Call ();

			// Create the java side IHelloWorld implementation
			var helloWorldJavaObject = VM.GetClass ("HelloWorld_impl").New ();
			var helloWorldJava = helloWorldJavaObject.GetInterface<IHelloWorld> ();

			// Call methods from C#
			helloWorldJava.Hello ();
			Console.WriteLine("C# helloWorldJava.GetWorld(): " + helloWorldJava.GetWorld());
		}
	}
}

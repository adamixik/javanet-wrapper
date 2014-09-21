
/**
 *
 * @author adamix
 */

public class HelloWorld_impl implements cli.JavaNet.Sample.IHelloWorld {
	public void Hello()
	{
		System.out.println("Hello from Java!");
	}
	
	public String GetWorld()
	{
		return "JavaWorld";
	}
}

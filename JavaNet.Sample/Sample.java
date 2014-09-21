/**
 *
 * @author adamix
 */
public class Sample {
    public int number = 10;
    public cli.JavaNet.Sample.IHelloWorld helloWorld = null;
    public static void main(String[] args) 
    {
        System.out.println(args[0]);
    }
    public void Hello()
    {
		System.out.println("Hello from Sample1! Number: " + number);
    }
    public static void HelloStatic()
    {
		System.out.println("Hello from Sample1Static!");
    }
    public void test(String arg, String[] arr)
    {
		System.out.println("test hashCode: " + hashCode());
		System.out.println("'arg' value: " + arg);
		System.out.println("first value of arr[]: " + arr[0]);
    }
    public void testHelloWorld()
    {
    	helloWorld.Hello();
    	System.out.println("Java helloWorld.GetWorld(): " + helloWorld.GetWorld());
    }
}

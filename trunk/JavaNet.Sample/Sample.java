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

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author adamix
 */
public class Sample {

    /**
     * @param args the command line arguments
     */
   
    public int number = 10;
    public static void main(String[] args) {
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
    
}

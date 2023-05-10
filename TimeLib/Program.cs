using TimeLib;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Time t1 = new Time(1);
TimePeriod t2 = new TimePeriod(0, 12, 1);
Time t3 = t1.Plus(t2);
Console.WriteLine(t3);

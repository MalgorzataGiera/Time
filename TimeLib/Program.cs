using System.Drawing;
using TimeLib;

var tP1 = new TimePeriod(3, 61, 60);
Time t1 = new Time("1:2:3");
Console.WriteLine(t1.ToString());

Console.WriteLine(t1.Equals(t1));

string z = new string("a");
Console.WriteLine(z.Length);
int i = 0;
Console.WriteLine(i.ToString());
var v = new Point();
Object obj = null;
Console.WriteLine(t1.Equals(obj));
Console.WriteLine(v.Equals(v));
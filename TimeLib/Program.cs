using System.Drawing;
using TimeLib;

var tP1 = new TimePeriod(3, 61, 60);
var t1 = new Time("1:2:3");

Console.WriteLine(t1.Plus(tP1));
Console.WriteLine(t1 + (tP1 * 3));
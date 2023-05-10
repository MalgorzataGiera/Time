using TimeLib;

var tP1 = new TimePeriod(24, 00, 00);
var tP2 = new TimePeriod(2, 20, 0);
Console.WriteLine(tP1.NumberOfSeconds);
Console.WriteLine(tP2.NumberOfSeconds);
var expectedNewTime = tP1.Plus(tP2);
Console.WriteLine(expectedNewTime);
Console.WriteLine(94800%3600 / 60);
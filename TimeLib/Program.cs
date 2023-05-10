using TimeLib;

var tP1 = new Time(00, 00, 00);
var tP2 = new TimePeriod(0, 0, 0);

var expectedNewTime = tP1.Minus(tP2);
Console.WriteLine(expectedNewTime);

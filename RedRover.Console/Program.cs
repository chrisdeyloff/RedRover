using RedRover.Parse;

Console.WriteLine("Red Rover Code Puzzle");

Console.WriteLine();

var standardOutput = Parser.ParseToMultilineNested(Constants.Input);
Console.WriteLine("*** standard output ***");
Console.WriteLine(standardOutput);

Console.WriteLine();

var sortedOutput = Parser.ParseToMultilineNested(Constants.Input, true);
Console.WriteLine("*** sorted output ***");
Console.WriteLine(sortedOutput);

Console.WriteLine();

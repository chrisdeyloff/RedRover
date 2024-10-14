using RedRover.Parse;

Console.WriteLine();

Console.WriteLine("Red Rover Code Puzzle");

Console.WriteLine();

Console.WriteLine($"Input - {Constants.Input}");

var standardOutput = Parser.ParseToMultilineNested(Constants.Input);
Console.WriteLine("*** standard output ***");
Console.WriteLine(standardOutput);

Console.WriteLine();

var sortedOutput = Parser.ParseToMultilineNested(Constants.Input, true);
Console.WriteLine("*** sorted output ***");
Console.WriteLine(sortedOutput);

Console.WriteLine();

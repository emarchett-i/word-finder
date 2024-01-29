
IEnumerable<string> matrix = new List<string> { "abcdcc", "fgwioh", "chilli", "pqnsdl", "uvdxyl", "chillp" };

var wordFinder = new WordFinder.Console.WordFinder(matrix);

IEnumerable<string> wordStream = new List<string> { "cold", "wind", "snow", "chill" };

IEnumerable<string> result = wordFinder.Find(wordStream);

Console.WriteLine("Top 10 most repeated words:");

foreach (string word in result)
{
    Console.WriteLine(word);
}

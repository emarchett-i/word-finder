namespace WordFinder.Console
{
    public class WordFinder
    {
        private readonly char[,] _matrix;
        private readonly HashSet<string> _uniqueWords;

        public WordFinder(IEnumerable<string> matrix)
        {
            string[] matrixArray = matrix.ToArray();

            if (matrix == null || !matrix.Any() || matrix.Count() > 64 || matrix.Any(row => row.Length != matrix.Count()))
            {
                throw new ArgumentException("Invalid matrix");
            }

            _matrix = new char[matrix.Count(), matrix.Count()];
            _uniqueWords = new HashSet<string>();

            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j = 0; j < matrix.Count(); j++)
                {
                    _matrix[i, j] = matrix.ToArray()[i][j];
                }
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            if (wordStream == null || !wordStream.Any())
            {
                return new List<string>();
            }

            foreach (var word in wordStream)
            {
                _uniqueWords.Add(word);
            }

            var wordCounts = new Dictionary<string, int>();

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    foreach (var direction in new[] { Direction.Horizontal, Direction.Vertical })
                    {
                        FindWordsRecursive(i, j, "", direction, wordCounts);
                    }
                }
            }

            return wordCounts.OrderByDescending(kv => kv.Value)
                             .Take(10)
                             .Select(kv => kv.Key)
                             .ToList();
        }

        private void FindWordsRecursive(int row, int col, string currentWord, Direction direction, Dictionary<string, int> wordCounts)
        {
            if (row < 0 || row >= _matrix.GetLength(0) || col < 0 || col >= _matrix.GetLength(1))
            {
                return;
            }

            currentWord += _matrix[row, col];

            if (!_uniqueWords.Any(uw => uw.StartsWith(currentWord)))
            {
                return;
            }

            //System.Console.WriteLine(currentWord);

            if (_uniqueWords.Contains(currentWord))
            {
                wordCounts.TryGetValue(currentWord, out int count);
                wordCounts[currentWord] = count + 1;
            }

            if (direction == Direction.Horizontal)
            {
                FindWordsRecursive(row, col + 1, currentWord, direction, wordCounts);
            }
            //Vertical
            else
            {
                FindWordsRecursive(row + 1, col, currentWord, direction, wordCounts);
            }
        }
    }
}

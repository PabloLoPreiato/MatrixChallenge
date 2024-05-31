namespace MatrixChallenge.Domain.Services
{
    public class WordFinderService : IWordFinderService
    {
        private readonly string[,] _matrix;
        public WordFinderService(IEnumerable<string> matrix)
        {
            _matrix = CreateMatrix(matrix);
        }

        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            var wordsOccurrences = CountWordsOccurrences(wordStream);

            return wordsOccurrences.Where(w => w.Value > 0).OrderByDescending(w => w.Value).Take(10).Select(w => w.Key);
        }

        private string[,] CreateMatrix(IEnumerable<string> strings)
        {
            var stringList = strings.ToList();
            int rows = stringList.Count;
            int cols = stringList[0].Length;

            var matrix = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                var row = stringList[i];

                for (int j = 0; j < cols; j++)
                {
                    {
                        matrix[i, j] = row[j].ToString();
                    }
                }
            }

            return matrix;
        }

        private Dictionary<string, int> CountWordsOccurrences(IEnumerable<string> words)
        {
            var occurrencesDictionary = new Dictionary<string, int>();

            foreach (var word in words)
            {
                occurrencesDictionary[word] = 0;
            }

            int rows = _matrix.GetLength(0);
            int cols = _matrix.GetLength(1);


            //Count occurrences from left to right
            for (int i = 0; i < rows; i++)
            {
                var foundWordsInRow = new HashSet<string>();
                for (int j = 0; j < cols; j++)
                {
                    foreach (var word in words)
                    {
                        //If the remaining columns are less than the lenght of the word then there is no need to search further
                        if (cols - word.Length - j < 0)
                            break;

                        //If the word was already found in that row then there is no need to search for it again in this row
                        if (!foundWordsInRow.Contains(word) && IsWordAtPosition(word, i, j, 0, 1))
                        {
                            occurrencesDictionary[word]++;
                            foundWordsInRow.Add(word);
                        }
                    }
                }
            }

            //Count occurrences from top to bottom
            for (int j = 0; j < cols; j++)
            {
                var foundWordsInColumn = new HashSet<string>();
                for (int i = 0; i < rows; i++)
                {
                    foreach (var word in words)
                    {
                        //If the remaining rows are less than the lenght of the word then there is no need to search further
                        if (rows - word.Length - i < 0)
                            break;
                        
                        //If the word was already found in that column then there is no need to search for it again in this column
                        if (!foundWordsInColumn.Contains(word) && IsWordAtPosition(word, i, j, 1, 0))
                        {
                            occurrencesDictionary[word]++;
                            foundWordsInColumn.Add(word);
                        }
                    }
                }
            }
            return occurrencesDictionary;
        }

        private bool IsWordAtPosition(string word, int indexX, int indexY, int searchInRows, int searchInCols)
        {
            for (int k = 0; k < word.Length; k++)
            {
                if (_matrix[indexX + k * searchInRows, indexY + k * searchInCols] != word[k].ToString())
                {
                    return false;
                }
            }
            return true;
        }
    }
}

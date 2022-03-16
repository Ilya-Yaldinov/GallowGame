using System.Collections;

namespace MyNamespace
{
    class MyClass
    {
        public static void Main()
        {
            Game game = new Game();
            game.Start();
        }
    }

    class Game
    {
        public readonly List<string> correctSymbol = new List<string>();
        public readonly List<string> mistakes = new List<string>();

        public void Start()
        {
            ConsoleDrawer consoleDrawer = new ConsoleDrawer();
            string word = consoleDrawer.InputWord();
            consoleDrawer.DrawLetterPlace(word.ToCharArray(), correctSymbol);
            consoleDrawer.DrawGallowPlace(0);
            for (int i = 0; i < word.Length;)
            {
                var isCorrect = IsLetterCorrect(word);
                if (isCorrect) i++;
            }
        }
        
        private bool IsLetterExist(char[] wordToLetter, string letter, bool isCorrect)
        {
            for (int i = 0; i < wordToLetter.Length; i++)
            {
                isCorrect = false;
                if (letter == wordToLetter[i].ToString())
                {
                    isCorrect = true;
                    break;
                }
            }

            return isCorrect;
        }
        
        public bool IsLetterCorrect(string truWord)
        {
            char[] wordToLetter = truWord.ToCharArray();
            bool isCorrect = false;
            ConsoleDrawer consoleDrawer = new ConsoleDrawer();
            Console.SetCursorPosition( 0, 0);

            var letter = consoleDrawer.Letter();
            
            if (letter.Length != 1 && char.IsLetter(Convert.ToChar(letter))) 
                consoleDrawer.RedrawConsol(mistakes, wordToLetter, correctSymbol);

            isCorrect = IsLetterExist(wordToLetter, letter, isCorrect);
            
            if (!correctSymbol.Contains(letter) && char.IsLetter(Convert.ToChar(letter)) && isCorrect) 
                correctSymbol.Add(letter);
            
            if (!isCorrect)
                if (!mistakes.Contains(letter) && char.IsLetter(Convert.ToChar(letter)))
                    mistakes.Add(letter);
            
            consoleDrawer.RedrawConsol(mistakes, wordToLetter, correctSymbol);
            if (IsGameWon(wordToLetter)) consoleDrawer.GameOver(true, truWord);
            if(mistakes.Count == 6) consoleDrawer.GameOver(false, truWord);
            if (correctSymbol.Contains(letter)) return false;
            
            return isCorrect;
        }

        private bool IsGameWon(char[] wordToLetter)
        {
            foreach (var e in wordToLetter)
            {
                if (!correctSymbol.Contains(e.ToString())) return false;
            }

            return true;
        }
    }

    class ConsoleDrawer
    {
        public string Letter()
        {
            Console.Write("Введите букву: ");
            string letter = Console.ReadLine();
            return letter;
        }
        
        public string InputWord()
        {
            Console.Write("Введите слово: ");
            string word = Console.ReadLine();
            Console.Clear();
            return word;
        }
        
        public void GameOver(bool isSuccess, string startWord)
        {
            Console.SetCursorPosition(50, 10);
            if (isSuccess)
            {
                Console.WriteLine("Поздравляем! Вы победили!");
            }
            else
            {
                Console.WriteLine("Вы проиграли.");
                Console.SetCursorPosition(50, 11);
                Console.WriteLine($"Загаданное слово: {startWord}");
            }
            Console.ReadKey();
            Environment.Exit(0);
        }
        
        public void RedrawConsol(List<string> mistakes, char[] wordToLetter, List<string> correctSymbol)
        {
            Console.Clear();
            DrawLetterPlace(wordToLetter, correctSymbol);
            SetMistakes(mistakes);
            DrawGallowPlace(mistakes.Count);
        }
        
        public void SetMistakes(List<string> mistakes)
        {
            Console.SetCursorPosition(0, 20);
            if(mistakes.Count != 0) Console.WriteLine($"Неверные буквы: {string.Join(",", mistakes)}");
        }

        public void DrawLetterPlace(char[] wordToLetter, List<string> correctSymbol)
        {
            Dictionary<int, string> symbol = new Dictionary<int, string>();
            for (int i = 0; i < wordToLetter.Length; i++)
            {
                symbol[i] = "_";
                for (int j = 0; j < correctSymbol.Count; j++)
                {
                    if(wordToLetter[i] == Convert.ToChar(correctSymbol[j])) symbol[i] = correctSymbol[j];
                }
            }
            Console.SetCursorPosition(50, 20);
            Console.Write($"{string.Join(" ", symbol.Select(x=> x.Value))}");
        }

        public void DrawGallowPlace(int mistakes)
        {
            if (mistakes >= 0)
            {
                Console.SetCursorPosition(50, 14);
                for (int i = 0; i < 5; i++)
                {
                    Console.Write("-");
                }

                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(55, 14 + i);
                    Console.Write("|");
                }
            }
            if (mistakes >= 1)
            {
                Console.SetCursorPosition(50, 15);
                Console.Write("O");
            }
            if (mistakes >= 2)
            {
                Console.SetCursorPosition(50, 16);
                Console.Write("||");
            }
            if (mistakes >= 3)
            {
                Console.SetCursorPosition(49, 16);
                Console.Write("/");
            }
            if (mistakes >= 4)
            {
                Console.SetCursorPosition(52, 16);
                Console.Write("\\");
            }
            if (mistakes >= 5)
            {
                Console.SetCursorPosition(50, 17);
                Console.Write("/");
            }
            if (mistakes >= 6)
            {
                Console.SetCursorPosition(51, 17);
                Console.Write("\\");
            }
        }
    }
}



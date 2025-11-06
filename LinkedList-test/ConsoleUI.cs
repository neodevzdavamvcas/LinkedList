using System.Text;

namespace LinkedList_test {
    public class ConsoleUi(Diary diary) {
        private readonly Dictionary<string, Action> _commandActionDict = new() {
            ["predchozi"] = () => {
                if (diary.Current?.Previous is not null) diary.Current = diary.Current.Previous;
            },
            ["dalsi"] = () => {
                if (diary.Current?.Next is not null) diary.Current = diary.Current.Next;
            },
            ["zacatek"] = () => diary.Current = diary.Entries.First,
            ["konec"] = () => diary.Current = diary.Entries.Last,
            ["novy"] = () => {
                DateOnly date;
                while (!DateOnly.TryParse(Prompt("Datum: ".Pastel(TitleColor)), out date)) {
                    ClearLastLine();
                }

                Console.WriteLine("\nText:".Pastel(TitleColor));

                var lines = new StringBuilder();
                while (true) {
                    var input = Console.ReadLine();
                    if (input == "uloz") {
                        break;
                    }

                    lines.AppendLine(input);
                }

                var entry = new Diary.DiaryEntry(date, lines.ToString());
                if (diary.Current is null) {
                    diary.Entries.AddLast(entry);
                    diary.Current = diary.Entries.Last;
                } else {
                    diary.Entries.AddAfter(entry, diary.Current);
                    diary.Current = diary.Current.Next;
                }
            },
            ["smaz"] = () => {
                if (diary.Current is null) return;

                Console.WriteLine("Pro odstranění tohoto záznamu stiskni \"a\", pro zrušení jiný znak.");
                if (Console.ReadKey().Key != ConsoleKey.A) return;

                var oldCurrent = diary.Current;
                diary.Current = diary.Current?.Previous ?? diary.Current?.Next;
                diary.Entries.Remove(oldCurrent);
            }
        };

        private bool _shouldClose;
        public void Start() {
            _commandActionDict["zavri"] = () => _shouldClose = true;

            while (!_shouldClose) {
                HandleCommands();
            }
        }

        private void HandleCommands() {
            Console.Clear();
            PrintCommands();
            Console.WriteLine($"{"Počet záznamů:".Pastel(TitleColor)} {diary.Entries.Count}\n");
            if (diary.Current is not null) Console.WriteLine(diary.Current.Value);

            var command = GetCommand();
            Console.WriteLine();
            command.Invoke();
        }
        private static void PrintCommands() {
            const string divider = "------------------------------------------------------";

            var commands = new Dictionary<string, string> {
                ["predchozi"] = "Přesunutí na předchozí záznam",
                ["dalsi"] = "Přesunutí na další záznam",
                ["zacatek"] = "Přesunutí na první záznam",
                ["konec"] = "Přesunutí na poslední záznam",
                ["novy"] = "Vytvoření nového záznamu",
                ["uloz"] = "Uložení vytvořeného záznamu",
                ["smaz"] = "Odstranění záznamu",
                ["zavri"] = "Zavření deníku"
            };

            var output = new StringBuilder(divider + "\n");

            foreach (var (command, description) in commands) {
                output.AppendLine($"- {command.Pastel(TitleColor)}: {description}");
            }

            output.AppendLine(divider);
            Console.WriteLine(output.ToString());
        }

        private Action GetCommand() {
            while (true) {
                var input = Prompt("Příkaz: ".Pastel(TitleColor));

                if (input.Length == 1) {
                    var letter = input.ToCharArray()[0];
                    var matched = _commandActionDict.FirstOrDefault(pair => pair.Key[0] == letter);
                    if (matched.Key is not null) return matched.Value;
                }

                if (_commandActionDict.TryGetValue(input, out var command)) {
                    return command;
                }

                ClearLastLine();
            }
        }
    }
}

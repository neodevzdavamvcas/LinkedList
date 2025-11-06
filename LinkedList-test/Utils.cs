using System.Drawing;

namespace LinkedList_test {
    public static class Utils {
        public static string Prompt(string prompt) {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }

        public static void ClearLastLine() {
            Console.CursorTop--;
            Console.CursorLeft = 0;
            Console.Write(new string(' ', Console.WindowWidth));
            Console.CursorLeft = 0;
        }

        public static class Consts {
            public static readonly Color TitleColor = Color.AliceBlue;
        }
    }
}

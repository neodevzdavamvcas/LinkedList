global using static LinkedList_test.Utils;
global using static LinkedList_test.Utils.Consts;
global using Pastel;

namespace LinkedList_test {
    public static class Program {
        public static void Main() {
            new Diary().Start();
            Console.Clear();
            Console.WriteLine("Goodbye.");
            Thread.Sleep(100);
            Console.WriteLine("Have a nice day and good lunch.");
            Thread.Sleep(300);
        }
    }
}

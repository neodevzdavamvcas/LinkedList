namespace LinkedList_test {
    public class Diary {
        public readonly LinkedList<DiaryEntry> Entries = new();
        public LinkedList<DiaryEntry>.Node? Current = null;
        public void Start() {
            new ConsoleUi(this).Start();
        }
        public readonly struct DiaryEntry(DateOnly date, string text) {
            public override string ToString() {
                return $"""
                        {"Datum:".Pastel(TitleColor)} {date}

                        {"Text:".Pastel(TitleColor)}
                        {text}
                        """;
            }
        }
    }
}

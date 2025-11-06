namespace LinkedList_test {
    public class LinkedList<T> {
        private int _count;
        public Node? First { get; private set; }
        public Node? Last { get; private set; }

        public int Count {
            get => _count;
            private set {
                _count = value;
                if (value > 0) return;

                First = null;
                Last = null;
            }
        }

        public Node AddAfter(T value, Node afterNode) {
            Count++;
            var newNode = new Node(value);
            var oldNext = afterNode.Next;

            afterNode.Next = newNode;
            newNode.Next = oldNext;
            newNode.Previous = afterNode;
            if (oldNext is not null) {
                oldNext.Previous = newNode;
            } else {
                Last = newNode;
            }

            return newNode;
        }

        public Node AddBefore(T value, Node beforeNode) {
            Count++;
            var newNode = new Node(value);
            var oldPrevious = beforeNode.Previous;

            beforeNode.Previous = newNode;
            newNode.Previous = oldPrevious;
            newNode.Next = beforeNode;
            if (oldPrevious is not null) {
                oldPrevious.Next = newNode;
            } else {
                First = newNode;
            }

            return newNode;
        }

        public Node AddFirst(T value) {
            Count++;

            var oldFirst = First;
            First = new(value);
            if (oldFirst is null) {
                Last = First;
                return First;
            }

            oldFirst.Previous = First;
            First.Next = oldFirst;
            return First;
        }

        public Node AddLast(T value) {
            Count++;

            var oldLast = Last;
            Last = new(value);
            if (oldLast is null) {
                First = Last;
                return Last;
            }

            oldLast.Next = Last;
            Last.Previous = oldLast;
            return Last;
        }

        public T? RemoveFirst() {
            if (Count <= 0 || First is null) return default;

            var oldFirst = First;
            First = oldFirst.Next;
            oldFirst.Next = null;
            if (First is not null) First.Previous = null;

            Count--;
            return oldFirst.Value;
        }

        public T? RemoveLast() {
            if (Count <= 0 || Last is null) return default;

            var oldLast = Last;
            Last = oldLast.Previous;
            oldLast.Previous = null;
            if (Last is not null) Last.Next = null;

            Count--;
            return oldLast.Value;
        }

        public T? Remove(Node node) {
            if (First == Last) {
                First = null;
                Last = null;
                Count--;
            } else if (node == First) {
                RemoveFirst();
            } else if (node == Last) {
                RemoveLast();
            } else {
                node.Previous!.Next = node.Next;
                node.Next!.Previous = node.Previous;
                Count--;
            }

            return node.Value;
        }

        public override string ToString() {
            var values = new T[Count];
            var node = First;

            for (var i = 0; i < Count; i++) {
                values[i] = node!.Value!;
                node = node.Next;
            }

            return '[' + string.Join(" ⇄ ", values) + ']';
        }

        public class Node {
            public readonly T? Value;
            public Node? Next;
            public Node? Previous;

            internal Node(T value) {
                Value = value;
            }
        }
    }
}

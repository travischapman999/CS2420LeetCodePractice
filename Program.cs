public class Program
{
	static void Main()
	{
		LinkedList linkedList1 = new LinkedList([1, 2, 3, 4, 5, 6, 7, 8, 9]);
		int k = 3;
		linkedList1.PrintList();
		linkedList1.ReverseAtK(k);
		linkedList1.PrintList();
		DoublyLinkedList doublyLinkedList1 = new DoublyLinkedList([1, 2, 3, 4, 5]);
		k = 2;
		doublyLinkedList1.PrintList();
		doublyLinkedList1.Rotate(k);
		doublyLinkedList1.PrintList();
		int[] prices = [8, 4, 6, 2, 3];
		int[] output = NextLowerPrices(prices);
		PrintArray(prices);
		PrintArray(output);
		int[] priorities = [2, 1, 3, 2];
		int location = 2;
		PrintArray(priorities);
		Console.WriteLine(PrinterQueue(priorities, location));

	}
	public static int[] NextLowerPrices(int[] prices)
	{
		int[] answer = new int[prices.Length];

		Stack<int> stack = new Stack<int>();

		for (int i = 0; i < prices.Length; i++)
		{
			answer[i] = -1;

			while (stack.Count > 0 && prices[i] < prices[stack.Peek()])
			{
				int previousDay = stack.Pop();
				answer[previousDay] = i - previousDay;
			}

			stack.Push(i);
		}

		return answer;
	}
	public static void PrintArray<T>(T[] array)
	{
		foreach (T item in array)
		{
			Console.Write(item + " ");
		}
		Console.WriteLine();
	}
	public static int PrinterQueue(int[] priorities, int location)
	{
		if (priorities.Length == 1) return 1;
		int count = 0;
		int i = 0;
		Queue<(int index, int priority)> queue = new Queue<(int, int)>();
		int[] priorityCounts = new int[10];
		foreach (int job in priorities)
		{
			queue.Enqueue((i, job));
			priorityCounts[job]++;
			i++;
		}
		while (queue.Count > 0)
		{
			var job = queue.Dequeue();
			bool higherPriorityExists = false;
			for (int p = job.priority + 1; p <= 9; p++)
			{
				if (priorityCounts[p] > 0)
				{
					higherPriorityExists = true;
					break;
				}
			}
			if (higherPriorityExists)
			{
				queue.Enqueue(job);
			}
			else
			{
				count++;
				priorityCounts[job.priority]--;
				if (job.index == location)
				{
					return count;
				}
			}
		}
		return -1;
	}
}
public class LLNode
{
	public int Value;
	public LLNode Next;
	public LLNode(int value)
	{
		this.Value = value;
	}
}
public class LinkedList
{
	private LLNode head;
	private LLNode tail;
	private int length;
	public LinkedList(int value)
	{
		LLNode newNode = new LLNode(value);
		head = newNode;
		tail = newNode;
		length = 1;
	}
	public LinkedList(int[] values)
	{
		foreach (int value in values)
		{
			Append(value);
		}
	}
	public void Append(int value)
	{
		LLNode newNode = new LLNode(value);
		if (length == 0)
		{
			head = newNode;
			tail = newNode;
		}
		else
		{
			tail.Next = newNode;
			tail = newNode;
		}
		length++;
	}
	public void PrintList()
	{
		LLNode temp = head;
		while (temp.Next != null)
		{
			Console.Write(temp.Value + ", ");
			temp = temp.Next;
		}
		Console.WriteLine(temp.Value);
	}
	public void PrintAll()
	{
		if (length == 0)
		{
			Console.WriteLine("Head: null");
			Console.WriteLine("Tail: null");
		}
		else
		{
			Console.WriteLine("Head: " + head.Value);
			Console.WriteLine("Tail: " + tail.Value);
		}
		Console.WriteLine("Length: " + length);
		Console.WriteLine("\nLinked List:");
		if (length == 0)
		{
			Console.WriteLine("empty");
		}
		else
		{
			PrintList();
		}
	}
	public void ReverseAtK(int k)
	{
		if (head == null || k <= 1) return;
		LLNode current = head;
		LLNode endOfPreviousGroup = null;
		bool reverse = true;
		while (current != null)
		{
			LLNode startOfGroup = current;
			LLNode endOfGroup = current;
			for (int i = 1; i < k && endOfGroup.Next != null; i++)
			{
				endOfGroup = endOfGroup.Next;
			}
			LLNode nextGroup = endOfGroup.Next;
			if (reverse)
			{
				LLNode previous = nextGroup;
				current = startOfGroup;
				while (current != nextGroup)
				{
					LLNode next = current.Next;
					current.Next = previous;
					previous = current;
					current = next;
				}
				if (endOfPreviousGroup == null)
				{
					head = endOfGroup;
				}
				else
				{
					endOfPreviousGroup.Next = endOfGroup;
				}
				endOfPreviousGroup = startOfGroup;
			}
			else
			{
				endOfPreviousGroup = endOfGroup;
			}
			current = nextGroup;
			reverse = !reverse;
		}
		while (tail != null && tail.Next != null)
		{
			tail = tail.Next;
		}
	}
}
public class DLLNode
{
	public int value;
	public DLLNode next;
	public DLLNode prev;
	public DLLNode(int value)
	{
		this.value = value;
	}
}
public class DoublyLinkedList
{
	private DLLNode head;
	private DLLNode tail;
	private int length;
	public DoublyLinkedList(int value)
	{
		DLLNode newNode = new DLLNode(value);
		head = newNode;
		tail = newNode;
		length = 1;
	}
	public DoublyLinkedList(int[] values)
	{
		foreach (int value in values)
		{
			Append(value);
		}
	}
	public void Append(int value)
	{
		DLLNode newNode = new DLLNode(value);
		if (length == 0)
		{
			head = newNode;
			tail = newNode;
		}
		else
		{
			DLLNode temp = tail;
			tail = newNode;
			tail.prev = temp;
			temp.next = tail;
		}
		length++;
	}
	public void PrintList()
	{
		DLLNode temp = head;
		while (temp != null)
		{
			Console.Write(temp.value);
			if (temp.next != null) Console.Write(" <-> ");
			temp = temp.next;
		}
		Console.WriteLine();
	}
	public void Rotate(int k)
	{
		if (head == null || head.next == null || k == 0) return;
		k = k % length;
		if (k == 0) return;
		int counter = 0;
		while (counter < k)
		{
			DLLNode oldHead = head;
			DLLNode oldTail = tail;
			DLLNode newTail = oldTail.prev;
			head = oldTail;
			head.prev = null;
			head.next = oldHead;
			oldHead.prev = head;
			tail = newTail;
			tail.next = null;
			counter++;
		}
	}
}
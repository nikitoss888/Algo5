using System;
using static System.Console;
using System.Diagnostics;
using static System.Diagnostics.Stopwatch;
using System.Text;

namespace Sorting
{
    class Program
    {
        public class Node //власне список
        {
            public int Data;
            public Node Next, Previous;
        }

        static Node NewNode(int data) //функція створення нового вузла
        {
            Node newNode = new Node();
            newNode.Data = data;
            newNode.Previous = newNode.Next = null;
            return newNode;
        }


        static void RandomFiller(int[] arr) //заповнює масив випадковими числами
        {
            Random random = new Random();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(-150, 150);
            }
        }

        static void SelectionSorting(int[] arr) //функція сортування вибором
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[min])
                        min = j;
                }
                int tmp = arr[i];
                arr[i] = arr[min];
                arr[min] = tmp;
            }
        }

        static void InsertionSortingArray(int[] arr) //функція сортування вставками (масив)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int j;
                int tmp = arr[i];

                for (j = i - 1; j >= 0; j--)
                {
                    if (arr[j] < tmp)
                        break;
                    arr[j + 1] = arr[j];
                }

                arr[j + 1] = tmp;
            }
        }

        static Node InsertionSortingList(Node Node, Node NewNode) //функція сортування вставками (список з додаванням вузлів)
        {
            Node current;

            if (Node == null)
            {
                Node = NewNode;
            }
            else if (Node.Data >= NewNode.Data)
            {
                NewNode.Next = Node;
                NewNode.Next.Previous = NewNode;
                Node = NewNode;
            }
            else
            {
                current = Node;

                while (current.Next != null && current.Next.Data < NewNode.Data)
                {
                    current = current.Next;
                }
                NewNode.Next = current.Next;
                if (current.Next != null)
                {
                    NewNode.Next.Previous = NewNode;
                }
                current.Next = NewNode;
                NewNode.Previous = current;
            }
            return Node;
        }

        static void Shell_SedgewickSorting(int[] arr) //функція сортування Шелла у модифікації Седжвіка
        {
            int step, i, j;
            int[] inc = new int[40];

            int p1 = 1, p2 = 1, p3 = 1, st = -1;
            do
            {
                if (++st % 2 != 0)
                    inc[st] = 8 * p1 - 6 * p2 + 1;
                else
                {
                    inc[st] = 9 * p1 - 9 * p3 + 1;
                    p2 *= 2;
                    p3 *= 2;
                }
                p1 *= 2;
            } while (3 * inc[st] < inc.Length);

            st = st > 0 ? --st : 0;

            while (st >= 0)
            {
                step = inc[st--];

                for (i = step; i < arr.Length; i++)
                {
                    int temp = arr[i];

                    for (j = i - step; (j >= 0) && (arr[j] > temp); j -= step)
                        arr[j + step] = arr[j];
                    arr[j + step] = temp;
                }
            }
        }

        static void Main()
        {
            InputEncoding = Encoding.Unicode;
            OutputEncoding = Encoding.Unicode;

            Stopwatch timer;
            int[] firstArray = new int[5000], secondArray = new int[10000], thirdArray = new int[50000], fourthArray = new int[100000];

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            WriteLine("Програма тестування різних методів сортування");

            {
                WriteLine("\tСортування вибором");

                RandomFiller(firstArray);
                timer = StartNew();
                SelectionSorting(firstArray);
                timer.Stop();
                WriteLine($"{firstArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                RandomFiller(secondArray);
                timer = StartNew();
                SelectionSorting(secondArray);
                timer.Stop();
                WriteLine($"{secondArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                RandomFiller(thirdArray);
                timer = StartNew();
                SelectionSorting(thirdArray);
                timer.Stop();
                WriteLine($"{thirdArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                RandomFiller(fourthArray);
                timer = StartNew();
                SelectionSorting(fourthArray);
                timer.Stop();
                WriteLine($"{fourthArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////

            {
                WriteLine("\tСортування вставками");
                RandomFiller(firstArray);
                timer = StartNew();
                InsertionSortingArray(firstArray);
                timer.Stop();
                WriteLine($"{firstArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                RandomFiller(secondArray);
                timer = StartNew();
                InsertionSortingArray(secondArray);
                timer.Stop();
                WriteLine($"{secondArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                RandomFiller(thirdArray);
                timer = StartNew();
                InsertionSortingArray(thirdArray);
                timer.Stop();
                WriteLine($"{thirdArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                RandomFiller(fourthArray);
                timer = StartNew();
                InsertionSortingArray(fourthArray);
                timer.Stop();
                WriteLine($"{fourthArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////

            {
                WriteLine("\tСортування списку вставками");
                Random random = new Random();
                Node node = null;
                timer = StartNew();
                for (int i = 0; i < firstArray.Length; i++)
                {
                    node = InsertionSortingList(node, NewNode(random.Next(-150, 150)));
                }
                timer.Stop();
                WriteLine($"{firstArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                node = null;
                timer = StartNew();
                for (int i = 0; i < secondArray.Length; i++)
                {
                    node = InsertionSortingList(node, NewNode(random.Next(-150, 150)));
                }
                timer.Stop();
                WriteLine($"{secondArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                node = null;
                timer = StartNew();
                for (int i = 0; i < thirdArray.Length; i++)
                {
                    node = InsertionSortingList(node, NewNode(random.Next(-150, 150)));
                }
                timer.Stop();
                WriteLine($"{thirdArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                node = null;
                timer = StartNew();
                for (int i = 0; i < fourthArray.Length; i++)
                {
                    node = InsertionSortingList(node, NewNode(random.Next(-150, 150)));
                }
                timer.Stop();
                WriteLine($"{fourthArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////

            {
                WriteLine("\tСортування Шелла, модифікація Седжвіка");
                RandomFiller(firstArray);
                timer = StartNew();
                Shell_SedgewickSorting(firstArray);
                timer.Stop();
                WriteLine($"{firstArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                RandomFiller(secondArray);
                timer = StartNew();
                Shell_SedgewickSorting(secondArray);
                timer.Stop();
                WriteLine($"{secondArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                RandomFiller(thirdArray);
                timer = StartNew();
                Shell_SedgewickSorting(thirdArray);
                timer.Stop();
                WriteLine($"{thirdArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");

                RandomFiller(fourthArray);
                timer = StartNew();
                Shell_SedgewickSorting(fourthArray);
                timer.Stop();
                WriteLine($"{fourthArray.Length} елементів = {timer.Elapsed.TotalSeconds} секунд");
            }
        }
    }
}
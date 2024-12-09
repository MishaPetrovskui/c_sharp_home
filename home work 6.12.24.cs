Console.Write("Enter the start of the range: ");
int start = int.Parse(Console.ReadLine());

Console.Write("Enter the end of the range: ");
int end = int.Parse(Console.ReadLine());

if (start > end)
{
    Console.WriteLine("The beginning of the range cannot be greater than the end!");
    return;
}

int a = 0, b = 1;
bool found = false;

Console.Write("Fibonacci numbers in the range {0} to {1}: ", start, end);

while (a <= end)
{
    if (a >= start)
    {
        if (found)
        {
            Console.Write(", ");
        }
        Console.Write(a);
        found = true;
    }
    int nextFib = a + b;
    a = b;
    b = nextFib;
}

if (!found)
    Console.WriteLine("ERROR!");
else
    Console.WriteLine();

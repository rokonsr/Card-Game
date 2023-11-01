// See https://aka.ms/new-console-template for more information
using CardGame;

int option = 0;

List<Player> players = new List<Player>();
Console.WriteLine("Card Game");

string[] cards = new string[52];
string[] numbers = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
string[] symbols = { "@", "#", "^", "*" };
//string[] letters = { "J", "Q", "K", "A" };

GenerateCards(cards, numbers, symbols);

OPTION:
Console.WriteLine();
Console.WriteLine("========Options==============================");
Console.WriteLine("1. Show Cards");
Console.WriteLine("2. Shuffle Cards");
Console.WriteLine("3. Distribute Cards");
Console.WriteLine("4. Show Result");
Console.WriteLine("Enter Options:");

try
{
    option = Convert.ToInt32(Console.ReadLine());

    if (option > 4)
    {
        Console.WriteLine("Please Enter Correct Option");
        goto OPTION;
    }
}
catch (Exception ex)
{
    ex.Message.ToString();
    Console.WriteLine("Please Enter Correct Option");
    goto OPTION;
}

if (option == 1)
{
    Console.WriteLine("===========ALL Cards========================");
    foreach (var n in cards)
    {
        Console.Write(n + "  ");
    }
    goto OPTION;
}
else if (option == 2)
{
    ShuffleCard(cards);
    Console.WriteLine("All Cards are shuffled");
    goto OPTION;
}
else if (option == 3)
{
    CardDistribution(cards, players);
    goto OPTION;

}
else if (option == 4)
{
    Result(players);
    goto OPTION;
}
else if (option == 5)
{
    System.Environment.Exit(1);
}
else
{
    goto OPTION;
}

Console.ReadKey();




static void GenerateCards(string[] cards, string[] numbers, string[] symbols)
{
    int j = 0;

    for (int i = 0; i < numbers.Length; i++)
    {
        cards[i + j] = numbers[i] + symbols[0];
        cards[i + 1 + j] = numbers[i] + symbols[1];
        cards[i + 2 + j] = numbers[i] + symbols[2];
        cards[i + 3 + j] = numbers[i] + symbols[3];

        j = j + 3;
    }
}

static void ShuffleCard(string[] cards)
{
    List<int> newIndexes = GenerateRandom(52);
    string[] newCrds = new string[52];
    for (int i = 0; i < 52; i++)
    {
        newCrds[i] = cards[newIndexes[i]];
    }

    for (int i = 0; i < 52; i++)
    {
        cards[i] = newCrds[i];
    }
}

static List<int> GenerateRandom(int count)
{
    Random random = new Random();
    HashSet<int> candidates = new HashSet<int>();
    while (candidates.Count < count)
    {
        candidates.Add(random.Next(52));
    }

    List<int> result = new List<int>();
    result.AddRange(candidates);

    // shuffle the results:
    int i = result.Count;
    while (i > 1)
    {
        i--;
        int k = random.Next(i + 1);
        int value = result[k];
        result[k] = result[i];
        result[i] = value;
    }
    return result;
}

static void CardDistribution(string[] cards, List<Player> players)
{
    players.Clear();
    for (int p = 1; p <= 4; p++)
    {
        Player player = new Player();
        player.Name = "Player " + p.ToString();
        player.Cards = new List<string>();
        players.Add(player);
    }

    for (int i = 0; i < cards.Length; i++)
    {
        players[0].Cards.Add(cards[i]);
        players[1].Cards.Add(cards[++i]);
        players[2].Cards.Add(cards[++i]);
        players[3].Cards.Add(cards[++i]);
    }

    foreach (var p in players)
    {
        Console.WriteLine();
        Console.Write(p.Name + " : ");
        foreach (var c in p.Cards)
            Console.Write(c + "\t");
    }
}

static void Result(List<Player> players)
{
    Console.WriteLine("=======Wining Cards and Result======================");
    foreach (var p in players)
    {
        GetMaxAlfanumeric(p);
        Console.WriteLine();
        Console.Write(p.Name + "  :");
        foreach (var wc in p.WiningCards)
            Console.Write(wc + "  ");

        //Console.Write("\t" + p.Result +"   S="+p.SymbolMarks);
    }

    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("WINNER : " + players.OrderByDescending(r => r.Result).ThenByDescending(d => d.CardLetterMarks).ThenByDescending(s => s.SymbolMarks).First().Name);
}

static void GetMaxAlfanumeric(Player p)
{
    int countA = p.Cards.Where(e => e.StartsWith('A')).Count();
    int countK = p.Cards.Where(e => e.StartsWith('K')).Count();
    int countQ = p.Cards.Where(e => e.StartsWith('Q')).Count();
    int countJ = p.Cards.Where(e => e.StartsWith('J')).Count();
    string cardChar = Largest(new int[] { countA, countK, countQ, countJ }, 4);
    p.CardLetterMarks = cardChar == "A" ? 4 : cardChar == "K" ? 3 : cardChar == "Q" ? 2 : 1;
    p.WiningCards = p.Cards.Where(e => e.StartsWith(Largest(new int[] { countA, countK, countQ, countJ }, 4))).ToList();
    p.Result = p.WiningCards.Count().ToString();
    p.SymbolMarks = p.WiningCards.Sum(s => s.EndsWith("*") ? 4 : s.EndsWith("^") ? 3 : s.EndsWith("#") ? 2 : 1);
}

static string Largest(int[] arr, int n)
{
    int i;
    int retIndex = 0;
    // Initialize maximum element
    int max = arr[0];
    for (i = 1; i < n; i++)
        if (arr[i] > max)
        {
            max = arr[i];
            retIndex = i;
        }

    return retIndex == 0 ? "A" : retIndex == 1 ? "K" : retIndex == 2 ? "Q" : "J";
}

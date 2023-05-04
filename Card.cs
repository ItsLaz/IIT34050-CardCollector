namespace CardCollector;

public class Card
{
    public string Name { get; set; }
    public bool Have { get; set; }

    public Card(string name)
    {
        Name = name;
        Have = false;
    }
}
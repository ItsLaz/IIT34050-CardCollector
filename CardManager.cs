namespace CardCollector;

public class CardManager
{
    public List<CardCollection> Collections { get; set; }

    public CardManager()
    {
        Collections = new List<CardCollection>();
    }

    public void AddCollection(CardCollection collection)
    {
        Collections.Add(collection);
    }
}
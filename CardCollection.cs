namespace CardCollector;

public class CardCollection
{
    public string CollectionName { get; set; }
    public List<Card> Cards { get; set; }

    public CardCollection(string collectionName)
    {
        CollectionName = collectionName;
        Cards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }
}
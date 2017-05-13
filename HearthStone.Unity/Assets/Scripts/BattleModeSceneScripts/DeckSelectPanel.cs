using HearthStone.Library;
using HearthStone.Protocol;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectPanel : MonoBehaviour
{
    [SerializeField]
    private Toggle deckTogglePrefab;
    [SerializeField]
    private RectTransform content;

    private void Start()
    {
        RenderAllDeck();
        content.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(700,700);
        EndPointManager.EndPoint.Player.OnDeckChanged += OnDeckChanged;
    }
    private void OnDestroy()
    {
        EndPointManager.EndPoint.Player.OnDeckChanged -= OnDeckChanged;
    }
    private void OnDeckChanged(Deck deck, DataChangeCode changeCode)
    {
        RenderAllDeck();
    }
    private void RenderAllDeck()
    {
        int deckCount = EndPointManager.EndPoint.Player.Decks.Count();

        content.sizeDelta = new Vector2(700, 700 * ((deckCount + 2) / 3));
        lock (this)
        {
            foreach (RectTransform child in content)
            {
                Destroy(child.gameObject);
            }
            foreach (Deck deck in EndPointManager.EndPoint.Player.Decks)
            {
                Toggle block = Instantiate(deckTogglePrefab, content);
                block.transform.localScale = Vector3.one;
                block.GetComponentInChildren<Text>().text = deck.DeckName;
                block.group = content.GetComponent<ToggleGroup>();
                block.name = deck.DeckID.ToString();
            }
        }
    }
}

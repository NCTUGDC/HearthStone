using HearthStone.Library;
using HearthStone.Protocol;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AllDeckPanel : MonoBehaviour
{
    public Button deckButtonPrefab;
    public RectTransform content;
    public GameObject createDeckPanel;
    public DeckCardPanel deckCardPanel;

    private void Start()
    {
        RenderAllDeck();
        EndPointManager.EndPoint.Player.OnDeckChanged += OnDeckChanged;
    }

    private void OnDeckChanged(Deck deck, DataChangeCode changeCode)
    {
        RenderAllDeck();
    }
    private void RenderAllDeck()
    {
        int cardCount = EndPointManager.EndPoint.Player.Decks.Count();

        content.sizeDelta = new Vector2(230, 60 * cardCount);
        lock(this)
        {
            foreach (RectTransform child in content)
            {
                Destroy(child.gameObject);
            }
            foreach (Deck deck in EndPointManager.EndPoint.Player.Decks)
            {
                Button block = Instantiate(deckButtonPrefab);
                block.transform.SetParent(content);
                block.transform.localScale = Vector3.one;
                block.GetComponentInChildren<Text>().text = deck.DeckName;
                int deckID = deck.DeckID;
                block.onClick.AddListener(() => deckCardPanel.StartBuildDeck(deckID));
            }
        }
    }

    public void OpenCreateDeckPanel()
    {
        createDeckPanel.transform.Find("InputField/Text").GetComponent<Text>().text = "自訂套牌";
        createDeckPanel.SetActive(true);
    }
    public void CreateDeck()
    {
        string deckName = createDeckPanel.transform.Find("InputField/Text").GetComponent<Text>().text;
        if (deckName.Length >= 1)
            EndPointManager.EndPoint.Player.OperationManager.CreateDeck(deckName);
        else
            EndPointManager.EndPoint.Player.OperationManager.CreateDeck("自訂套牌");
    }
}

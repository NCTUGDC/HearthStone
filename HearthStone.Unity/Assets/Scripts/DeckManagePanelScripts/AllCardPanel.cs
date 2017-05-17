using HearthStone.Library;
using HearthStone.Library.Cards;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AllCardPanel : MonoBehaviour
{
    public ServantCardBlock servantCardBlockPrefab;
    public SpellCardBlock spellCardBlockPrefab;
    public WeaponCardBlock weaponCardBlockPrefab;
    public RectTransform content;
    public DeckCardPanel deckCardPanel;

    private void Start()
    {
        int cardCount = CardManager.Instance.Cards.Count();

        content.sizeDelta = new Vector2(200 * (cardCount / 2 + 1), 500);
        foreach(GameObject child in content)
        {
            Destroy(child);
        }
        foreach (Card card in CardManager.Instance.Cards)
        {
            Button button = null;
            if (card is ServantCard)
            {
                ServantCardBlock block = Instantiate(servantCardBlockPrefab);
                block.SetCard(card as ServantCard);
                block.transform.SetParent(content);
                block.transform.localScale = Vector3.one;
                button = block.GetComponent<Button>();
            }
            else if (card is SpellCard)
            {
                SpellCardBlock block = Instantiate(spellCardBlockPrefab);
                block.SetCard(card as SpellCard);
                block.transform.SetParent(content);
                block.transform.localScale = Vector3.one;
                button = block.GetComponent<Button>();
            }
            else if(card is WeaponCard)
            {
                WeaponCardBlock block = Instantiate(weaponCardBlockPrefab);
                block.SetCard(card as WeaponCard);
                block.transform.SetParent(content);
                block.transform.localScale = Vector3.one;
                button = block.GetComponent<Button>();
            }
            int cardID = card.CardID;
            button.onClick.AddListener(() => 
            {
                if(deckCardPanel.IsUnderBuildDeck)
                {
                    deckCardPanel.AddCardToDeck(cardID);
                }
            });
        }
    }
}

using HearthStone.Library;
using HearthStone.Library.Cards;
using UnityEngine.UI;

public class ServantCardBlock : CardBlock
{
    private Text attackText;
    private Text healthText;

    protected override void Awake()
    {
        base.Awake();
        attackText = transform.Find("Attack/Text").GetComponent<Text>();
        healthText = transform.Find("Health/Text").GetComponent<Text>();
    }
    public override void SetCard(Card card)
    {
        base.SetCard(card);
        ServantCard servantCard = card as ServantCard;
        attackText.text = servantCard.Attack.ToString();
        healthText.text = servantCard.Health.ToString();
    }
}

using HearthStone.Library;
using HearthStone.Library.CardRecords;
using HearthStone.Library.Cards;
using UnityEngine;
using UnityEngine.UI;

public class ServantCardRecordBlock : CardRecordBlock
{
    private Text attackText;
    private Text healthText;

    protected override void Awake()
    {
        base.Awake();
        attackText = transform.Find("LeftNumber/Text").GetComponent<Text>();
        healthText = transform.Find("RightNumber/Text").GetComponent<Text>();
    }

    public override void SetCard(CardRecord card, int selfGamePlayerID)
    {
        base.SetCard(card, selfGamePlayerID);
        ServantCardRecord servantCard = card as ServantCardRecord;
        attackText.text = servantCard.Attack.ToString();
        healthText.text = servantCard.RemainedHealth.ToString();

        if (servantCard.Attack > (servantCard.Card as ServantCard).Attack)
        {
            attackText.color = Color.green;
        }

        if (servantCard.RemainedHealth == servantCard.Health)
        {
            if (servantCard.Health > (servantCard.Card as ServantCard).Health)
            {
                healthText.color = Color.green;
            }
        }
        else
        {
            healthText.color = Color.red;
        }
    }
}

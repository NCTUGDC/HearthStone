using HearthStone.Library;
using HearthStone.Library.CardRecords;
using HearthStone.Library.Cards;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCardRecordBlock : CardRecordBlock
{
    private Text attackText;
    private Text durabilityText;

    protected override void Awake()
    {
        base.Awake();
        attackText = transform.Find("LeftNumber/Text").GetComponent<Text>();
        durabilityText = transform.Find("RightNumber/Text").GetComponent<Text>();
    }

    public override void SetCard(CardRecord card, int selfGamePlayerID)
    {
        base.SetCard(card, selfGamePlayerID);
        WeaponCardRecord weaponCard = card as WeaponCardRecord;
        attackText.text = weaponCard.Attack.ToString();
        durabilityText.text = weaponCard.RemainedDurability.ToString();

        if (weaponCard.Attack > (weaponCard.Card as WeaponCard).Attack)
        {
            attackText.color = Color.green;
        }

        if (weaponCard.RemainedDurability == weaponCard.Durability)
        {
            if (weaponCard.Durability > (weaponCard.Card as WeaponCard).Durability)
            {
                durabilityText.color = Color.green;
            }
        }
        else
        {
            durabilityText.color = Color.red;
        }
    }
}

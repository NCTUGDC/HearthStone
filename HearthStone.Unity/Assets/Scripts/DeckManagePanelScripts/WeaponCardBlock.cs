using HearthStone.Library;
using HearthStone.Library.Cards;
using UnityEngine.UI;

public class WeaponCardBlock : CardBlock
{
    private Text attackText;
    private Text durabilityText;

    protected override void Awake()
    {
        base.Awake();
        attackText = transform.Find("Attack/Text").GetComponent<Text>();
        durabilityText = transform.Find("Durability/Text").GetComponent<Text>();
    }
    public override void SetCard(Card card)
    {
        base.SetCard(card);
        WeaponCard weaponCard = card as WeaponCard;
        attackText.text = weaponCard.Attack.ToString();
        durabilityText.text = weaponCard.Durability.ToString();
    }
}

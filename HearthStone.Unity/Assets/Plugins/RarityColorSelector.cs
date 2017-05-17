using HearthStone.Protocol;
using UnityEngine;

public static class RarityColorSelector
{
    public static Color RarityToColor(RarityCode rarity)
    {
        switch (rarity)
        {
            case RarityCode.Common:
                return Color.white;
            case RarityCode.Rare:
                return Color.blue;
            case RarityCode.Epic:
                return new Color(155f / 255, 94f / 255, 246f / 255);
            case RarityCode.Legendary:
                return new Color(255f / 255, 193f / 255, 58f / 255);
            default:
                return Color.gray;
        }
    }
}

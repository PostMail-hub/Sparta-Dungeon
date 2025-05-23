using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Hunger,
    Health,
    Buff
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[System.Serializable]
public class ItemDataBuff
{
    public float duration;
    public float speedMultiplier;
    public float jumpMultiplier;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("정보")]
    public string displayName;
    public string description;
    public ItemType type;

    [Header("소모품")]
    public ItemDataConsumable[] consumables;

    [Header("버프")]
    public ItemDataBuff buffData = new ItemDataBuff();
}
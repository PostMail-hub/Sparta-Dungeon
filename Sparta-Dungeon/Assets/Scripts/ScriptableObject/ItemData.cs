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
    [Header("����")]
    public string displayName;
    public string description;
    public ItemType type;

    [Header("�Ҹ�ǰ")]
    public ItemDataConsumable[] consumables;

    [Header("����")]
    public ItemDataBuff buffData = new ItemDataBuff();
}
using UnityEngine;

// �������� ��ü ������ ���� (�ڿ�, ���, �Ҹ�ǰ ��)
public enum ItemType
{
    Resource,  // �ڿ��� ������ ( ������Ʈ������ �������� ���Ͽ����� �з��� ������ )
    Consumable // ��� �� ȿ���� �ִ� ������
}

// �Ҹ� �������� ���� ������ ����
public enum ConsumableType
{
    Health, // ü�� ȸ��
    Buff    // �ɷ�ġ ����
}

// �Ҹ�ǰ �������� � ȿ���� ������ ��Ÿ���� ������ ����
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type; // � ������ �Ҹ�ǰ����
    public float value;         // ȸ���� �Ǵ� ȿ�� ��ġ
}

// ���� ȿ���� ���� ������ ����
[System.Serializable]
public class ItemDataBuff
{
    public float duration;          // ���� ���� �ð�  
    public float speedMultiplier;   // �̵� �ӵ� ����
    public float jumpMultiplier;    // ������ ����
}

// ScriptableObject�� ����Ǵ� ������ ������ ����
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("����")]
    public string displayName;  // ������ �̸�
    public string description;  // ������ ����
    public ItemType type;       // ������ ����

    [Header("�Ҹ�ǰ")]
    public ItemDataConsumable[] consumables; // �Ҹ�ǰ ȿ�� ���

    [Header("����")]
    public ItemDataBuff buffData = new ItemDataBuff(); // ���� ������
}
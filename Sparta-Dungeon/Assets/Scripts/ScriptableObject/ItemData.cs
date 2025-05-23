using UnityEngine;

// 아이템의 전체 유형을 정의 (자원, 장비, 소모품 등)
public enum ItemType
{
    Resource,  // 자원형 아이템 ( 프로젝트에서는 구현하지 못하였으나 분류를 나눠둠 )
    Consumable // 사용 시 효과가 있는 아이템
}

// 소모 아이템의 세부 유형을 정의
public enum ConsumableType
{
    Health, // 체력 회복
    Buff    // 능력치 버프
}

// 소모품 아이템이 어떤 효과를 갖는지 나타내는 데이터 구조
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type; // 어떤 종류의 소모품인지
    public float value;         // 회복량 또는 효과 수치
}

// 버프 효과에 대한 데이터 구조
[System.Serializable]
public class ItemDataBuff
{
    public float duration;          // 버프 지속 시간  
    public float speedMultiplier;   // 이동 속도 배율
    public float jumpMultiplier;    // 점프력 배율
}

// ScriptableObject로 저장되는 아이템 데이터 정의
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("정보")]
    public string displayName;  // 아이템 이름
    public string description;  // 아이템 설명
    public ItemType type;       // 아이템 유형

    [Header("소모품")]
    public ItemDataConsumable[] consumables; // 소모품 효과 목록

    [Header("버프")]
    public ItemDataBuff buffData = new ItemDataBuff(); // 버프 데이터
}
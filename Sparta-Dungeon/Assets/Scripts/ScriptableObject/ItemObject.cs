using UnityEngine;

// 상호작용 가능한 오브젝트를 위한 인터페이스 정의
public interface IInteractable
{
    public string GetInteractPrompt(); // 상호작용 시 표시할 프롬프트(문구) 반환
    public void OnInteract();          // 실제 상호작용 실행 메서드
}

// 아이템 오브젝트 클래스, IInteractable 인터페이스를 구현함
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data; // 아이템의 데이터 (ScriptableObject로 관리됨)

    // 상호작용 프롬프트를 문자열로 반환 (아이템 이름 + 설명)
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    // 플레이어가 상호작용할 때 실행되는 메서드
    public void OnInteract()
    {
        // 플레이어 상태 시스템 가져오기
        PlayerConditions player = CharacterManager.Instance.Player.condition;

        // 아이템에 포함된 소모 효과들 모두 순회
        foreach (var consumable in data.consumables)
        {
            // 체력 관련 소모품일 경우
            if (consumable.type == ConsumableType.Health)
            {
                float value = consumable.value;

                if (value >= 0)
                    player.Heal((int)value); // 체력 회복

                else
                    player.TakePhysicalDamage((int)-value); // 음수면 데미지 처리
            }

            // 버프 관련 소모품일 경우
            else if (consumable.type == ConsumableType.Buff)
            {
                var buffData = data.buffData;

                if (buffData != null)
                {
                    // 플레이어에게 버프 컨트롤러가 있는 경우 버프 적용
                    PlayerBuffController buffController = CharacterManager.Instance.Player.GetComponent<PlayerBuffController>();
                    buffController.ApplyBuff(buffData.duration, buffData.speedMultiplier, buffData.jumpMultiplier);
                }
            }
        }
        //사용한 아이템은 소모용 버프 아이템이므로 맵에서 오브젝트 삭제
        Destroy(gameObject);
    }
}
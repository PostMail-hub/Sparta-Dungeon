using UnityEngine;

// 플레이어의 체력 등 상태 정보를 UI에 연결해주는 클래스
public class UICondition : MonoBehaviour
{
    public Condition health; // 체력 상태를 표현하는 Condition 인스턴스 (UI에 바인딩될 수 있음)

    private void Start()
    {
        // 게임 시작 시, 플레이어 상태 시스템에서 이 UICondition을 연결
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}

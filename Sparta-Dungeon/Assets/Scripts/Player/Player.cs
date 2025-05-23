using UnityEngine;

// 플레이어 본체를 구성하는 주요 컴포넌트들을 관리하는 클래스
public class Player : MonoBehaviour
{
    public PlayerController controller;             // 이동, 점프, 카메라 회전 등 조작 담당
    public PlayerConditions condition;              // 체력, 데미지 등 상태 관리
    public PlayerBuffController buffController;     // 버프 효과 적용 관리

    public ItemData itemData;                       // 플레이어가 들고 있는 아이템 정보 (테스트용/기본 아이템 등)

    private void Awake()
    {
        // 게임 시작 시 자신을 CharacterManager에 등록 (전역 접근 가능하게)
        CharacterManager.Instance.Player = this;

        // 주요 컴포넌트들 초기화
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerConditions>();
        // buffController는 외부에서 직접 연결되거나 따로 초기화될 수 있음
    }
}
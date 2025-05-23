using System.Collections;
using UnityEngine;

// 플레이어에게 버프 효과(이동 속도, 점프력)를 적용하고 일정 시간 후 원상복구하는 컨트롤러

public class PlayerBuffController : MonoBehaviour
{
    private PlayerController playerController;  // 플레이어 컨트롤러 참조
    private Coroutine activeBuffCoroutine;      // 현재 활성화된 버프 코루틴 참조

    //원래 가지고 있던 이동속도와 점프력을 저장해두는 곳
    private float originalMoveSpeed;
    private float originalJumpPower;

    private float remainingTime;                // 남은 버프 시간
    private float currentSpeedMultiplier = 1f;  // 현재 적용된 속도 배율
    private float currentJumpMultiplier = 1f;   // 현재 적용된 점프 배율

    public UIBuffBar buffBarUI;                 // 버프 UI 바 참조

    private void Awake()
    {
        playerController = GetComponent<PlayerController>(); // PlayerController 가져오기

        // 초기 상태 저장
        originalMoveSpeed = playerController.moveSpeed;
        originalJumpPower = playerController.jumpPower;

        // buffBarUI가 연결되어 있지 않다면 자동으로 찾아 연결
        if (buffBarUI == null)
        {
            buffBarUI = FindObjectOfType<UIBuffBar>();
        }
    }


    // 버프 적용 메서드
    public void ApplyBuff(float duration, float speedMultiplier, float jumpMultiplier)
    {
        // 기존 버프가 있으면 중단
        if (activeBuffCoroutine != null)
        {
            StopCoroutine(activeBuffCoroutine);
        }

        // UI 바 시작
        buffBarUI?.StartBuff(duration);

        // 버프 수치 적용
        currentSpeedMultiplier = speedMultiplier;
        currentJumpMultiplier = jumpMultiplier;
        remainingTime = duration;

        // 코루틴 실행
        activeBuffCoroutine = StartCoroutine(BuffRoutine());
    }

    // 버프를 적용하고 일정 시간 후 원래대로 돌리는 코루틴
    private IEnumerator BuffRoutine()
    {
        // 플레이어의 속도와 점프력 증가
        playerController.moveSpeed = originalMoveSpeed * currentSpeedMultiplier;
        playerController.jumpPower = originalJumpPower * currentJumpMultiplier;

        // 버프 유지 시간 동안 대기
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        // 버프 시간 종료 후 원래 수치로 복원
        playerController.moveSpeed = originalMoveSpeed;
        playerController.jumpPower = originalJumpPower;

        activeBuffCoroutine = null; // 현재 코루틴 참조 초기화
    }
}

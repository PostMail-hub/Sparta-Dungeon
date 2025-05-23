using System.Collections;
using UnityEngine;

public class PlayerBuffController : MonoBehaviour
{
    private PlayerController playerController;
    private Coroutine activeBuffCoroutine;

    //원래 가지고 있던 이동속도와 점프력을 저장해두는 곳
    private float originalMoveSpeed;
    private float originalJumpPower;

    private float remainingTime;
    private float currentSpeedMultiplier = 1f;
    private float currentJumpMultiplier = 1f;

    public UIBuffBar buffBarUI;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        originalMoveSpeed = playerController.moveSpeed;
        originalJumpPower = playerController.jumpPower;

        if (buffBarUI == null)
        {
            buffBarUI = FindObjectOfType<UIBuffBar>();
        }
    }

    public void ApplyBuff(float duration, float speedMultiplier, float jumpMultiplier)
    {
        // 이미 버프가 걸려있다면 수치를 갱신함
        if (activeBuffCoroutine != null)
        {
            StopCoroutine(activeBuffCoroutine);
        }

        buffBarUI?.StartBuff(duration);

        // 버프 수치 갱신
        currentSpeedMultiplier = speedMultiplier;
        currentJumpMultiplier = jumpMultiplier;
        remainingTime = duration;

        // 코루틴 재시작
        activeBuffCoroutine = StartCoroutine(BuffRoutine());
    }

    private IEnumerator BuffRoutine()
    {
        // 버프 적용
        playerController.moveSpeed = originalMoveSpeed * currentSpeedMultiplier;
        playerController.jumpPower = originalJumpPower * currentJumpMultiplier;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        // 지속 시간이 끝나면 원래 수치로 복원
        playerController.moveSpeed = originalMoveSpeed;
        playerController.jumpPower = originalJumpPower;

        activeBuffCoroutine = null;
    }
}

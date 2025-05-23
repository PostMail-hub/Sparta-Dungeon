using System.Collections;
using UnityEngine;

public class PlayerBuffController : MonoBehaviour
{
    private PlayerController playerController;
    private Coroutine activeBuffCoroutine;

    //���� ������ �ִ� �̵��ӵ��� �������� �����صδ� ��
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
        // �̹� ������ �ɷ��ִٸ� ��ġ�� ������
        if (activeBuffCoroutine != null)
        {
            StopCoroutine(activeBuffCoroutine);
        }

        buffBarUI?.StartBuff(duration);

        // ���� ��ġ ����
        currentSpeedMultiplier = speedMultiplier;
        currentJumpMultiplier = jumpMultiplier;
        remainingTime = duration;

        // �ڷ�ƾ �����
        activeBuffCoroutine = StartCoroutine(BuffRoutine());
    }

    private IEnumerator BuffRoutine()
    {
        // ���� ����
        playerController.moveSpeed = originalMoveSpeed * currentSpeedMultiplier;
        playerController.jumpPower = originalJumpPower * currentJumpMultiplier;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        // ���� �ð��� ������ ���� ��ġ�� ����
        playerController.moveSpeed = originalMoveSpeed;
        playerController.jumpPower = originalJumpPower;

        activeBuffCoroutine = null;
    }
}

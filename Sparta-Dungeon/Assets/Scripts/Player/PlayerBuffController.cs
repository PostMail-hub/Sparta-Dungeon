using System.Collections;
using UnityEngine;

// �÷��̾�� ���� ȿ��(�̵� �ӵ�, ������)�� �����ϰ� ���� �ð� �� ���󺹱��ϴ� ��Ʈ�ѷ�

public class PlayerBuffController : MonoBehaviour
{
    private PlayerController playerController;  // �÷��̾� ��Ʈ�ѷ� ����
    private Coroutine activeBuffCoroutine;      // ���� Ȱ��ȭ�� ���� �ڷ�ƾ ����

    //���� ������ �ִ� �̵��ӵ��� �������� �����صδ� ��
    private float originalMoveSpeed;
    private float originalJumpPower;

    private float remainingTime;                // ���� ���� �ð�
    private float currentSpeedMultiplier = 1f;  // ���� ����� �ӵ� ����
    private float currentJumpMultiplier = 1f;   // ���� ����� ���� ����

    public UIBuffBar buffBarUI;                 // ���� UI �� ����

    private void Awake()
    {
        playerController = GetComponent<PlayerController>(); // PlayerController ��������

        // �ʱ� ���� ����
        originalMoveSpeed = playerController.moveSpeed;
        originalJumpPower = playerController.jumpPower;

        // buffBarUI�� ����Ǿ� ���� �ʴٸ� �ڵ����� ã�� ����
        if (buffBarUI == null)
        {
            buffBarUI = FindObjectOfType<UIBuffBar>();
        }
    }


    // ���� ���� �޼���
    public void ApplyBuff(float duration, float speedMultiplier, float jumpMultiplier)
    {
        // ���� ������ ������ �ߴ�
        if (activeBuffCoroutine != null)
        {
            StopCoroutine(activeBuffCoroutine);
        }

        // UI �� ����
        buffBarUI?.StartBuff(duration);

        // ���� ��ġ ����
        currentSpeedMultiplier = speedMultiplier;
        currentJumpMultiplier = jumpMultiplier;
        remainingTime = duration;

        // �ڷ�ƾ ����
        activeBuffCoroutine = StartCoroutine(BuffRoutine());
    }

    // ������ �����ϰ� ���� �ð� �� ������� ������ �ڷ�ƾ
    private IEnumerator BuffRoutine()
    {
        // �÷��̾��� �ӵ��� ������ ����
        playerController.moveSpeed = originalMoveSpeed * currentSpeedMultiplier;
        playerController.jumpPower = originalJumpPower * currentJumpMultiplier;

        // ���� ���� �ð� ���� ���
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        // ���� �ð� ���� �� ���� ��ġ�� ����
        playerController.moveSpeed = originalMoveSpeed;
        playerController.jumpPower = originalJumpPower;

        activeBuffCoroutine = null; // ���� �ڷ�ƾ ���� �ʱ�ȭ
    }
}

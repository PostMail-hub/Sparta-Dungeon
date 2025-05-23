using UnityEngine;
using UnityEngine.UI;

public class UIBuffBar : MonoBehaviour
{
    public Image barImage;           // ������ ǥ���� UI �̹��� (Fill ��� ���)
    private float totalDuration;     // ������ ��ü ���� �ð�
    private float timeLeft;          // ���� ���� �ð�
    private bool isRunning = false;  // ���� Ÿ�̸Ӱ� ���� ������ ����

    public void StartBuff(float duration)
    {
        totalDuration = duration;            // ��ü ���� �ð� ����
        timeLeft = duration;                 // ���� �ð� �ʱ�ȭ
        isRunning = true;                    // Ÿ�̸� ����

        barImage.fillAmount = 1f;            // �ٸ� �� ä��
        barImage.gameObject.SetActive(true); // UI �� Ȱ��ȭ
    }

    private void Update()
    {
        if (!isRunning) return;  // ���� ���� �ƴϸ� �ƹ��͵� ���� ����

        timeLeft -= Time.deltaTime;                                     // ���� �ð� ����
        barImage.fillAmount = Mathf.Clamp01(timeLeft / totalDuration);  // ���� ä���� ���� ����

        // �ð��� �� �Ǹ� ���� ���� ó��
        if (timeLeft <= 0f)
        {
            isRunning = false;                    // Ÿ�̸� ����
            barImage.fillAmount = 0f;             // �ٸ� ���
            barImage.gameObject.SetActive(false); // UI �� ��Ȱ��ȭ
        }
    }
}
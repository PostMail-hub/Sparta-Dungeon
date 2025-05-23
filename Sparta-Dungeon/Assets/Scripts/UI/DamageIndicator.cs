using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾ �������� �Ծ��� �� ȭ�鿡 ������ �÷��ø� ���� Ŭ����    
public class DamageIndicator : MonoBehaviour
{
    public Image image;         // ȭ�鿡 ��� UI �̹��� (���� ȿ��)
    public float flashSpeed;    // �÷��ð� ������� �ӵ�

    private Coroutine coroutine; // ���� ���� ���� �ڷ�ƾ ����

    private void Start()
    {
        // �÷��̾ �������� �Ծ��� �� Flash �޼��尡 ȣ��ǵ��� �̺�Ʈ ���
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }

    // �������� �Ծ��� �� ȣ��Ǵ� �޼���
    public void Flash()
    {
        // ���� �÷��� �ڷ�ƾ�� ���� ���̸� ����
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        // �̹��� Ȱ��ȭ �� �ʱ� ���� ����
        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 105f / 255f); // ���� ���� ��
        coroutine = StartCoroutine(FadeAway()); // ���� ������� ȿ�� ����
    }

    // �̹����� ���� ���������� ������� �ڷ�ƾ
    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f; // ���� ����
        float a = startAlpha;

        // ���İ��� 0�� �� ������ �ݺ��ϸ� �̹��� ���� �����ϰ�
        while (a > 0.0f)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime; // ���� ������ ����
            image.color = new Color(1f, 100f / 255f, 100f / 255f, a); // ���� �������� ������
            yield return null;
        }

        image.enabled = false; // ������ ������� ��Ȱ��ȭ
    }
}
using UnityEngine;
using UnityEngine.UI;

// ü��, ���� ���� ���� ��ġ�� �����ϰ� UI �ٿ� �����ϴ� Ŭ����
public class Condition : MonoBehaviour
{
    public float curValue;        // ���� ��ġ
    public float maxValue;        // �ִ� ��ġ
    public float startValue;      // ���� ���� �� �ʱ� ��ġ
    public float passiveValue;    // �ʴ� ȸ��/���� ��ġ (���� Ȯ�� ����)
    public Image uiBar;           // UI �� (Image ������Ʈ�� FillAmount�� ǥ��)

    private void Start()
    {
        curValue = startValue; // ���� �� ��ġ�� �ʱⰪ���� ����
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage(); // �� �����Ӹ��� UI �� ����
    }

    // ��ġ�� ������Ű���� �ִ밪�� ���� �ʵ��� ����
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    // ��ġ�� ���ҽ�Ű���� 0 �̸����� �������� �ʵ��� ����
    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    // ���� ��ġ�� ������ ��ȯ (0 ~ 1 ����)
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
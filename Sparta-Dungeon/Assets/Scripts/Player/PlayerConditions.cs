using System;
using UnityEngine;

// ������ �������� ���� �� �ִ� ��� ������ �������̽� ����
public interface IDamagable
{
    void TakePhysicalDamage(int damage); // �������� �Դ� �޼���
}

// �÷��̾��� ü�� �� ������ ó��, UI ���� ���� ����ϴ� Ŭ����
public class PlayerConditions : MonoBehaviour, IDamagable 
{
    public UICondition uiCondition; // �÷��̾� ���¸� UI�� �����ϴ� ������Ʈ

    // ü�� ���¸� ��Ÿ���� ������Ƽ (UICondition ������ health�� ����)
    Condition health { get { return uiCondition.health; } }

    // �������� �Ծ��� �� �߻��ϴ� �̺�Ʈ
    public event Action onTakeDamage;

    void Update()
    {
        // �� �����Ӹ��� ü���� 0 �����̸� ��� ó��
        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    // ü���� ȸ���ϴ� �޼���
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    // ��� ó�� �޼��� (���� ����� ������ �ð��� ����)
    public void Die()
    {
        Time.timeScale = 0; // ������ ����
    }

    // �������̽� ����: ���� �������� ���� �� ȣ��Ǵ� �޼���
    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage); // ü�� ����
        onTakeDamage?.Invoke();  // ������ �� �̺�Ʈ �߻�
    }
}
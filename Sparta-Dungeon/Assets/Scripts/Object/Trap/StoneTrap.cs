using System.Collections.Generic;
using UnityEngine;

// ���� �ֱ⸶�� ���� �� ��󿡰� ���ظ� �ִ� ���� Ŭ����

public class StoneTrap : MonoBehaviour
{
    public int damage;          // ��󿡰� ���� ���ط�
    public float damageRate;    // ���ظ� �ִ� �ֱ� (�� ����)        

    private List<IDamagable> things = new List<IDamagable>(); // ���ظ� ���� �� �ִ� ��� ���

    private void Start()
    {
        // damageRate �������� DealDamage() �޼��带 �ݺ� ȣ��
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    // ����Ʈ�� �ִ� ���鿡�� ���ظ� �ִ� �޼���
    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage); // �� ��󿡰� ���� ���ظ� ����
        }
    }

    // ���� ������ ���� ����� IDamagable�� �����ϰ� �ִٸ� ��Ͽ� �߰�
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            things.Add(damagable);
        }
    }

    // ���� �������� ����� ��Ͽ��� ����
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            things.Remove(damagable);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

// 일정 주기마다 범위 내 대상에게 피해를 주는 함정 클래스

public class StoneTrap : MonoBehaviour
{
    public int damage;          // 대상에게 입힐 피해량
    public float damageRate;    // 피해를 주는 주기 (초 단위)        

    private List<IDamagable> things = new List<IDamagable>(); // 피해를 입힐 수 있는 대상 목록

    private void Start()
    {
        // damageRate 간격으로 DealDamage() 메서드를 반복 호출
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    // 리스트에 있는 대상들에게 피해를 주는 메서드
    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage); // 각 대상에게 물리 피해를 가함
        }
    }

    // 함정 범위에 들어온 대상이 IDamagable을 구현하고 있다면 목록에 추가
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            things.Add(damagable);
        }
    }

    // 함정 범위에서 벗어나면 목록에서 제거
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            things.Remove(damagable);
        }
    }
}
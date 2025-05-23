using System;
using UnityEngine;

// 물리적 데미지를 받을 수 있는 대상에 적용할 인터페이스 정의
public interface IDamagable
{
    void TakePhysicalDamage(int damage); // 데미지를 입는 메서드
}

// 플레이어의 체력 및 데미지 처리, UI 연동 등을 담당하는 클래스
public class PlayerConditions : MonoBehaviour, IDamagable 
{
    public UICondition uiCondition; // 플레이어 상태를 UI와 연동하는 컴포넌트

    // 체력 상태를 나타내는 프로퍼티 (UICondition 내부의 health에 접근)
    Condition health { get { return uiCondition.health; } }

    // 데미지를 입었을 때 발생하는 이벤트
    public event Action onTakeDamage;

    void Update()
    {
        // 매 프레임마다 체력이 0 이하이면 사망 처리
        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    // 체력을 회복하는 메서드
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    // 사망 처리 메서드 (현재 기능은 게임의 시간만 멈춤)
    public void Die()
    {
        Time.timeScale = 0; // 게임을 멈춤
    }

    // 인터페이스 구현: 물리 데미지를 입을 때 호출되는 메서드
    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage); // 체력 감소
        onTakeDamage?.Invoke();  // 데미지 후 이벤트 발생
    }
}
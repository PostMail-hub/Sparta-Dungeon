using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 플레이어가 데미지를 입었을 때 화면에 붉은색 플래시를 띄우는 클래스    
public class DamageIndicator : MonoBehaviour
{
    public Image image;         // 화면에 띄울 UI 이미지 (붉은 효과)
    public float flashSpeed;    // 플래시가 사라지는 속도

    private Coroutine coroutine; // 현재 실행 중인 코루틴 참조

    private void Start()
    {
        // 플레이어가 데미지를 입었을 때 Flash 메서드가 호출되도록 이벤트 등록
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }

    // 데미지를 입었을 때 호출되는 메서드
    public void Flash()
    {
        // 기존 플래시 코루틴이 실행 중이면 중지
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        // 이미지 활성화 및 초기 색상 설정
        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 105f / 255f); // 밝은 붉은 색
        coroutine = StartCoroutine(FadeAway()); // 점점 사라지는 효과 실행
    }

    // 이미지가 점점 투명해지며 사라지는 코루틴
    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f; // 시작 투명도
        float a = startAlpha;

        // 알파값이 0이 될 때까지 반복하며 이미지 점점 투명하게
        while (a > 0.0f)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime; // 일정 비율로 감소
            image.color = new Color(1f, 100f / 255f, 100f / 255f, a); // 점점 연해지는 붉은색
            yield return null;
        }

        image.enabled = false; // 완전히 사라지면 비활성화
    }
}
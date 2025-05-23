using UnityEngine;
using UnityEngine.UI;

public class UIBuffBar : MonoBehaviour
{
    public Image barImage;           // 버프를 표시할 UI 이미지 (Fill 방식 사용)
    private float totalDuration;     // 버프의 전체 지속 시간
    private float timeLeft;          // 남은 버프 시간
    private bool isRunning = false;  // 버프 타이머가 동작 중인지 여부

    public void StartBuff(float duration)
    {
        totalDuration = duration;            // 전체 지속 시간 설정
        timeLeft = duration;                 // 남은 시간 초기화
        isRunning = true;                    // 타이머 시작

        barImage.fillAmount = 1f;            // 바를 꽉 채움
        barImage.gameObject.SetActive(true); // UI 바 활성화
    }

    private void Update()
    {
        if (!isRunning) return;  // 동작 중이 아니면 아무것도 하지 않음

        timeLeft -= Time.deltaTime;                                     // 남은 시간 감소
        barImage.fillAmount = Mathf.Clamp01(timeLeft / totalDuration);  // 바의 채워진 정도 갱신

        // 시간이 다 되면 버프 종료 처리
        if (timeLeft <= 0f)
        {
            isRunning = false;                    // 타이머 종료
            barImage.fillAmount = 0f;             // 바를 비움
            barImage.gameObject.SetActive(false); // UI 바 비활성화
        }
    }
}
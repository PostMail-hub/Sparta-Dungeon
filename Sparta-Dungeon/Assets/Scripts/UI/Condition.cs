using UnityEngine;
using UnityEngine.UI;

// 체력, 마나 등의 상태 수치를 관리하고 UI 바와 연동하는 클래스
public class Condition : MonoBehaviour
{
    public float curValue;        // 현재 수치
    public float maxValue;        // 최대 수치
    public float startValue;      // 게임 시작 시 초기 수치
    public float passiveValue;    // 초당 회복/감소 수치 (추후 확장 가능)
    public Image uiBar;           // UI 바 (Image 컴포넌트의 FillAmount로 표시)

    private void Start()
    {
        curValue = startValue; // 시작 시 수치를 초기값으로 설정
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage(); // 매 프레임마다 UI 바 갱신
    }

    // 수치를 증가시키지만 최대값을 넘지 않도록 제한
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    // 수치를 감소시키지만 0 미만으로 내려가지 않도록 제한
    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    // 현재 수치의 비율을 반환 (0 ~ 1 사이)
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
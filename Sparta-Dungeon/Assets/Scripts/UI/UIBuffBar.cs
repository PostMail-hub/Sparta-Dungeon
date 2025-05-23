using UnityEngine;
using UnityEngine.UI;

public class UIBuffBar : MonoBehaviour
{
    public Image barImage;
    private float totalDuration;
    private float timeLeft;
    private bool isRunning = false;

    public void StartBuff(float duration)
    {
        Debug.Log("BuffBar Started with duration: " + duration);

        totalDuration = duration;
        timeLeft = duration;
        isRunning = true;

        barImage.fillAmount = 1f;
        barImage.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!isRunning) return;

        timeLeft -= Time.deltaTime;
        barImage.fillAmount = Mathf.Clamp01(timeLeft / totalDuration);

        if (timeLeft <= 0f)
        {
            isRunning = false;
            barImage.fillAmount = 0f;
            barImage.gameObject.SetActive(false);
        }
    }
}
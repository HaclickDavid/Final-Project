using UnityEngine;
using TMPro;

public class TimerExample : MonoBehaviour
{
    public float countdownTime = 60.0f;  // 設定初始倒計時時間
    private float timer;                  // 計時器

    public TextMeshProUGUI countdownText;  // 引用Canvas上的TextMeshProUGUI元件

    void Start()
    {
        timer = countdownTime;  // 將計時器設定為初始倒計時時間
    }

    void Update()
    {
        if (timer > 0)
        {
            // 如果計時器尚未達到零，則減少計時器
            timer -= Time.deltaTime;

            // 更新Canvas上的TextMeshProUGUI顯示
            UpdateCountdownText();

            // 在這裡執行倒計時期間的任何操作
            // 例如更新UI、播放聲音等
        }
        else
        {
            // 如果計時器達到零，執行相應的操作
            TimerExpired();
        }
    }

    void UpdateCountdownText()
    {
        // 更新Canvas上的TextMeshProUGUI顯示
        if (countdownText != null)
        {
            countdownText.text = "TIME: " + Mathf.CeilToInt(timer).ToString(); // 使用Mathf.CeilToInt取整數
        }
    }

    void TimerExpired()
    {
        // 在倒計時過期時執行的操作
        Debug.Log("倒計時結束!");
    }
}

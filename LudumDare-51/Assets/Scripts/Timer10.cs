using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer10 : MonoBehaviour
{
    private float time = 10f;
    public static Action OnTimerHit0;
    public static bool paused;

    private TextMeshPro timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshPro>();
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            time -= Time.deltaTime;
            timerText.SetText(time.ToString("0.0"));

            if (time <= 0)
            {
                time = 10f;
                OnTimerHit0?.Invoke();
            }
        }
    }
}

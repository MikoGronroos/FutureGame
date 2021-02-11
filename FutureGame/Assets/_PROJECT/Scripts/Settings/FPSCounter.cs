using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class FPSCounter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI fpsText;

    DateTime _lastTime; // marks the beginning the measurement began
    int _framesRendered; // an increasing count
    int _fps; // the FPS calculated from the last measurement

    void Update()
    {
        _framesRendered++;

        if ((DateTime.Now - _lastTime).TotalSeconds >= 1)
        {
            // one second has elapsed 

            _fps = _framesRendered;
            _framesRendered = 0;
            _lastTime = DateTime.Now;
        }

        // draw FPS on screen here using current value of _fps 
        fpsText.text = $"FPS: {_fps}";
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsData : MonoBehaviour
{
    private float _framePerSecond;
    public float FramePerSecond
    {
        get => _framePerSecond;
        set
        {
            _framePerSecond = value;
            OnFrameChanged?.Invoke(_framePerSecond);
        }
    }

    public event Action<float> OnFrameChanged;
}

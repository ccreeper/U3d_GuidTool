using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mask : MonoBehaviour {

    public float radius;

    private float maxRadius;
    private float current;
    private float velocity = 0;
    private Material material;
    private void Awake()
    {
        maxRadius = radius +1;
        radius=Mathf.Floor(radius);
        current = maxRadius;
        material = GetComponent<RawImage>().material;
    }

    private void Update()
    {
        float value = Mathf.SmoothDamp(current, radius,ref velocity, 0.3f);
        if (!Mathf.Approximately(current, value))
        {
            current = value;
            material.SetFloat("_Slider", current);
        }
    }
}

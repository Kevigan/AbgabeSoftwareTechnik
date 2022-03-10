using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Pulse : MonoBehaviour
{
    private Volume volume;
    private Bloom bloom;
    private float _timerBoom;
    private bool expand = false;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        //Sbloom = GetComponent<Bloom>();
    }

    // Update is called once per frame
    void Update()
    {
        _timerBoom += Time.deltaTime;
        if (_timerBoom >= 1)
        {
            _timerBoom = 0;
            expand = !expand;
        }
        //if (expand)
        //    bloom.threshold.value += Time.deltaTime * 2;
        //else bloom.threshold.value -= Time.deltaTime * 2;
        if (expand)
        {
            if (volume.profile.TryGet<Bloom>(out var bloom2))
            {
                bloom2.threshold.value += Time.deltaTime * 2;
            }
        }
        else
        {
            if (volume.profile.TryGet<Bloom>(out var bloom2))
            {
                bloom2.threshold.value -= Time.deltaTime * 2;
            }
        }
    }
}

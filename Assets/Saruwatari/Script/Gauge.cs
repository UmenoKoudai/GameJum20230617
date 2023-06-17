using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    //[SerializeField] 

    [SerializeField,Header("Player1のゲージ")] Slider Player1Gauge;
    [SerializeField,Header("Player2のゲージ")] Slider Player2Gauge;

    [Header("Player1のゲージの現在の値")] public float GeugeMeter1 = 0;
    [Header("Player2のゲージの現在の値")] public float GeugeMeter2 = 0;
    [Header("Player1のゲージの最大の値")] public float MaxGeugeMeter1 = 100;
    [Header("Player2のゲージの最大の値")] public float MaxGeugeMeter2 = 100;

    public int GaugeSpeed;

    public bool Meter1;
    public bool Meter2;

    // Start is called before the first frame update
    void Start()
    {
        Meter1 = false;
        Meter2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        Meter();
        MeterStop();
    }

    public void Meter()
    {
        if (Meter1 == false)
        {
            GeugeMeter1 += Time.deltaTime * GaugeSpeed;
            Player1Gauge.value = GeugeMeter1 / (float)MaxGeugeMeter1;
            
            if (GeugeMeter1 >= 100)
            {
                GeugeMeter1 = 0;
                Player1Gauge.value = GeugeMeter1 / (float)MaxGeugeMeter1;
            }
        }

        if (Meter2 == false)
        {
            GeugeMeter2 += Time.deltaTime * GaugeSpeed;
            Player2Gauge.value = GeugeMeter2 / (float)MaxGeugeMeter2;

            if (GeugeMeter2 >= 100)
            {
                GeugeMeter2 = 0;
                Player2Gauge.value = GeugeMeter2 / (float)MaxGeugeMeter2;
            }
        }
    }

    public void MeterStop()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Meter1 = true;
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Meter2 = true;
        }
    }
}

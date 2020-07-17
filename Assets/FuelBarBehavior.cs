using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarBehavior : MonoBehaviour
{
    public Slider slider;

    public void SetFuel(float fuel)
    {
        slider.value = fuel;
    }

    public void SetMaxFuel(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

}

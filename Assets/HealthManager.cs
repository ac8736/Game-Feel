using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Image shield;

    private void Start()
    {
        //fill.color = Color.red;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    { 
        slider.value = health;
    }

    public void SetInvincible(bool set)
    {
        if (set)
        {
            fill.color = Color.blue;
        }
        else
        {
            fill.color = Color.red;
        }
    }

    public void SetShieldUI(bool set)
    { 
        shield.enabled = set;
    }
}

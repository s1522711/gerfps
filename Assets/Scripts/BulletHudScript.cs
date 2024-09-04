using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletHudScript : MonoBehaviour
{
    [SerializeField] Text bulletText;

    int bullets = 0;

    void Awake()
    {
        UpdateHUD();
    }

    public int Bullets
    {
        get { return bullets; }
        set
        {
            bullets = value;
            UpdateHUD();
        }
    }

    private void UpdateHUD()
    {
        bulletText.text = "Bullets: " + bullets.ToString();
        if (bullets <= 10 && bullets > 0)
        {
            bulletText.color = Color.yellow;
        }
        else if (bullets == 0)
        {
            bulletText.color = Color.red;
        }
        else
        {
            bulletText.color = Color.white;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthProcesing : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] Image fillImage;
     public int healthMax;
    public void Reload()
    {
        if (healthMax == 0)
        {
            healthMax = health;
        }
        health = healthMax;
        fillImage.fillAmount = health * 1f / healthMax;
    }
    public int Dameage(int dameage)
    {
        health -= dameage;
        if (health <= 0)
        {
            health = 0;
        }
        fillImage.fillAmount = health * 1f / healthMax;
        return health;
    }

    public int Die()
    {
        health -= health;
        if (health <= 0)
        {
            health = 0;
        }
        fillImage.fillAmount = health * 1f / healthMax;
        return health;
    }
}

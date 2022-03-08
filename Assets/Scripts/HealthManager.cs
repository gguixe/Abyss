using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public Image[] health;
    public Sprite fullContainer;
    public Sprite halfContainer;
    public Sprite emptyCointainer;
    public FloatValue totalContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHealth();
    }

    public void InitHealth()
    {
        for(int i = 0; i < totalContainers.initialValue; i++)
        {
            health[i].gameObject.SetActive(true);
            health[i].sprite = fullContainer;
        }
    }

    public void UpdateHealth()
    {
        float tempHealth = playerCurrentHealth.RunTimeValue / 2;

        for (int i = 0; i < totalContainers.initialValue; i++)
        {
            if(i <= tempHealth-1)
            {
                //Full Heart
                health[i].sprite = fullContainer;
            }else if (i >= tempHealth)
            {
                //Empty Heart
                health[i].sprite = emptyCointainer;
            }
            else
            {
                //Half heart
                health[i].sprite = halfContainer;
            }
        }
    }
}

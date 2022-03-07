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
}

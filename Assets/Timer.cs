using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float minutes;
    public float secondes;
    float tempsRestant;
    // Start is called before the first frame update
    void Start()
    {
        tempsRestant = (minutes * 60) + secondes;
    }

    // Update is called once per frame
    void Update()
    {
        if (tempsRestant > 0)
        {
            tempsRestant -= Time.deltaTime;
            minutes = Mathf.FloorToInt(tempsRestant / 60);
            secondes = Mathf.FloorToInt(tempsRestant % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, secondes);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountPoints : MonoBehaviour
{
    int points;
    public Text textePoints;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        points += 5;
        textePoints.text = points.ToString();
    }

}

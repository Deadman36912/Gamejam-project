using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float minutes;
    public float secondes;
    float tempsRestant;
    
    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    RigidbodyFirstPersonController controller;

    [SerializeField]
    LancerObjet lanceur;

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
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            controller.mouseLook.SetCursorLock(false);
        }
        if (Input.GetKeyDown("escape"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PauseGame();
        }
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        controller.mouseLook.SetCursorLock(false);
        lanceur.canThrow = false;
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame");
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        controller.mouseLook.SetCursorLock(true);
        lanceur.canThrow = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

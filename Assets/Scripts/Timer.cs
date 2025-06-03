using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{ 
    float currentTime = 0f;
    float startingTime = 60f;

    [SerializeField] TMP_Text countdownText;
    
    // End menu
    public GameObject winCanvas;

    private bool isRunning = true;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0); // Ensure it doesn’t go below 0
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            // Show win canvas
            if (winCanvas != null)
            {
                winCanvas.SetActive(true);
            }

            // End game logic
            Weapon weapon = FindObjectOfType<Weapon>();
            if (weapon != null)
            {
                weapon.SetGameEnded(true);
            }

            isRunning = false;
        }
    }


    public void StopTimer()
    {
        isRunning = false;
    }
}

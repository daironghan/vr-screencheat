using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public List<Button> buttons;
    private int currentIndex = 0;

    void Start()
    {   
        if (buttons != null && buttons.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
        }
    }

    void Update()
    {
        if (buttons == null || buttons.Count == 0) return;

        // Navigate up
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentIndex = (currentIndex - 1 + buttons.Count) % buttons.Count;
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
        }

        // Navigate down
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentIndex = (currentIndex + 1) % buttons.Count;
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
        }

        // Select current
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }
}

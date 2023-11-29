using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_UI_Actions : MonoBehaviour
{
    [SerializeField] BoolVariable cameraLock;
    private void Awake()
    {
        cameraLock.RuntimeValue = false;
    }
    public void ToggleCameraLock()
    {
        if (!cameraLock.RuntimeValue)
        {
            cameraLock.RuntimeValue = true;
        }
        else
        {
            cameraLock.RuntimeValue = false;
        }
    }

    public void ToggleSetActive(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }

    public void ToggleGameTime()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void ToggleCursorMode()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void SetGameTime(float t)
    {

        Time.timeScale = t;

    }

    public void SetCursorMode(CursorLockMode c)
    {
        Cursor.lockState = c;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit!");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinaleManager : MonoBehaviour
{
    [SerializeField] int consoles;
    [SerializeField] UnityEvent endedFinaleActions;
    [SerializeField] UnityEvent lockDownEndedActions;
    public void ConsoleCleared()
    {
        consoles -= 1;
        if(consoles <= 0)
        {
            //Time.timeScale = 0;
            if(endedFinaleActions != null)
            {
                endedFinaleActions.Invoke();
            }
        }
        else
        {
            StartCoroutine(ConsoleLockDown());
        }
    }

    IEnumerator ConsoleLockDown()
    {
        yield return new WaitForSeconds(5);
        if (lockDownEndedActions != null)
        {
            lockDownEndedActions.Invoke();
        }
    }

}

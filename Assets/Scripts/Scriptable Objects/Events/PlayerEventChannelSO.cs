using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Player Event Channel")]
public class PlayerEventChannelSO : ScriptableObject
{
    #region Move Input Event
    public UnityAction<Vector3> OnEventMove_Input;

    public void MoveEvent_Input(Vector3 moveDir)
    {
        if (OnEventMove_Input != null)
        {
            //Debug.Log("Move Input Event Invoked");
            OnEventMove_Input.Invoke(moveDir);
        }
    }
    #endregion

    #region Jump Input Event
    public UnityAction OnEventJump_Input;

    public void JumpEvent_Input()
    {
        //Debug.Log("Jump Input Event Invoked");
        OnEventJump_Input.Invoke();
    }

    #endregion

    #region Camera Input Event
    public UnityAction<float, float> OnEventCamera_Input;

    public void CameraEvent_Input(float mouseX, float mouseY)
    {
        //Debug.Log("Camera Input Event Invoked");
        //OnEventCamera_Input.Invoke(mouseX, mouseY);
    }

    #endregion

    #region Raised Death Event
    public UnityAction OnEventRaised_Death;

    public void RaiseEvent_Death()
    {
        if (OnEventRaised_Death != null)
        {
            Debug.Log("Death Event Invoked");
            OnEventRaised_Death.Invoke();
        }
    }
    #endregion
}

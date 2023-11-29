using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameObjectEventListener : MonoBehaviour
{
	[SerializeField] private GameObjectEventChannelSO _channel = default;
	[SerializeField] GameObject objectToCompare;
	public UnityEvent OnEventRaised;

	private void OnEnable()
	{
		if (_channel != null)
			_channel.OnEventRaised += Respond;
	}

	private void OnDisable()
	{
		if (_channel != null)
			_channel.OnEventRaised -= Respond;
	}

	private void Respond(GameObject thisObject)
	{
		if(thisObject == objectToCompare)
        {
			if (OnEventRaised != null)
				OnEventRaised.Invoke();
		}
	}
}

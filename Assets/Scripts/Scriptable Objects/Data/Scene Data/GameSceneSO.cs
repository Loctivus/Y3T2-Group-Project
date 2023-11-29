using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Scene/Scene Data")]
public class GameSceneSO : ScriptableObject
{
	[Header("Information")]
	public string sceneName;
	public string shortDescription;

	[Header("Sounds")]
	public AudioClip music;

	[Header("Settings")]
	public CursorLockMode sceneDefaultCursor;
	public float sceneDefaultTimeScale;
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorUI : MonoBehaviour
{
#if UNITY_EDITOR
	public GameSceneSO pauseScene;

	private void Awake()
	{
		Debug.Log("Check if Active UI Scene");
		for (int i = 0; i < SceneManager.sceneCount; ++i)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.name == pauseScene.sceneName || scene.name == "Main Menu")
			{
				return;
			}
		}
		SceneManager.LoadSceneAsync(pauseScene.sceneName, LoadSceneMode.Additive);
	}
#endif
}

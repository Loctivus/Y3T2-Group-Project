using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{

	[Header("Initialization Scene")]
	[SerializeField] private GameSceneSO _initializationScene;
	[Header("Load on start")]
	[SerializeField] private GameSceneSO[] _mainMenuScenes;
	[Header("Load Event")]
	//The load event we are listening to
	[SerializeField] private LoadEventChannelSO _loadEventChannel;
	[SerializeField] private VoidEventChannelSO _voidEventChannel;
	[SerializeField] VoidEventChannelSO sceneStartedChannel;
	//List of the scenes to load and track progress
	private List<AsyncOperation> _scenesToLoadAsyncOperations = new List<AsyncOperation>();
	//List of scenes to unload
	private List<Scene> _ScenesToUnload = new List<Scene>();
	//Keep track of the scene we want to set as active (for lighting/skybox)
	private GameSceneSO _activeScene;
	// Start is called before the first frame update

	private void OnEnable()
	{
		_loadEventChannel.OnLoadingRequested += LoadScenes;
	}

	private void OnDisable()
	{
		_loadEventChannel.OnLoadingRequested -= LoadScenes;
	}

	private void Start()
	{
		Debug.Log("Game Started");
		if (SceneManager.GetActiveScene().name == _initializationScene.sceneName)
		{
			Debug.Log("Started In init Scene, Adding Menu Scene");
			LoadMainMenu();
		}
	}
	private void LoadMainMenu()
	{
		LoadScenes(_mainMenuScenes);
	}

	private void LoadScenes(GameSceneSO[] locationsToLoad)
	{
		StopCoroutine(LoadSceneDelay());
		if (locationsToLoad[0].sceneName == "Active Scene")
        {
			locationsToLoad[0] = _activeScene;
        }
			Time.timeScale = locationsToLoad[0].sceneDefaultTimeScale;
			Cursor.lockState = locationsToLoad[0].sceneDefaultCursor;


		

		//Add all current open scenes to unload list
		AddScenesToUnload();

		_activeScene = locationsToLoad[0];

		if (locationsToLoad[0].sceneName == SceneManager.GetActiveScene().name)
        {
			//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			//return;
		}

		for (int i = 0; i < locationsToLoad.Length; ++i)
		{
			String currentSceneName = locationsToLoad[i].sceneName;
			

			//if (!CheckLoadState(currentSceneName))
			//{
				//Add the scene to the list of scenes to load asynchronously in the background
				_scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive));
			//}
			Debug.Log(_scenesToLoadAsyncOperations.Count);
		}

		
		StartCoroutine(LoadSceneDelay());

	}

	private void SetActiveScene(AsyncOperation asyncOp)
	{
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(_activeScene.sceneName));
	}

	private void AddScenesToUnload()
	{
		for (int i = 0; i < SceneManager.sceneCount; ++i)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.name != _initializationScene.sceneName)
			{
				Debug.Log("Added scene to unload = " + scene.name);
				//Add the scene to the list of the scenes to unload
				_ScenesToUnload.Add(scene);
			}
		}
	}

	private void UnloadScenes()
	{
		if (_ScenesToUnload != null)
		{
			for (int i = 0; i < _ScenesToUnload.Count; ++i)
			{
				//Unload the scene asynchronously in the background
				SceneManager.UnloadSceneAsync(_ScenesToUnload[i]);
			}
		}
		_ScenesToUnload.Clear();
	}

	private bool CheckLoadState(String sceneName)
	{
		for (int i = 0; i < SceneManager.sceneCount; ++i)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.name == sceneName)
			{
				return true;
			}
		}
		return false;
	}

	IEnumerator CallSceneSetUp()
    {
		yield return new WaitForSeconds(0.05f);
		sceneStartedChannel.RaiseEvent();
	}

	IEnumerator LoadSceneDelay()
	{
		UnloadScenes();
		yield return new WaitForSeconds(1f);
		if(_scenesToLoadAsyncOperations.Count>0)
			_scenesToLoadAsyncOperations[0].completed += SetActiveScene;

		//Clear the scenes to load
		_scenesToLoadAsyncOperations.Clear();

		//Unload the scenes
		
		StartCoroutine(CallSceneSetUp());
	}

	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEnforcer : MonoBehaviour
{
	[SerializeField] string sceneName;
	[SerializeField] LoadSceneMode mode;

	private void Awake()
	{
		if (!isSceneLoaded(sceneName))
		{
			SceneManager.LoadScene(sceneName, mode);
		}
	}

	public static bool isSceneLoaded(string sceneName)
	{
		// check loaded scenes for matching scene name
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			var scene = SceneManager.GetSceneAt(i);
			if (scene.name == sceneName)
			{
				return true;
			}
		}

		return false;
	}
}

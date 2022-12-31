using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ModelChoose : MonoBehaviour {

	public GameObject endUI;
	public Text endMessage;

	public void OnStageMode()
	{
		SceneManager.LoadScene(3);
	}
	public void OnEndlessMode()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
	public void setting()
	{
		SceneManager.LoadScene(10);

	}

}

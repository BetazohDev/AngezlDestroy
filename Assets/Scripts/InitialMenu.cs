using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InitialMenu : MonoBehaviour
{
	public void Play(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Options(){
		SceneManager.LoadScene("OptionsMenu");
	}

	public void Exit(){
		SceneManager.LoadScene("Credits");
	}
}

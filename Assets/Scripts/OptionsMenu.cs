using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void PantallaCompleta(bool pantallaCompleta)
    {
       Screen.fullScreen = pantallaCompleta;
    }

    public void ChangeVolume(float volume){
        audioMixer.SetFloat("Volume", volume); 
    }

    public void ChangeQuality(int index){
        QualitySettings.SetQualityLevel(index);
    }

    public void Back(){
        SceneManager.LoadScene("InitialMenu");
    }
}

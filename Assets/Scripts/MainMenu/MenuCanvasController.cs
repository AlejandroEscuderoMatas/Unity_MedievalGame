using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCanvasController : MonoBehaviour
{
    public GameObject      m_mainMenu;
    public GameObject      m_settings;
    private WaitForSeconds timeToGame= new WaitForSeconds(5.0f);

    private int            m_personalBest = 0;
    public TextMeshProUGUI m_personalBestText;
    public Slider          m_musicSlider;
    public Slider          m_sfxSlider;

    private void Start()
    {
        if (FileExists())
        {
            LoadBin();
        }else
            SaveBin();

        m_personalBestText.text = "Your Best: " + m_personalBest;
    }

    public void Play()
    {
        //CARGAR EL TUTORIAL
        if (Blackboard.tutorialDone == false)
        {
            //SceneManager.LoadScene(AppScenes.TUTORIAL_SCENE);
        }
        
        //GUARDAR QUE YA HEMOS INICIADO PARTIDA UNA VEZ PARA NO MOSTRAR MAS EL TUTORIAL
        Blackboard.tutorialDone = true;
        
        MusicManager.Instance.PlayBackgroundMusic(AppSounds.GAME_MUSIC);
        
        //CARGAR PANTALLA DE CARGA
        SceneManager.LoadScene(AppScenes.LOADING_SCENE, LoadSceneMode.Single);
    }

    public void OnMusicValueChanged()
    {
        MusicManager.Instance.MusicVolume = m_musicSlider.value;
        MusicManager.Instance.PlaySfxMusic(AppSounds.BUTTON_SFX);
    }
    
    public void OnSfxValueChanged()
    {
        MusicManager.Instance.SfxVolume = m_sfxSlider.value;
        MusicManager.Instance.PlaySfxMusic(AppSounds.BUTTON_SFX);
    }

    public void Settings(bool isSettings)
    {
        m_mainMenu.SetActive(!isSettings);
        m_settings.SetActive(isSettings);

        if (!isSettings)
        {
            MusicManager.Instance.MusicVolumeSave = m_musicSlider.value;
            MusicManager.Instance.SfxVolume = m_sfxSlider.value;
        }
        
        MusicManager.Instance.PlaySfxMusic(AppSounds.BUTTON_SFX);
    }

    public void Exit()
    {
        MusicManager.Instance.PlaySfxMusic(AppSounds.BUTTON_SFX);
        Application.Quit();
    }
    
    public bool FileExists()
    {
        if(System.IO.Path.GetExtension(Application.persistentDataPath + "/GameData.aem").ToLower() == ".aem")
            if (System.IO.File.Exists(Application.persistentDataPath + "/GameData.aem"))
            {
                return true;
            }else
            {
                return false;
            }

        return false;
    }
    
    public void SaveBin()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream      file = File.Create(Application.persistentDataPath + "/GameData.aem");

        DataSaving dataAux = new DataSaving();
        dataAux.personalBest = m_personalBest;
        bf.Serialize(file, dataAux);
        file.Close();
    }

    public void LoadBin()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream      file = File.Open(Application.persistentDataPath + "/GameData.aem", FileMode.Open);

        //COMPROBAR SI EXISTE FICHERO
        DataSaving dataAux = (DataSaving)bf.Deserialize(file);
        m_personalBest = dataAux.personalBest;
        file.Close();
    }
}
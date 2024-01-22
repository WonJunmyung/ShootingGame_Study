using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuBack;
    public GameObject Story;
    public GameObject Setting;

    public GameObject CheckMusic;
    public GameObject CheckSound;


    public int isMusic = 0;
    public int isSound = 0;

    // Start is called before the first frame update
    void Start()
    {
        CheckMusic.SetActive(true);
        CheckSound.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnStart()
    {
        SceneManager.LoadScene("Game");

    }

    public void BtnStory()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenStory", 1.5f);
    }

    public void BtnSetting()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenSetting", 1.5f);
    }

    void OpenStory()
    {
        Story.SetActive(true);
        Story.GetComponent<Animator>().SetTrigger("Open");
    }

    void OpenSetting()
    {
        Setting.SetActive(true);
        Setting.GetComponent<Animator>().SetTrigger("Open");
    }

    void OpenMenuBack()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Open");
    }

    public void BtnMusic()
    {
        CheckMusic.SetActive(!CheckMusic.activeInHierarchy);
    }

    public void BtnSound()
    {
        CheckSound.SetActive(!CheckSound.activeInHierarchy);
    }
    


    public void BtnBack(int num)
    {
        switch (num)
        {
            case 0: // MENUAL
                Story.GetComponent<Animator>().SetTrigger("Close");
                Invoke("OpenMenuBack", 1.5f);
                break;
            case 1: // STORY
                Setting.GetComponent<Animator>().SetTrigger("Close");
                Invoke("OpenMenuBack", 1.5f);
                break;
        }
    }
}

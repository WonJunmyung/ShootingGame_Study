using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public enum MenuStatus
    {
        None,
        Pause,
        Clear,
        GameOver,
        Continue,
    }

    public MenuStatus menuStatus = MenuStatus.None;

    TextMeshProUGUI title;


    // Start is called before the first frame update
    void Start()
    {
        this.transform.gameObject.SetActive(false);
        title = this.transform.Find("BG").Find("Title").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMenu(MenuStatus _menuStatus)
    {
        menuStatus = _menuStatus;
        this.gameObject.SetActive(true);

        switch (_menuStatus)
        {
            case MenuStatus.None:
                title.text = "None";
                break;
            case MenuStatus.Clear:
                title.text = "CLEAR!";
                break;
            case MenuStatus.Pause:
                title.text = "PAUSE";
                break;
            case MenuStatus.GameOver:
                title.text = "GAME OVER!";
                break;
            case MenuStatus.Continue:
                title.text = "None";
                this.gameObject.SetActive(false);
                break;
        }
    }

    
    


    public void BtnClose()
    {
        menuStatus = MenuStatus.None;
        Manager.Instance.ContinueGame();
    }

    public void BtnRestart()
    {
        menuStatus = MenuStatus.None;
        Manager.Instance.RestartGame();
    }

    public void BtnHome()
    {
        Manager.Instance.MainMenu();
    }
    
}

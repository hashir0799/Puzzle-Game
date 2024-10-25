using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUiManager : MonoBehaviour
{
    [Header("Texts")]
    public TMP_Text TurnsText;
    public TMP_Text MatchText;
    int turnscount;
    int matchcount;
    [Space(10)]
    [Header("Screens")]
    public List<GameObject> Screens;
    public GameObject ScreensBg;
    [Space(10)]
    [Header("Screen Pause Buttons")]
    public Button PauseResume;
    public Button PauseRestart;
    public Button PauseMainMenu;
    public Button Pause;
    [Space(10)]
    [Header("Screen Completed Buttons")]
    public Button CompletedMainMenu;
    public Button CompletedRestart;
  

    private void OnEnable()
    {
        GameManager.MatchFound += MatchFound;
        GameManager.ImageSelected += ClikedOnImage;
        GameManager.GameCompleted += GameCompleted;
    }
    void Start() 
    {
        PauseResume.onClick.AddListener(()=>
        {
            OnResume();
        });
        PauseRestart.onClick.AddListener(()=> 
        {
            OnRestart();
        });
        PauseMainMenu.onClick.AddListener(()=> 
        {
            OnMainMenu();
        });
        Pause.onClick.AddListener(()=> 
        {
            ActivateScreen(1);
        });
        CompletedMainMenu.onClick.AddListener(()=>
        {
            OnMainMenu();
        });
        CompletedRestart.onClick.AddListener(()=>
        {
            OnRestart();
        });
    }
    public void OnDisable()
    {
        GameManager.MatchFound -= MatchFound;
        GameManager.ImageSelected -= ClikedOnImage;
        GameManager.GameCompleted -= GameCompleted;
    }

    private void GameCompleted()
    {
        ActivateScreen(0);
    }

    private void ClikedOnImage(ButtonHandler obj)
    {
        turnscount++;
        TurnsText.text = turnscount.ToString();
    }

    private void MatchFound(ButtonHandler arg1, ButtonHandler arg2)
    {
        matchcount++;
        MatchText.text = matchcount.ToString();
    }

    public void ActivateScreen(int screenNumber) 
    {
        ScreensBg.SetActive(true);
        for (int i =0;i< Screens.Count;i++) 
        {
            Screens[i].SetActive(false);
        }
        Screens[screenNumber].SetActive(true);
    }
    public void OnRestart() 
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void OnResume() 
    {
        ScreensBg.SetActive(false);
    }
    public void OnMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }


}

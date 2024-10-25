
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuUiManager : MonoBehaviour
{
    public Button Play;
    public TMP_Dropdown DropDown;
    void Start()
    {
        GameManager._Instance.CurrentMode = DifficultyModes.easy;
        Play.onClick.AddListener(()=> 
        {
            SceneManager.LoadScene("Gameplay");
        });
    }
    public void SetDifficulity() 
    {
        switch (DropDown.value)
        {
            case 0:
                GameManager._Instance.CurrentMode = DifficultyModes.easy;
                break;
            case 1:
                GameManager._Instance.CurrentMode = DifficultyModes.medium;
                break;
            case 2:
                GameManager._Instance.CurrentMode = DifficultyModes.hard;
                break;
        }
    }

 
 
}

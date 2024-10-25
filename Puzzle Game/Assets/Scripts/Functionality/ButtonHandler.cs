using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Image _Image;
    public Button _button;
    public Animator Anim;
    public string _SpriteName;

    public void Start()
    {
        GameManager.MatchFound += OnMatchFound;
        GameManager.MatchNotFound += OnMatchNotFound;
    }

    private void OnMatchNotFound(ButtonHandler arg1, ButtonHandler arg2)
    {

        if (arg1 == this || arg2 == this)
        {
            Anim.SetTrigger(GameManager.ScaleDownAnim);
        }
    }

    private void OnMatchFound(ButtonHandler arg1, ButtonHandler arg2)
    {
        if (arg1 == this || arg2 == this)
        {
            _button.interactable = false;
        }
    }

    private void OnDisable()
    {
        GameManager.MatchFound -= OnMatchFound;
        GameManager.MatchNotFound -= OnMatchNotFound;
    }
    public void AssignValues(Sprite s,string Name) 
    {
        _Image.sprite = s;
        _SpriteName = Name;
        _button.onClick.AddListener(() => 
        {
            Anim.SetTrigger(GameManager.ScaleUpAnim);
            GameManager.ImageSelected?.Invoke(this);
        });
    }
}


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SpriteSpwanManager 
{
    public Sprite _sprite;
    public string _SpriteName;
    public bool Spwan;
    public bool Matched;
    public ButtonHandler ButtonRefernce;
}
public class GameplayManager : MonoBehaviour
{
    [Header("Debug Mode")]
    public bool DebugMode;
    public DifficultyModes TestMode;
    [Space(10)]
    [Header("Lists ")]
    public List<Difficulity> ModesScriptables;
    List<SpriteSpwanManager> _SpriteManager = new();
    [Space(10)]
    [Header("UI Elements ")]
    public RectTransform Container;
    public GridLayoutGroup _gridLayoutGroup;
    [Space(10)]
    [Header("Prefabs")]
    public GameObject GameButtonPrefab;
    ButtonHandler SelectedImage;
  
    private void Start()
    {
        if (DebugMode)
        {
            Intialize(TestMode);
        }
        else 
        {
            Intialize(GameManager._Instance.CurrentMode);
        }
    }
    public void OnEnable()
    {
       GameManager.ImageSelected += OnSelectedImage;
    }
    public void OnDisable()
    {
       GameManager.ImageSelected -= OnSelectedImage;
    }
    void OnSelectedImage(ButtonHandler handler) 
    {

        if (SelectedImage)
        {
            if (string.Compare(SelectedImage._SpriteName, handler._SpriteName) == 0)
            {
                
                GameManager.MatchFound?.Invoke(SelectedImage, handler);
                FillSlot(SelectedImage, handler);
                SelectedImage = null;
            }
            else
            {
                
                GameManager.MatchNotFound?.Invoke(SelectedImage, handler);
                SelectedImage = null;
            }
        }
        else 
        {
            SelectedImage = handler;
        }
    }
    public void Intialize(DifficultyModes _mode) 
    {
        for (int i =0;i< ModesScriptables.Count;i++) 
        {
            if (ModesScriptables[i].Mode == _mode) 
            {
                //Debug.Log($"mode {_mode}");
                ModesScriptables[i].SetGridValues(_gridLayoutGroup, Container);
                List<Sprite> s = ModesScriptables[i].SpriteToSpwanFormTheList();
                foreach (Sprite _s in s)
                {
                    SpriteSpwanManager m = new SpriteSpwanManager();
                    SpriteSpwanManager m1 = new SpriteSpwanManager();
                    m._sprite = _s;   m1._sprite = _s;
                    m._SpriteName = _s.name; m1._SpriteName = _s.name;
                    _SpriteManager.Add(m); _SpriteManager.Add(m1);
                }
                SpwanGameButton();
            }
        }
    }
  
    public void SpwanGameButton() 
    {
        for (int i=0; i< _SpriteManager.Count;i++)
        {
            GameObject Obj = Instantiate(GameButtonPrefab, Container);
            ButtonHandler Ref =  Obj.GetComponent<ButtonHandler>();
            int Random = FindRandomSpriteToSpwan();
            Ref.AssignValues(_SpriteManager[Random]._sprite, _SpriteManager[Random]._SpriteName);
            _SpriteManager[Random].ButtonRefernce = Ref;
            _SpriteManager[Random].Spwan = true;


        }
    }

    public int FindRandomSpriteToSpwan() 
    {
        int no = Random.Range(0, _SpriteManager.Count);
        if (_SpriteManager[no].Spwan)
        {
            return FindRandomSpriteToSpwan();
        }
        else 
        {
            return no;
        }
    }

    public void FillSlot(ButtonHandler a,ButtonHandler b) 
    {
        foreach (SpriteSpwanManager manager in _SpriteManager) 
        {
            if (manager.ButtonRefernce == a || manager.ButtonRefernce == b)
            {
                manager.Matched = true;
            }
        }
        GameCompleteCheck();
    }
    public void GameCompleteCheck()
    {
        foreach (SpriteSpwanManager manager in _SpriteManager)
        {
            if (!manager.Matched)
                return;
        }
        GameManager.GameCompleted?.Invoke();
       
    }
}

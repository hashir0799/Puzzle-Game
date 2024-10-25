using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DifficultyModes 
{
    easy,medium,hard
}

[CreateAssetMenu(fileName = "Difficulity Settings", menuName = "ScriptableObjects/Difficulity Settings", order = 1)]
public class Difficulity : ScriptableObject
{
    #region Settings
    [Header("Complexity")]
    public DifficultyModes Mode;
    [Space(10)]
    [Header("GamePlay Images")]
    public List<Sprite> GameplaySprites = new();
    public int SpwanCount;
    [Space(5)]
    [Header("sprite Setting")]
    public bool CanRepeat;
    public int RepeatCount;
    [Space(10)]
    [Header("Grid Settings")]
    public Vector2 Spacing;
    public GridLayoutGroup.Constraint constraint = GridLayoutGroup.Constraint.Flexible;
    public GridLayoutGroup.Axis Axis = GridLayoutGroup.Axis.Vertical;
    public GridLayoutGroup.Corner StartCorner = GridLayoutGroup.Corner.UpperLeft;
    public int PaddingLeft = 0;
    public int PaddingRight = 0;
    public int PaddingTop = 0;
    public int PaddingBottom = 0;

    [SerializeField]
    List<Sprite> SpwanSprites;

    #endregion
   
    public List<Sprite> SpriteToSpwanFormTheList() 
    {
        SpwanSprites.Clear();
        int spriteCountToSpwan = SpwanCount / 2;
        if (CanRepeat)
        {
         
            for (int i=0;i< spriteCountToSpwan;i++)
            {

                CheckForValidSpriteAndAddSpriteInList(SpwanSprites);
            }
         
          

        }
        else 
        {
            for (int i = 0; i < spriteCountToSpwan; i++)
            {

                Sprite s = CheckForValidSpriteAndAddSpriteInListWhichNotRepeat(SpwanSprites);
                if(s!=null)
                    SpwanSprites.Add(s);

            }

        }
        return SpwanSprites;
    }
    public void SetGridValues(GridLayoutGroup group, RectTransform container)
    {
        ApplyGridLayoutCellSize(group, container);
        group.constraint = constraint;
        group.startAxis = Axis;
        group.startCorner = StartCorner;
        group.padding.left = PaddingLeft;
        group.padding.right = PaddingRight;
        group.padding.top = PaddingTop;
        group.padding.bottom = PaddingBottom;
    }
    public void CheckForValidSpriteAndAddSpriteInList(List<Sprite> s)
    {
        int count = 0;
        Sprite sprite = GetSprite();
        string name = sprite.name;
        foreach (Sprite _sprite in s) 
        {
            if (string.Compare(_sprite.name, name) == 0) 
            {
                count++;
            }
        }
        if (count >= RepeatCount)
        {
            CheckForValidSpriteAndAddSpriteInList(s);
        }
        else 
        {
            s.Add(sprite);
        }
       
    }
    public Sprite CheckForValidSpriteAndAddSpriteInListWhichNotRepeat(List<Sprite> s)
    {
        int count = 0;
        Sprite sprite = GetSprite();
        string name = sprite.name;
        if (s.Count > 0)
        {
            foreach (Sprite _sprite in s)
            {
                if (string.Compare(_sprite.name, name) == 0)
                {
                 
                    return CheckForValidSpriteAndAddSpriteInListWhichNotRepeat(s);
                  
                }
                else
                {
                    return sprite;
                }
            }
        }
        else 
        {
            return sprite;
        }
        return null;

    }
    public Sprite GetSprite()
    {
        int random = Random.Range(0, GameplaySprites.Count);
        return GameplaySprites[random];
    }


    void ApplyGridLayoutCellSize(GridLayoutGroup gridLayoutGroup, RectTransform container)
    {
        float containerWidth = container.rect.width;
        float containerHeight = container.rect.height;
        int columns = Mathf.CeilToInt(Mathf.Sqrt(SpwanCount));
        int rows = Mathf.CeilToInt((float)SpwanCount / columns);
        float cellWidth = (containerWidth - (columns - 1) * Spacing.x) / columns;
        float cellHeight = (containerHeight - (rows - 1) * Spacing.y) / rows;
        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
        gridLayoutGroup.spacing = new Vector2(Spacing.x, Spacing.y);

      //  Debug.Log("Calculated Cell Size: " + gridLayoutGroup.cellSize);
    }
}

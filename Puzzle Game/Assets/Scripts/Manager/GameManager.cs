

using System;

public class GameManager 
{
    public static GameManager Instance;
    public DifficultyModes CurrentMode = DifficultyModes.easy;
    public static Action<ButtonHandler> ImageSelected;
    public static Action<ButtonHandler, ButtonHandler> MatchFound;
    public static Action<ButtonHandler, ButtonHandler> MatchNotFound;
    public static Action GameCompleted;
    public static GameManager _Instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = new GameManager();
            }
            return Instance;
        }
    }
    public const string ScaleUpAnim = "ScaleUp";
    public const string ScaleDownAnim = "Scale Down";

}

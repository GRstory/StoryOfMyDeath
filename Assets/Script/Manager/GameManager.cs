using UnityEngine;

public class GameManager : SingletonMonobehavior<GameManager>
{
    public UIManager _uiManager;

    protected override void Awake()
    {
        base.Awake();

        _uiManager = new UIManager();
    }


}

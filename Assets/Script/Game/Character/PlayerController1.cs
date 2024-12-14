using UnityEngine;

public class PlayerController1 : SingletonMonobehavior<PlayerController1>
{
    [SerializeField] private BubbleManager _bubbleManager;
    [SerializeField] private CharacterController _characterController;

    public string[] _testString;
    public int _count = 0;
    public IInteraction Interaction;

    protected override void Awake()
    {
        base.Awake();

        _bubbleManager = GetComponentInChildren<BubbleManager>();
    }

    private void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.F))
        {
            Interaction?.Interaction();
        }
    }

    public void AddBubble(string dialog)
    {
        int testIndex = Random.Range(0, _testString.Length);

        if(_bubbleManager.CanAddBubble())
        {
            _bubbleManager?.AddBubble(_testString[_count++]);
        }
    }

    public void Move()
    {
        //임시 코드
        float x = Input.GetAxisRaw("Horizontal");
        _characterController?.Move(new Vector3(x, 0, 0) * 2f * Time.deltaTime);
    }
}

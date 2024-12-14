using I2.Loc;
using System;
using UnityEngine;

public class MapTravel : MonoBehaviour, IInteraction
{
    public Direction Direction = Direction.up;
    public Transform TeleportPosition;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator?.SetTrigger(Enum.GetName(typeof(Direction), Direction));
    }

    public void Interaction()
    {
        PlayerController1.Instance.transform.position = TeleportPosition.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController1>(out PlayerController1 player))
        {
            OnPlayerTriggerEnter();
            player.Interaction = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController1>(out PlayerController1 player))
        {
            OnPlayerTriggerExit();
            player.Interaction = null;
        }
    }

    public void OnPlayerTriggerEnter()
    {
        _animator?.SetTrigger("active");
    }

    public void OnPlayerTriggerExit()
    {
        _animator?.SetTrigger("deactive");
    }
}
public enum Direction
{
    left,
    up,
    right,
    down,
}
using UnityEngine;

public interface IInteraction
{
    public abstract void Interaction();
    public abstract void OnPlayerTriggerEnter();
    public abstract void OnPlayerTriggerExit();
}

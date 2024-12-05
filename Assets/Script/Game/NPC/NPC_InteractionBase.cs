using UnityEngine;

public class NPC_InteractionBase : MonoBehaviour, IInteraction
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {

        }
    }

    public void Interaction()
    {
        
    }

    public void OnPlayerTriggerEnter()
    {
        
    }

    public void OnPlayerTriggerExit()
    {
        
    }
}

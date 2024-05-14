using UnityEngine;
using UnityEngine.UIElements;

public class DebugCheats : MonoBehaviour
{
    private CollisionHandler _collisionHandler;
    private Collider _collider;
    
    public bool collisionDisabled = false;
    
    private void Start()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        CheatLoadNextLevel();
        CheatDisableCollision();
    }

    private void CheatLoadNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _collisionHandler.LoadNextLevel();
        }
    }

    private void CheatDisableCollision()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
            _collider.enabled = !_collider.enabled;
        }
    }
}

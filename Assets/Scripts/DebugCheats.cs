using UnityEngine;

public class DebugCheats : MonoBehaviour
{
    public bool collisionDisabled = false;
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
    private CollisionHandler _collisionHandler;
    private Collider _collider;
    
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
#endif
}


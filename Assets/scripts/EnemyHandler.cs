using UnityEngine;
using Pathfinding;

public class EnemyHandler : MonoBehaviour
{
    public AIPath aiPath;
    public AnimationHandler animationHandler;

    private void Update()
    {
        if (aiPath.desiredVelocity.x > 0 || aiPath.desiredVelocity.y > 0)
            SetWalkAnimation();
        else SetIdleAnimation();
    }
    public void SetWalkAnimation()
    {
        SetAnimation(true);
    }
    public void SetIdleAnimation()
    {
        SetAnimation(false);
    }
    private void SetAnimation(bool state)
    {
        animationHandler.UpdateEnemyAnimation(state);
    }
}

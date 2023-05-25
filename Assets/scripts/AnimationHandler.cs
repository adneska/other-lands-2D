using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private string currentState;

    const string EnemyIdle = "EnemyIdle";
    const string EnemyWalk = "EnemyWalk";
    private void Awake()
    {
        _animator = GetComponent<Animator>();        
    }
    public void UpdateEnemyAnimation(bool isMoving)
    {
        if (isMoving)
        {
            ChangeAnimationState(EnemyWalk);
        }
        else
        {
            ChangeAnimationState(EnemyIdle);
        }
    }
    
    
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        _animator.Play(newState);
    }
}

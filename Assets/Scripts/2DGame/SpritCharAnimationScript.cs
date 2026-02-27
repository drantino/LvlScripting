using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public class SpritCharAnimationScript : MonoBehaviour
{
    private SpriteRenderer mySpriteRend;
    public List<AnimationStateData> animationStates = new List<AnimationStateData>();
    private Dictionary<PlayerAnimationState, AnimationData>
         animationDictionary = new Dictionary<PlayerAnimationState, AnimationData>();
    bool isPlaying = false;
    public PlayerAnimationState currentState;

    private void Start()
    {
        currentState = PlayerAnimationState.Idle_Down;
        InitializeDictionary();
        mySpriteRend = GetComponent<SpriteRenderer>();
        SpritCharScript spriteChar = GetComponent<SpritCharScript>();
        spriteChar.OnMove += SetAnimationState;
    }
    public void InitializeAnimation(AnimationData animation)
    {
        StopAllCoroutines();
        StartCoroutine(PlayAnimation(animation));
    }
    public void SetAnimationState(Vector2 moveDirection)
    {
        if (moveDirection == Vector2.zero)
        {
            currentState = GetIdleState(currentState);
        }
        if (moveDirection.y < 0)
        {
            currentState = PlayerAnimationState.Walk_Down;
        }
        else if (moveDirection.y > 0)
        {
            currentState = PlayerAnimationState.Walk_Up;
        }
        else if (moveDirection.x > 0)
        {
            currentState = PlayerAnimationState.Walk_Right;
        }
        else if (moveDirection.x < 0)
        {
            currentState = PlayerAnimationState.Walk_Left;
        }
        InitializeAnimation(animationDictionary[currentState]);
    }
    public PlayerAnimationState GetIdleState(PlayerAnimationState currentState)
    {
        PlayerAnimationState tmp = PlayerAnimationState.Idle_Down;
        switch (currentState)
        {
            case PlayerAnimationState.Walk_Down:
                {
                    tmp = PlayerAnimationState.Idle_Down;
                    break;
                }
            case PlayerAnimationState.Walk_Up:
                {
                    tmp = PlayerAnimationState.Idle_Up;
                    break;
                }
            case PlayerAnimationState.Walk_Left:
                {
                    tmp = PlayerAnimationState.Idle_Left;
                    break;
                }
            case PlayerAnimationState.Walk_Right:
                {
                    tmp = PlayerAnimationState.Idle_Right;
                    break;
                }
        }
        return tmp;
    }
    private IEnumerator PlayAnimation(AnimationData animation)
    {
        isPlaying = true;
        mySpriteRend.sprite = animation.frames[0];
        int frameCount = animation.frames.Length;
        int frameIndex = 0;

        while (isPlaying)
        {
            yield return new WaitForSeconds(animation.frameDelay);
            frameIndex++;
            if (frameIndex >= frameCount)
            {
                frameIndex = 0;
            }
            mySpriteRend.sprite = animation.frames[frameIndex];

            yield return null;
        }
        yield return null;
    }
    public void StopAnimation()
    {
        isPlaying = false;
    }
    public void InitializeDictionary()
    {
        foreach (AnimationStateData animationStateData in animationStates)
        {
            animationDictionary.Add(animationStateData.state, animationStateData.animation);
        }
    }
}
//[CreateAssetMenu(fileName = "AnimationSO", menuName = "AnimationSO")]
//public class AnimationData : ScriptableObject
//{
//    public string animationName;
//    public Sprite[] frames;
//    public float frameDelay;
//}
public enum PlayerAnimationState
{
    Idle_Down,
    Idle_Up,
    Idle_Left,
    Idle_Right,
    Walk_Down,
    Walk_Up,
    Walk_Left,
    Walk_Right
}
[Serializable]
public class AnimationStateData
{
    public PlayerAnimationState state;
    public AnimationData animation;
}
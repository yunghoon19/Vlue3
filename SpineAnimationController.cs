using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineAnimationController : MonoBehaviour
{
    public enum M_AnimState
    {
        IDLE, S_ATTACK, D_ATTACK, STURN
    }

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] arr_AnimClip;

    public M_AnimState m_animState;
    private string currentAnimation;
    private void FixedUpdate()
    {
        SetCurrentAnimation(m_animState);
    }

    private void AsncAnimation(AnimationReferenceAsset _animClip, bool _loop, float _timeScale)
    {
        if (_animClip.name.Equals(currentAnimation)) return;

        skeletonAnimation.state.SetAnimation(0, _animClip, _loop).TimeScale = _timeScale;
        skeletonAnimation.loop = _loop;
        skeletonAnimation.timeScale = _timeScale;

        currentAnimation = _animClip.name;
    }

    private void SetCurrentAnimation(M_AnimState _state)
    {
        switch (_state)
        {
            case M_AnimState.IDLE:
                AsncAnimation(arr_AnimClip[(int)M_AnimState.IDLE], true, 1f);
                break;
            case M_AnimState.S_ATTACK:
                AsncAnimation(arr_AnimClip[(int)M_AnimState.S_ATTACK], false, 1f);
                break;
            case M_AnimState.D_ATTACK:
                AsncAnimation(arr_AnimClip[(int)M_AnimState.D_ATTACK], true, 1f);
                break;
            case M_AnimState.STURN:
                AsncAnimation(arr_AnimClip[(int)M_AnimState.STURN], true, 1f);
                break;
            default:
                break;
        }

    }
}

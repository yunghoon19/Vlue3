using UnityEngine;
using System.Collections;
using Spine.Unity;

public class JumpAnim_B_End : StateMachineBehaviour
{
    // 
    //string name of animation clip
    public string animationClip;

    //layer to play animation on
    public int layer = 0;

    //for playing the anim at a different timescale if desired
    public float timeScale = 1f;

    private float normalizedTime;
    public float exitTime = .85f;
    public bool loop;

    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState spineAnimationState;
    private Spine.TrackEntry trackEntry;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (skeletonAnimation == null)
        {
            skeletonAnimation = animator.GetComponentInChildren<SkeletonAnimation>();
            spineAnimationState = skeletonAnimation.state;
        }

        trackEntry = spineAnimationState.SetAnimation(layer, animationClip, loop);
        trackEntry.TimeScale = timeScale;

        normalizedTime = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<CharacterMove>().isGrounded)
        {
            animator.SetTrigger("transition");
        }
        // 이게 전부였어요
        //normalizedTime = trackEntry.AnimationLast / trackEntry.AnimationEnd;  //3.6기준
        //                                                                      //normalizedTime = trackEntry.AnimationLast / trackEntry.AnimationEnd; //3.8 기준
        //                                                                      // 스파인 런타임 쪽은 버젼 바뀔때 마다 함수 이름 바꾸는게 일인듯 . . .

        ////애니메이션이 루프가 아닐경우 , 애니메이션이 끝나면 트리거 실행
        //if (!loop && normalizedTime >= exitTime)
        //{
        //    animator.SetTrigger("transition");
        //}
    }
    // 스테이트 종료시 발생할것  없다면 지워도되는부분
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
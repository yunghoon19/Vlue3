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
        // �̰� ���ο����
        //normalizedTime = trackEntry.AnimationLast / trackEntry.AnimationEnd;  //3.6����
        //                                                                      //normalizedTime = trackEntry.AnimationLast / trackEntry.AnimationEnd; //3.8 ����
        //                                                                      // ������ ��Ÿ�� ���� ���� �ٲ� ���� �Լ� �̸� �ٲٴ°� ���ε� . . .

        ////�ִϸ��̼��� ������ �ƴҰ�� , �ִϸ��̼��� ������ Ʈ���� ����
        //if (!loop && normalizedTime >= exitTime)
        //{
        //    animator.SetTrigger("transition");
        //}
    }
    // ������Ʈ ����� �߻��Ұ�  ���ٸ� �������Ǵºκ�
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
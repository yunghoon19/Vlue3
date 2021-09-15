using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CharacterMove : MonoBehaviour
{
    public PlayableDirector playableDirectors;

    private Vector3 moveTemp = Vector3.zero;
    private bool playerMove;
    private float walkSoundSecond;
 
    [Header("바닥 충돌 판정")]
    public bool isGrounded;
    [Header("걸음 속도")]
    public float speed = 8.0f;
    [Header("점프력")]
    public float jumpForce;

    // 플레이어 Animator
    public Animator animator;

    // 플레이어 Rigidbody2D
    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    
    private void FixedUpdate()
    {
        if (playerMove)
        {
            animator.SetBool("Run", true);
            transform.position += moveTemp * speed * Time.deltaTime;

            // 발걸음 재생 시간 재는 중
            walkSoundSecond += Time.deltaTime;
            // 0.5f 이상일때 새로 발걸음 재생 및 재생 시간 초기화
            if (walkSoundSecond >= 0.5f)
            {
                // SoundManager 싱글톤으로 관리 WalkSound 재생
                if (isGrounded)
                {
                    SoundManager.Inst.PlayWalkSound1();
                    walkSoundSecond = 0.0f;
                }
            }
        }
        else
        {
            // 이동 안할때는 달리기 포즈 안하기
            animator.SetBool("Run", false);
        }
    }

    private void Update()
    {
        //  플레이어의 transform을 매프레임마다 게임 매니저에 전송해준다
        GameManager.Inst.SetPlayerTransform(transform);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                animator.SetTrigger("Jump");
                playerRigidbody.velocity = new Vector2(0, jumpForce);
                isGrounded = false;
            }
        }
    }

    // 바닥에 닿았는지 판정
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BELOW")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TIMELINE"))
        {
            playerMove = false;
            playableDirectors.gameObject.SetActive(true);
            playableDirectors.Play();
        }
    }

    public void RightDown()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        moveTemp = new Vector3(1, 0, 0);
        playerMove = true;
    }
    public void RightUp()
    {
        moveTemp = Vector3.zero;
        playerMove = false;
    }

    public void LeftDown()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        moveTemp = new Vector3(-1, 0, 0);
        playerMove = true;
    }
    public void LeftUp()
    {
        moveTemp = Vector3.zero;
        playerMove = false;
    }

    public void OnJumpBtnDown()
    {
        // 점프 액션
        if (isGrounded == true)
        {
            animator.SetTrigger("Jump");
            playerRigidbody.velocity = new Vector2(0, jumpForce);
            isGrounded = false;
        }
    }

    public void Trapped()
    {
        speed = 2.0f;
    }

    public void UnTrapped()
    {
        speed = 8.0f;
    }
}
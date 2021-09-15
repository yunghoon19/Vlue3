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
 
    [Header("�ٴ� �浹 ����")]
    public bool isGrounded;
    [Header("���� �ӵ�")]
    public float speed = 8.0f;
    [Header("������")]
    public float jumpForce;

    // �÷��̾� Animator
    public Animator animator;

    // �÷��̾� Rigidbody2D
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

            // �߰��� ��� �ð� ��� ��
            walkSoundSecond += Time.deltaTime;
            // 0.5f �̻��϶� ���� �߰��� ��� �� ��� �ð� �ʱ�ȭ
            if (walkSoundSecond >= 0.5f)
            {
                // SoundManager �̱������� ���� WalkSound ���
                if (isGrounded)
                {
                    SoundManager.Inst.PlayWalkSound1();
                    walkSoundSecond = 0.0f;
                }
            }
        }
        else
        {
            // �̵� ���Ҷ��� �޸��� ���� ���ϱ�
            animator.SetBool("Run", false);
        }
    }

    private void Update()
    {
        //  �÷��̾��� transform�� �������Ӹ��� ���� �Ŵ����� �������ش�
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

    // �ٴڿ� ��Ҵ��� ����
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
        // ���� �׼�
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
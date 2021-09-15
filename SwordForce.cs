using UnityEngine;

public class SwordForce : MonoBehaviour
{
    public float sf_Speed = 100f;
    private Rigidbody2D sf_Rigidbody;
    private BeeBossAi beeBossAi;

    private void Awake()
    {
        sf_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        sf_Speed *= Time.deltaTime;

        Vector3 target_boss_Direct = GameManager.Inst.GetPlayerTransform().position - transform.position;

        sf_Rigidbody.velocity = target_boss_Direct.normalized * sf_Speed;
        
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        // ȸ�� (����)
        //transform.Rotate(0, 0, 720 * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        CharacterMove characterMove = other.GetComponent<CharacterMove>();

    //        if (characterMove != null)
    //        {
    //            //�˱Ⱑ �÷��̾�� �浹�� �߻��� �̺�Ʈ
    //        }
    //    }
    //}
}

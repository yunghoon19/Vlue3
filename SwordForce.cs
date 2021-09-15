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
        // 회전 (보류)
        //transform.Rotate(0, 0, 720 * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        CharacterMove characterMove = other.GetComponent<CharacterMove>();

    //        if (characterMove != null)
    //        {
    //            //검기가 플레이어와 충돌시 발생할 이벤트
    //        }
    //    }
    //}
}

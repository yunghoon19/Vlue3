using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BeeBossAi : SwordForceSpawner
{
    //  보스 패턴 열거체
    public enum BossPattern
    {
        Bee_Nomal, Bee_SF_Spawn, Bee_Charge,
        Pattern_Size
    }

    //  보스의 이동속도
    public float speed_Beeboss;

    //  현재 보스의 패턴
    public BossPattern current_BossPattern;

    //  검기 프리팹
    public GameObject[] prefab;

    //  타겟의 transform
    public Transform target;

    //  이동을 위한 보스의 리지드바디
    private Rigidbody2D bossRigidbody;

    //  애니메이션 재생할 애니메이터 컨트롤러
    private Animator animBeeBoss;

    private float lim_Distance;

    private void Awake()
    {
        lim_Distance = 15f;
        bossRigidbody = GetComponent<Rigidbody2D>();
        animBeeBoss = GetComponent<Animator>();
        speed_Beeboss *= Time.deltaTime;
    }

    private void Start()
    {
        //  항상 실행중인 기본 행동 코루틴
        StartCoroutine(P_Nomal());

        //  보스 패턴 실행 코루틴
        //StartCoroutine(PlayPattern());
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(P_Charge());
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(P_SpawnSF());
        }
    }
    private void LateUpdate()
    {

    }

    public float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }

    /* 보스가 타겟을 쳐다보는 매소드 */
    void RotateToTarget()
    {
        float dx = target.transform.position.x - transform.position.x;

        if (dx > 0)
        {
            transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else if (dx < 0)
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    /* 보스와 타겟의 거리를 체크 */
    bool BossToTarget_DistanceOK(float _norm)
    {
        Debug.Log("플레이어와 보스 사이의 거리 : " + Vector3.Distance(target.position, transform.position));
        
        if (Vector3.Distance(target.position, transform.position) >= _norm)
        {
            return true;
        }
        return false;
    }

    /* 패턴 선택 */

    /* 패턴 재생 코루틴 */


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* 패턴 메소드 */

    /* 보스의 NOMAL패턴 매소드 */
    IEnumerator P_Nomal()
    {
        while(true)
        {
            Vector3 dir;
            Vector3 target_boss_Direct = target.position - transform.position;

            // 보스와 타겟의 거리 체크
            if (BossToTarget_DistanceOK(lim_Distance))
            {
                // 플레이어와의 거리가 멀다면 다가온다.
                float ranX = Random.Range(-6f, 6f);
                float ranY = Random.Range(5f, 6f);

                // 플레이어의 반대 쪽 방향 백터.
                dir = new Vector3(target_boss_Direct.x + ranX, target_boss_Direct.y + ranY, 0);
            }
            else // 가깝다면 멀어진다
            {
                dir = -target_boss_Direct;
            }

            bossRigidbody.velocity = dir.normalized * speed_Beeboss;

            RotateToTarget();   // 이동 하며 보스가 플레이어를 쳐다봄

            yield return new WaitForSeconds(0.5f);
        }
    }

    /* 검기 패턴 코루틴 */
    IEnumerator P_SpawnSF()
    {
        animBeeBoss.SetTrigger("Slash_Attack");
        yield return new WaitForSeconds(0.4f);
        SpawnSF(prefab[0]);
        yield return new WaitForSeconds(0.6f);
        SpawnSF(prefab[0]);
        yield return new WaitForSeconds(1.2f);
        SpawnSF(prefab[0]);
        yield return null;
    }

    void Sturn()
    {
        animBeeBoss.SetTrigger("Sturn");
    }

    /* 돌진 패턴 코루틴 */
    IEnumerator P_Charge()
    {
        animBeeBoss.SetBool("Dash_Attack", true);

        Vector2 direction;

        GameObject red_Line = Instantiate(prefab[1], transform);

        direction = new Vector2(red_Line.transform.position.x - target.position.x, red_Line.transform.position.y - target.position.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        red_Line.transform.rotation = angleAxis;

        direction = target.position - transform.position;
        yield return new WaitForSeconds(1f);

        Destroy(red_Line);
        lim_Distance = 0f;
        bossRigidbody.AddForce(direction.normalized * 5000);

        //while (true)
        //{
        //    transform.position = Vector3.Lerp(transform.position, target.position * 2, Time.deltaTime * 1000);
        //    if (BossToTarget_DistanceOK(40f))
        //    {
        //        break;
        //    }
        //    yield return null;
        //}

        yield return new WaitForSeconds(0.5f);
        lim_Distance = 15f;
    }
}

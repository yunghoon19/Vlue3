using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BeeBossAi : SwordForceSpawner
{
    //  ���� ���� ����ü
    public enum BossPattern
    {
        Bee_Nomal, Bee_SF_Spawn, Bee_Charge,
        Pattern_Size
    }

    //  ������ �̵��ӵ�
    public float speed_Beeboss;

    //  ���� ������ ����
    public BossPattern current_BossPattern;

    //  �˱� ������
    public GameObject[] prefab;

    //  Ÿ���� transform
    public Transform target;

    //  �̵��� ���� ������ ������ٵ�
    private Rigidbody2D bossRigidbody;

    //  �ִϸ��̼� ����� �ִϸ����� ��Ʈ�ѷ�
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
        //  �׻� �������� �⺻ �ൿ �ڷ�ƾ
        StartCoroutine(P_Nomal());

        //  ���� ���� ���� �ڷ�ƾ
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

    /* ������ Ÿ���� �Ĵٺ��� �żҵ� */
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

    /* ������ Ÿ���� �Ÿ��� üũ */
    bool BossToTarget_DistanceOK(float _norm)
    {
        Debug.Log("�÷��̾�� ���� ������ �Ÿ� : " + Vector3.Distance(target.position, transform.position));
        
        if (Vector3.Distance(target.position, transform.position) >= _norm)
        {
            return true;
        }
        return false;
    }

    /* ���� ���� */

    /* ���� ��� �ڷ�ƾ */


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* ���� �޼ҵ� */

    /* ������ NOMAL���� �żҵ� */
    IEnumerator P_Nomal()
    {
        while(true)
        {
            Vector3 dir;
            Vector3 target_boss_Direct = target.position - transform.position;

            // ������ Ÿ���� �Ÿ� üũ
            if (BossToTarget_DistanceOK(lim_Distance))
            {
                // �÷��̾���� �Ÿ��� �ִٸ� �ٰ��´�.
                float ranX = Random.Range(-6f, 6f);
                float ranY = Random.Range(5f, 6f);

                // �÷��̾��� �ݴ� �� ���� ����.
                dir = new Vector3(target_boss_Direct.x + ranX, target_boss_Direct.y + ranY, 0);
            }
            else // �����ٸ� �־�����
            {
                dir = -target_boss_Direct;
            }

            bossRigidbody.velocity = dir.normalized * speed_Beeboss;

            RotateToTarget();   // �̵� �ϸ� ������ �÷��̾ �Ĵٺ�

            yield return new WaitForSeconds(0.5f);
        }
    }

    /* �˱� ���� �ڷ�ƾ */
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

    /* ���� ���� �ڷ�ƾ */
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

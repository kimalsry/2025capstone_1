using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar_action : MonoBehaviour
{
    public float moveSpeed = 2f; // 멧돼지 이동 속도
    public float moveRadius = 10f; // 이동 가능한 반경
    public float waitTime = 3f; // 다음 이동까지 대기 시간

    private Vector3 targetPosition;
    private float fixedY;

    void Start()
    {
        fixedY = transform.position.y; // y좌표 고정
        SetNewRandomTarget();
        StartCoroutine(MoveRoutine());
    }

    void SetNewRandomTarget()
    {
        float randomX = Random.Range(-moveRadius, moveRadius);
        float randomZ = Random.Range(-moveRadius, moveRadius);
        targetPosition = new Vector3(randomX, fixedY, randomZ) + transform.position;
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            // 목표 지점까지 이동
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                yield return null;
            }

            // 대기 후 새로운 목표 설정
            yield return new WaitForSeconds(waitTime);
            SetNewRandomTarget();
        }
    }
}
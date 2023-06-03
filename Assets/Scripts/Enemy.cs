using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyWallCheck enemyWallCheck;

    [SerializeField] GameObject player;

    Animator animator;
    Vector3 targetPosition;
    public Vector3 TargetPosition { get { return TargetPosition; } set { targetPosition = value; } }

    public float speed = 2f;


    float enemyMoveDistance = 4f;
    float avoidanceDistance = 2f;
    float minimumHeight = 0.5f;
    float distanceDirection;
    bool isWait = false;


    private void Start()
    {
        enemyWallCheck = GetComponentInChildren<EnemyWallCheck>();
        player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = PickRandomTarget();
        animator = GetComponent<Animator>();
        animator.SetBool("run",true);
    }

    private void Update()
    {
        ProtectHeigt();

        if (enemyWallCheck.isCanMove)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < avoidanceDistance)
            {
                EnemyAvoidPlayer();
            }
            else
            {
                DistanceProtect();
            }

            Vector3 newPosition = Vector3.LerpUnclamped(transform.position, targetPosition, speed * Time.deltaTime);
            transform.position = newPosition;
        }
        else
        {
            ProtectPosition();
        }
    }



    private void DistanceProtect()

    {
        if (Vector3.Distance(transform.position, targetPosition) < 1.2f || Vector3.Distance(transform.position, targetPosition) > distanceDirection + 0.4f)
        {
            targetPosition = PickRandomTarget();
        }
    }

    private void ProtectPosition()
    {
        targetPosition = transform.position;
        transform.position = targetPosition;
    }

    private void ProtectHeigt()
    {
        if (transform.position.y < minimumHeight || transform.position.y > minimumHeight)
        {
            transform.position = new Vector3(transform.position.x, minimumHeight, transform.position.z);
        }
    }

    private void EnemyAvoidPlayer()
    {
        if (IsObstacleInTheWay(targetPosition))
        {
            targetPosition = PickRandomTarget();
        }
    }

    private bool IsObstacleInTheWay(Vector3 newTargetPos)
    {
        Vector3 direction = (newTargetPos - transform.position).normalized;
        float distance = Vector3.Distance(newTargetPos, transform.position);
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance + 0.3f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return true;
            }
        }

        if (Vector3.Distance(newTargetPos, player.transform.position) < avoidanceDistance)
        {
            return true;
        }

        return false;

    }

    public Vector3 PickRandomTarget()
    {
        if (isWait) return targetPosition;

        int maxTries = 360;
        int currentTry = 0;

        do
        {
            Vector2 randomDirection2D = Random.insideUnitCircle * enemyMoveDistance;
            Vector3 randomDirection = new(randomDirection2D.x, 0f, randomDirection2D.y);
            targetPosition = transform.position + randomDirection.normalized * enemyMoveDistance;
            currentTry++;
        } while (IsObstacleInTheWay(targetPosition) && currentTry < maxTries);

        distanceDirection = Vector3.Distance(transform.position, targetPosition);
        // do while güvenlik açýðý araþtýr

        StartCoroutine(Wait());
        Vector3 relativePos = targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;
        return targetPosition;
    }

    private IEnumerator Wait()
    {
        isWait = true;
        yield return new WaitForSecondsRealtime(0.4f);
        isWait = false;
    }
}
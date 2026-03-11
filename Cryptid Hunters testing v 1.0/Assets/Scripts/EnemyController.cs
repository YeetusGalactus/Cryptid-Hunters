using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum EnemyState { 
    Patrolling,
    Following,
    Attacking
}

public class EnemyController : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Bite = Animator.StringToHash("Bite");

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] patrolPoints;
    


    [Header("Settings")]
    [SerializeField] private float patrolWaitTime = 2f;
    [SerializeField] private float stopAtDistance = 0.5f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private float losePlayerTime = 3f;
    [SerializeField] private float attackRange = 1.2f;



    private NavMeshAgent _agent;
    private Animator _animator;
    private int _currentPatrolIndex;
    private bool _isWaiting;
    private EnemyState _state = EnemyState.Patrolling;
    private float _timeSinceLostPlayer;
    private bool _isBiting;


    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        GoToNextPatrolPoint();
    }

    private void Update() {
        var distanceToPlayer = Vector3.Distance(player.position, transform.position);

        switch (_state) {
            case EnemyState.Patrolling:
                Patrol();
                if(distanceToPlayer <= detectionRange && CanSeePlayer()) {
                    _state = EnemyState.Following;
                }
                break;

            case EnemyState.Following:
                FollowPlayer();
                if(distanceToPlayer <= attackRange) {
                    _state = EnemyState.Attacking;
                    StartAttack();
                }
                if(!CanSeePlayer()) {
                    _timeSinceLostPlayer += Time.deltaTime;
                    if(_timeSinceLostPlayer >= losePlayerTime) {
                        _state = EnemyState.Patrolling;
                        GoToClosestPatrolPoint();
                    }
                } else {
                    _timeSinceLostPlayer = 0f;
                }
                break;
            case EnemyState.Attacking:
                
                //Attack();
                //if(!_isBiting && distanceToPlayer > attackRange) {
                //    _state = EnemyState.Following;
                //    _agent.isStopped = false;
                //}
                break;

        }
        
        UpdateAnimations();
    }

    private void StartAttack() {
        _agent.isStopped = true;
        _isBiting = true;
        _animator.SetTrigger(Bite);
    }

    private void Attack() {
        _agent.isStopped = true;
        var direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        if(direction != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        
    }

    private void OnBiteAnimationEnd() {
        _isBiting = false;
    }

    private void FollowPlayer() {
        _agent.SetDestination(player.position);
    }

    private void Patrol() {
        if (_isWaiting) {
            return;
        }
        if(!_agent.pathPending && _agent.remainingDistance <= stopAtDistance) {
            StartCoroutine(WaitAtPatrolPoint());
        }
    }

    private IEnumerator WaitAtPatrolPoint() {
        _isWaiting = true;
        _agent.isStopped = true;

        yield return new WaitForSeconds(patrolWaitTime);

        _agent.isStopped = false;
        GoToNextPatrolPoint();
        _isWaiting = false;
    }

    private void GoToClosestPatrolPoint() {
        if (patrolPoints.Length == 0) {
            return;
        }
        var closestIndex = 0;
        var closestDistance = float.MaxValue;

        for (var i = 0; i < patrolPoints.Length; i++) {
            var distance = Vector3.Distance(transform.position, patrolPoints[i].position);
            if(distance < closestDistance) {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        _currentPatrolIndex = closestIndex;
        _agent.SetDestination(patrolPoints[_currentPatrolIndex].position);
    }

    private void GoToNextPatrolPoint() {
        if(patrolPoints.Length == 0) {
            return;
        }
        _agent.SetDestination(patrolPoints[_currentPatrolIndex].position);
        _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void UpdateAnimations() {
        bool isWalking = _agent.velocity.sqrMagnitude > 0.01f;
        _animator.SetBool(IsWalking, isWalking);
    }

    private bool CanSeePlayer() {
        return IsFacingPlayer() && HasClearPathToPlayer();
    }

    private bool IsFacingPlayer() {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        var angle = Vector3.Angle(transform.forward, dirToPlayer);
        return angle <= viewAngle / 2f;
    }

    private bool HasClearPathToPlayer() {
        var dirToplayer = player.position - transform.position;
        if(Physics.Raycast(transform.position, dirToplayer.normalized, out RaycastHit hit, dirToplayer.magnitude)) {
            return hit.transform == player;
        }

        return true;
    }
}

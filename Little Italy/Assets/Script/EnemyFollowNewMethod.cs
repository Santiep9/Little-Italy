using System;
using UnityEngine;

namespace Systems.AI
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyFollowNewMethod : MonoBehaviour
    {
        private enum State { Patrolling, Chasing }

        [Header("Configuracion de Vision")] 
        [SerializeField] private float viewRadius =0.8f;
        [SerializeField] private float viewAngle = 90f;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask obstacleLayer;
        
        [Header("Configuracion de Movimiento")]
        [SerializeField] private float patrolSpeed = 2f;
        [SerializeField] private float chaseSpeed = 5.5f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private Transform[] waypoints;

        [Header("Script de Persecución")]
        [SerializeField] private EnemyFollow enemyFollowScript;
        

        private State _currentState;
        private Rigidbody2D rb;
        private Transform _targetPlayer;
        private int _currentWaypointIndex = 0;
        private Vector2 _moveDirection;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
            _currentState = State.Patrolling;

            RotateSprite();

            if (enemyFollowScript == null)
            {
                enemyFollowScript = GetComponent<EnemyFollow>();
            }

            if (enemyFollowScript != null)
            {
                enemyFollowScript.enabled = false;
            }
        }

        private void Update()
        {
            LookForPlayer();
            StateMachineLogic();
            if (_currentState == State.Patrolling)
            {
                RotateTowardsMovement(); 
            }
            
        }

        private void FixedUpdate()
        {
            if (_currentState == State.Patrolling)
            {
                MoveCharacter();
            }
            
        }

        private void LookForPlayer()
        {
            Collider2D playerInArea = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayer);

            if (playerInArea != null)
            {
                Transform target = playerInArea.transform;
                Vector2 dirToTarget = (target.position - transform.position).normalized;

                if (Vector2.Angle(transform.up, dirToTarget) < viewAngle / 2)
                {
                    float distToTarget = Vector2.Distance(transform.position, target.position);

                    if (!Physics2D.Raycast(transform.position, dirToTarget, distToTarget, obstacleLayer))
                    {
                        _targetPlayer = target;
                        return;
                    }
                }
            }
            _targetPlayer = null;
        }

        private void StateMachineLogic()
        {
            switch (_currentState)
            {
                case State.Patrolling:
                    if (_targetPlayer != null)
                    {
                        _currentState= State.Chasing;
                        OnStartChasing();
                    }
                    CalculatePatrolDirection();
                    break;
                case State.Chasing:
                    if (_targetPlayer == null)
                    {
                        _currentState = State.Patrolling;
                        OnStopChasing();
                    }
                    break;
                    
            }
        }

        private void OnStartChasing()
        {
            if (enemyFollowScript != null)
            {
                enemyFollowScript.enabled = true;
            }
            rb.linearVelocity = Vector2.zero;
        }

        private void OnStopChasing()
        {
            if (enemyFollowScript != null)
            {
                enemyFollowScript.enabled = false;
            }
        }
        private void CalculateChaseDirection()
        {
            if (_targetPlayer != null) return;
            _moveDirection = (_targetPlayer.position - transform.position).normalized;
        }

        private void CalculatePatrolDirection()
        {
            if (waypoints.Length == 0)
            {
                _moveDirection = Vector2.zero;
                return;
            }
            
            Transform wp = waypoints[_currentWaypointIndex];
            if (Vector2.Distance(transform.position, wp.position) < 0.2)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            }
            
            _moveDirection = (wp.position - transform.position).normalized;
        }

        private void MoveCharacter()
        {
            float speed = (_currentState == State.Chasing) ? chaseSpeed : patrolSpeed;
            
            rb.MovePosition(rb.position + _moveDirection * speed * Time.fixedDeltaTime);
        }
        public void RotateSprite()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            if (sr != null)
            {
                sr.transform.Rotate(0f, 0f, 90f);
            }
        }

        private void RotateTowardsMovement()
        {
            if (_moveDirection == Vector2.zero) return;
            
            float angle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg - 90f;
            
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, viewRadius);

            Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);
            Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);

            if (_targetPlayer != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, _targetPlayer.position);
            }
        }

        private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if(!angleIsGlobal) angleInDegrees -= transform.eulerAngles.z;
            return new Vector3(Mathf.Sin(angleInDegrees* Mathf.Deg2Rad), Mathf.Cos(angleInDegrees* Mathf.Deg2Rad), 0);
        }
    }

}
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterCharacter.Unity.StatePatternInUnity
{
    public class MonsterCharacter : MonoBehaviour
    {
        #region Variables
        // behaviour
        public StateMachine movementSM;
        public ActiveState active;
        public AggressiveState aggressive;
        public IdleState idle;

        // Controller
        public Player player;

        [SerializeField]
        private LayerMask mask;

        [SerializeField]
        private float visionDistance;
        [SerializeField]
        private float angle;

        //Motor
        private NavMeshAgent agent;

        [SerializeField]
        private PointsScript point;
        [SerializeField]
        private GameObject nextPoint;

        private float distanceToChangeGoal = 1f;

        private Vector3 lastPositionPlayer;
        private bool pursuit = false;

        [SerializeField]
        private GameObject[] allPoints;

        //Shoot
        public Weapon weapon;
        public bool fireRateIsGone = true;

        public Animator gunAnimator;

        //Monster
        public Monster monster;

        // Rotate

        #endregion

        #region Methods

        
        public bool FindPlayer()
        {
            if (player == null) { Debug.Log("Im Win"); movementSM.ChangeState(idle); }
            if (BotSee.IsVisibleUnit<Player>(player, transform, angle, visionDistance, mask))
            {
                lastPositionPlayer = player.transform.position;
                return true;
            }
            return false;
        }

        public void Move()
        {
            if (agent.isStopped == true)
                agent.isStopped = false;

            if (agent.remainingDistance < distanceToChangeGoal)
            {
                if (!pursuit)
                {
                    nextPoint = point.ChoiseRandomNextPoint();
                    point = nextPoint.GetComponent<PointsScript>();
                    agent.destination = nextPoint.transform.position;
                }
                else
                {
                    nextPoint = FindNearestPoint();
                    point = nextPoint.GetComponent<PointsScript>();
                    pursuit = false;
                }
            }
        }
        public void Stay()
        {
            agent.isStopped = true;
        }

        private GameObject FindNearestPoint()
        {
            GameObject nearestPoint = null;
            float minimalDistance = Mathf.Infinity;
            foreach (GameObject point in allPoints)
            {
                Vector3 curPos = point.transform.position;
                if ((gameObject.transform.position - curPos).sqrMagnitude < minimalDistance)
                {
                    minimalDistance = (gameObject.transform.position - curPos).sqrMagnitude;
                    nearestPoint = point;
                }
            }
            return nearestPoint;
        }

        public IEnumerator PlayerLossAndMoveToLastPlayerPos()
        {
            yield return new WaitForSeconds(0.5f);
            movementSM.ChangeState(active);
            agent.destination = lastPositionPlayer;
            pursuit = true;
        }
        // задержка выстрела
        public IEnumerator FireRate()
        {
            yield return new WaitForSeconds(this.weapon.delay);
            fireRateIsGone = true;
        }
        
        public void Shoot()
        {
            fireRateIsGone = false;
            weapon.animator.SetTrigger("Shoot");
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, transform.forward, out _hit, weapon.range))
            {
                StartCoroutine(FireRate());
                if (_hit.collider.tag == "Player")
                {
                    string namePlayer = _hit.collider.name;
                    Player player = _hit.collider.GetComponent<Player>();
                    player.TakeDamage(weapon.damage);
                }
            }
        }
        public void LookAtPlayer()
        {
            gameObject.transform.LookAt(player.transform.position);
        }

        #endregion

        #region MonoBehaviour Callbacks
        private void Start()
        {
            movementSM = new StateMachine();
            active = new ActiveState(this, movementSM);
            aggressive = new AggressiveState(this, movementSM);
            idle = new IdleState(this, movementSM);


            monster = GetComponent<Monster>();
            agent = GetComponent<NavMeshAgent>();
            if (agent == null) Debug.LogError("Нету агента");
            agent.destination = nextPoint.transform.position;

            movementSM.Initialize(active);
        }


        private void Update()
        {
            movementSM.CurrentState.LogicUpdate();
        }

        void FixedUpdate()
        {
            movementSM.CurrentState.PhysicsUpdate();
        }
        #endregion
    }
}
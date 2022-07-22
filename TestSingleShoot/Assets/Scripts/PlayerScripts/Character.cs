using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterPlayer.Unity.StatePatternInUnity
{
    public class Character : MonoBehaviour
    {
        #region Variables
        //State
        public StateMachine movementSM;
        public StandingState standing;

        //motor
        [SerializeField]
        private Camera cam;
        private Rigidbody rb;

        private Vector3 velocity = Vector3.zero;
        private Vector3 rotation = Vector3.zero;
        private Vector3 rotationCam = Vector3.zero;

        //ShootVariables
        public Weapon weapon;
        public bool fireRateIsGone = true;

        #endregion

        #region Methods
        public void Move(Vector3 velocity)
        {
            if (velocity != Vector3.zero)
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        public void Rotate(Vector3 rotation)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
            if (cam != null)
                cam.transform.Rotate(-rotationCam);
        }
        public void RotateCam(Vector3 _rotationCam)
        {
            //Debug.Log((float)Mathf.Acos(cam.transform.position.x) * Mathf.Rad2Deg);
            rotationCam = _rotationCam;
        }

        public IEnumerator FireRate(float delay)
        {
            yield return new WaitForSeconds(delay);
            fireRateIsGone = true;
        }
        public void Shoot()
        {
            fireRateIsGone = false;
            weapon.animator.SetTrigger("Shoot");
            RaycastHit _hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range))
            {
                FireRate(weapon.delay);
                if (_hit.collider.tag == "Monster")
                {
                    string nameMonster = _hit.collider.name;
                    Monster monster = _hit.collider.GetComponent<Monster>();
                    monster.TakeDamage(weapon.damage);
                }
            }
        }
        #endregion

        #region MonoBehaviour Callbacks
        void Start()
        {
            if (cam == null)
            {
                Debug.LogError("No Camera");
            }
            rb = GetComponent<Rigidbody>();

            movementSM = new StateMachine();
            standing = new StandingState(this, movementSM);

            movementSM.Initialize(standing);
        }

        void Update()
        {
            movementSM.CurrentState.HandleInput();

            movementSM.CurrentState.LogicUpdate();
        }
        private void FixedUpdate()
        {
            movementSM.CurrentState.PhysicsUpdate();

        }
        #endregion

    }
}


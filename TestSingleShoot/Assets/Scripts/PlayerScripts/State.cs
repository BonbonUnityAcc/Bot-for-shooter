using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterPlayer.Unity.StatePatternInUnity
{
    public abstract class State
    {
        protected Character character;
        protected StateMachine stateMachine;

        private float yRot;
        private float xRot;

        private Vector3 rotation;
        private Vector3 camRotation;

        protected float lookSpeed = 5f;


        protected State(Character character, StateMachine stateMachine)
        {
            this.character = character;
            this.stateMachine = stateMachine;
        }
        public virtual void Enter()
        {

        }
        public virtual void HandleInput()
        {
            if (Input.GetButton("Fire1"))
            {
                if (character.fireRateIsGone)
                {
                    character.Shoot();
                    character.StartCoroutine(character.FireRate(character.weapon.delay));
                    if (character.weapon.particleSystem.isPlaying == false)
                        character.weapon.particleSystem.Play(true);
                }
            }
            else if (character.weapon.particleSystem.isPlaying == true)
                character.weapon.particleSystem.Stop(true);

            yRot = Input.GetAxisRaw("Mouse X");
            xRot = Input.GetAxisRaw("Mouse Y");
        }
        public virtual void LogicUpdate()
        {
            rotation = new Vector3(0f, yRot, 0f) * lookSpeed;
            camRotation = new Vector3(xRot, 0f, 0f) * lookSpeed;
        }
        public virtual void PhysicsUpdate()
        {
            character.Rotate(rotation);
            character.RotateCam(camRotation);
        }
        public virtual void Exit()
        {

        }
    }
}


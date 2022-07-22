using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CharacterPlayer.Unity.StatePatternInUnity
{
    public class GroundedState : State
    {
        protected float speed = 5f;

        private float horizontalInput;
        private float verticalInput;

        

        private Vector3 velocity;
        
        public GroundedState(Character character, StateMachine stateMachine) : base(character, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void HandleInput()
        {
            base.HandleInput();
            verticalInput = Input.GetAxisRaw("Horizontal");
            horizontalInput = Input.GetAxisRaw("Vertical");
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            velocity = (character.transform.right * verticalInput + character.transform.forward * horizontalInput).normalized * speed;
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            character.Move(velocity);
        }
    }
}


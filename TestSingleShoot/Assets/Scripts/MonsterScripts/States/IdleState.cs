using UnityEngine;

namespace MonsterCharacter.Unity.StatePatternInUnity
{
    public class IdleState : State
    {

        public IdleState(MonsterCharacter character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Idle State Enter");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
        }
    }
}
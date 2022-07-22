using UnityEngine;

namespace MonsterCharacter.Unity.StatePatternInUnity
{
    public class ActiveState : State
    {

        public ActiveState(MonsterCharacter character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Active Enter State");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (monsterCh.FindPlayer()) 
                stateMachine.ChangeState(monsterCh.aggressive);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            monsterCh.Move();
            if (monsterCh.monster.takingDamage)
                monsterCh.transform.Rotate(new Vector3(0, 10, 0));
        }
    }
}
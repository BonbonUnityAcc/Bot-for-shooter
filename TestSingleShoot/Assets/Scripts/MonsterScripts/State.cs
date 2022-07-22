namespace MonsterCharacter.Unity.StatePatternInUnity
{
    public abstract class State
    {
        protected MonsterCharacter monsterCh;
        protected StateMachine stateMachine;

        protected State(MonsterCharacter monsterCh, StateMachine stateMachine)
        {
            this.monsterCh = monsterCh;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}

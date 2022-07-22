using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace MonsterCharacter.Unity.StatePatternInUnity
{
    public class AggressiveState : State
    {
        private bool courIsGone = false;
        private float delayAfterFire = 4f;
        private bool goFire = false;

        public AggressiveState(MonsterCharacter character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }
        private IEnumerator UsersFireRate()
        {
            yield return new WaitForSeconds(delayAfterFire);
            goFire = true;
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Monster Aggressive Enter!");
            monsterCh.fireRateIsGone = true;
            monsterCh.Stay();
            monsterCh.StartCoroutine(UsersFireRate());
        }

        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            // теряем игрока из вида
            if (!monsterCh.FindPlayer())
            {
                if (!courIsGone)
                {
                    monsterCh.StartCoroutine(monsterCh.PlayerLossAndMoveToLastPlayerPos());
                    courIsGone = true;
                }
            }
            else
            {
                if (courIsGone)
                {
                    monsterCh.StopCoroutine(monsterCh.PlayerLossAndMoveToLastPlayerPos());
                    courIsGone = false;
                }
            }
            if (monsterCh.monster.takingDamage) monsterCh.monster.takingDamage = false;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            monsterCh.LookAtPlayer();

            if (goFire)
            {
                if (monsterCh.fireRateIsGone)
                {
                    Debug.Log("Monster shoot");
                    monsterCh.Shoot();
                    if (monsterCh.weapon.particleSystem.isPlaying == false)
                        monsterCh.weapon.particleSystem.Play(true);
                    else if (monsterCh.weapon.particleSystem.isPlaying == true)
                        monsterCh.weapon.particleSystem.Stop(true);
                }
            }

        }
    }
}
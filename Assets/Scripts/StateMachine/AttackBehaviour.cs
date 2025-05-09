using UnityEngine;

namespace FlatformerTest {
    public class AttackBehaviour : StateMachineBehaviour {
        #region Variables
        //제어할 Animator 내의 Parameter
        [SerializeField] string boolParam;
        //상태머신 점검
        public bool updateOnState;
        public bool updateOnStateMachine;
        //Parameter가 제어될 값
        public bool valueEnter;
        public bool valueExit;

        #endregion
        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if(updateOnState) {
                animator.SetBool(boolParam, valueEnter);
            }
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        //OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
          if(updateOnState) {
                animator.SetBool(boolParam, valueExit);
            }
        }

        // OnStateMove is called before OnStateMove is called on any state inside this state machine
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateIK is called before OnStateIK is called on any state inside this state machine
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if(updateOnStateMachine) {
                animator.SetBool(boolParam, valueEnter);
            }
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if(updateOnStateMachine) {
                animator.SetBool(boolParam, valueExit);
            }
        }
    }
}
using UnityEngine;

namespace FlatformerTest {
    public class SetFloatBehaviour : StateMachineBehaviour {
        //float 형 파라미터를 관리하는 behaviour

        #region Variables
        //제어할 Animator 내의 Parameter
        [SerializeField] string floatParam;
        //상태머신 점검
        public bool updateOnStateEnter;
        public bool updateOnStateExit;
        public bool updateOnStateMachineEnter;
        public bool updateOnStateMachineExit;
        //Parameter가 제어될 값
        public float valueEnter;
        public float valueExit;
        #endregion

        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(updateOnStateEnter) {
                animator.SetFloat(floatParam, valueEnter);
            }
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(updateOnStateExit) {
                animator.SetFloat(floatParam, valueExit);
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
            if(updateOnStateMachineEnter) {
                animator.SetFloat(floatParam, valueEnter);
            }
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if(updateOnStateMachineExit) {
                animator.SetFloat(floatParam, valueExit);
            }
        }
    }
}
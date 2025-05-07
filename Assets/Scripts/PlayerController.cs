using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlatformerTest {
    //플레이어 이동을 제어하는 클래스
    public class PlayerController : MonoBehaviour {
        #region Variables
        Rigidbody2D rb2d;

        //걷는 속도와 달리는 속도
        float walkSpeed = 3f;
        float runSpeed = 6f;

        //이동에 사용할 입력값
        Vector2 inputVector = new Vector2(0, 0);

        //이동/달리기 키 입력
        bool isMoving = false;
        bool isRunning = false;

        //바라보고 있는 방향
        bool isFacingRight = true;
        #endregion

        #region Property
        public bool IsMoving {
            get => isMoving;
            set { 
                isMoving = value; 
                GetComponent<Animator>().SetBool(AnimationString.walkinput, isMoving);
            }
        }
        public bool IsRunning {
            get => isRunning;
            set {
                isRunning = value;
                GetComponent<Animator>().SetBool(AnimationString.runinput, isRunning);
                rb2d.linearVelocity = new Vector2(inputVector.x * runSpeed, 0f);
            }
        }
        public float GetSpeed {
            get {
                if (IsMoving) {
                    if (IsRunning) return runSpeed;
                    else return walkSpeed;
                }
                else return 0f;
            }
        }

        public bool IsFacingRight {
            get => isFacingRight;
            set {
                //scale.x에 -1을 곱하면 좌우 반전 효과가 있음
                if (isFacingRight != value) {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, 1f, 1f);
                }
                isFacingRight = value;

            }
        }
        #endregion

        #region Unity Event Method
        private void Start() {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            rb2d.linearVelocity = new Vector2(inputVector.x * GetSpeed, 0f);
        }
        #endregion

        #region Custom Method
        public void OnMove(InputAction.CallbackContext context) {
            //입력값을 받아옴
            inputVector = context.ReadValue<Vector2>();
            IsMoving = (inputVector != Vector2.zero);
            SetDirection();
        }

        public void OnRun(InputAction.CallbackContext context) {
            //TODO : 토글형식/누르기 형식 전환하는 옵션 추가
            //누르기 형식으로 달리기
            if (context.started) {
                //달리기 시작
                IsRunning = true;
                SetDirection();
            }
            else if (context.canceled) {
                //달리기 멈춤
                IsRunning = false;
                SetDirection();
            }
        }

        void SetDirection() {
            //입력값에 따라 바라보는 방향을 설정
            if (inputVector.x > 0 && !isFacingRight) {
                IsFacingRight = true;
            }
            else if (inputVector.x < 0 && isFacingRight) {
                IsFacingRight = false;
            }
        }
        #endregion
    }
}


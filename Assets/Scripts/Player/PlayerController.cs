using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlatformerTest {
    //플레이어 이동을 제어하는 클래스
    public class PlayerController : MonoBehaviour {
        #region Variables
        Rigidbody2D rb2d;

        //걷는 속도와 달리는 속도
        [SerializeField] float walkSpeed = 3f;
        [SerializeField] float runSpeed = 6f;
        [SerializeField] float jumpSpeed = 10f;

        //애니메이션을 위한 애니메이터
        Animator animator;
        //이동에 사용할 입력값
        Vector2 inputVector = new Vector2(0, 0);

        //이동/달리기 키 입력
        bool isMoving = false;
        bool isRunning = false;
        float airSpeedMult = 0.5f; //공중에서의 속도 감소 비율

        //바라보고 있는 방향
        bool isFacingRight = true;

        //벽타기와 땅 닿는거 판정 용
        [SerializeField] CheckCollision cc;

        #endregion

        #region Property
        public bool IsMoving {
            get => isMoving;
            set { 
                isMoving = value; 
                animator.SetBool(AnimationString.walkinput, isMoving);
            }
        }
        public bool IsRunning {
            get => isRunning;
            set {
                isRunning = value;
                animator.SetBool(AnimationString.runinput, isRunning);
                rb2d.linearVelocity = new Vector2(inputVector.x * runSpeed, 0f);
            }
        }

        public float GetSpeed {
            get {
                if(CannotMove) return 0f; //움직일 수 없는 상태일 시 속도 0

                if (IsMoving && !cc.IsWall) {
                    //공중에 떠 있을 시
                    if (!cc.IsGround) {
                        if (IsMoving) return runSpeed * airSpeedMult;
                    }
                    //그 외
                    if (IsRunning) return runSpeed;
                    else return walkSpeed;
                } //유휴상태 혹은 벽에 충돌 시
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

        public bool CannotMove {
            get => animator.GetBool(AnimationString.cannotmove);
        }
        #endregion

        #region Unity Event Method
        private void Start() {
            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            rb2d.linearVelocity = new Vector2(inputVector.x * GetSpeed, rb2d.linearVelocity.y);
            animator.SetFloat(AnimationString.yvelocity, rb2d.linearVelocityY);
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

        public void OnJump(InputAction.CallbackContext context) {
            //다중 점프를 방지하기 위해 IsGround를 사용
            if (context.started && cc.IsGround) {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocityX, jumpSpeed);
                //점프 트리거 설정
                animator.SetTrigger(AnimationString.jumpinput);
            }
        }

        public void OnAttack(InputAction.CallbackContext context) {
            if (context.started) {
                //지상 공격
                if(cc.IsGround) animator.SetTrigger(AnimationString.groundattack);
                //TODO : 공격 도중 이동 불가
                //TODO : 공격 도중 점프 불가

            }
        }
        #endregion
    }
}


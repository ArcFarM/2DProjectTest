using UnityEngine;

namespace FlatformerTest {
    public class EnemyController : MonoBehaviour {
        #region Variables
        Rigidbody2D rb2d;

        //적 이동속도
        [SerializeField] float runSpeed = 6f;
        [SerializeField] float jumpSpeed = 10f;

        //애니메이션을 위한 애니메이터
        [SerializeField] Animator animator;

        //바라보고 있는 방향
        Vector2 dirVector = Vector2.right;

        //벽타기와 땅 닿는거 판정 용
        [SerializeField] CheckCollision cc;

        //플레이어 감지
        [SerializeField] Detector detector;
        bool findplayer = false;
        #endregion

        #region Property

        public bool FindPlayer {
            get => findplayer;
            set {
                findplayer = value;
                animator.SetBool(AnimationString.findplayer, value);
            }
        }

        public float GetSpeed {
            get {
                if (cc.IsWall || FindPlayer) return 0f;
                return runSpeed;
            }
        }
        public bool isRight {
            get => dirVector.x > 0;
        }
        public bool isLeft {
            get => dirVector.x < 0;
        }

        public bool CannotMove {
            get => animator.GetBool(AnimationString.cannotmove);
        }
        #endregion

        #region Unity Event Method
        private void Start() {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            //플레이어가 감지되면 공격 명령 실행
            FindPlayer = detector.contactedList.Count > 0;
        }

        private void FixedUpdate() {
            rb2d.linearVelocity = new Vector2(dirVector.x * GetSpeed, rb2d.linearVelocity.y);
            if(cc.IsWall && cc.IsGround) {
                FlipDirection();
            }
        }
        #endregion

        #region Custom Method
        //방향 전환하기
        void FlipDirection() {
            if(isRight && dirVector == Vector2.right) {
                dirVector = Vector2.left;
                FlipSprite();
            }
            else if (isLeft && dirVector == Vector2.left) {
                dirVector = Vector2.right;
                FlipSprite();
            }
        }
        //방향에 맞춰 스프라이트 조절
        void FlipSprite() {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        #endregion
    }

}


using UnityEngine;

namespace FlatformerTest {
    //Collider를 이용하여 현재 벽면/지면에 충돌하고 있는 지 확인
    public class CheckCollision : MonoBehaviour {
        #region Variables
        CapsuleCollider2D cc2d;
        //충돌을 감지할 범위
        [SerializeField] float g_checkRange = 0.05f;
        [SerializeField] float c_checkRange = 0.05f;
        [SerializeField] float w_checkRange = 0.05f;
        //충돌에 사용할 조건
        [SerializeField] ContactFilter2D cf2d;
        //캐스팅된 리스트
        RaycastHit2D[] g_hitResult = new RaycastHit2D[10];
        RaycastHit2D[] c_hitResult = new RaycastHit2D[10];
        RaycastHit2D[] w_hitResult = new RaycastHit2D[10];
        //지면/벽면 확인 결과
        [SerializeField] bool isGround = false;
        [SerializeField] bool isCeiling = false;
        [SerializeField] bool isWall = false;
        //animation string 세팅용
        [SerializeField] Animator animator;
        #endregion

        #region Properties
        public bool IsGround {
            get { return isGround; }
            set { 
                isGround = value;
                animator.SetBool(AnimationString.isground, isGround);
            }
        }
        public bool IsCeiling {
            get { return isCeiling; }
            set { 
                isCeiling = value;
                animator.SetBool(AnimationString.isceiling, isCeiling);
            }
        }
        public bool IsWall {
            get { return isWall; }
            set { 
                isWall = value;
                animator.SetBool(AnimationString.iswall, isWall);
            }
        }

        Vector2 PlayerDirection {
            get {
                if (gameObject.transform.localScale.x > 0) return Vector2.right;
                else return Vector2.left;
            }
        }
        #endregion

        #region Unity Event Methods
        private void Awake() {
            cc2d = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            //캐스팅된 리스트 초기화
            InitArray();
            //캐스팅 - 아래 방향으로 checkRange 내에 contactfilter에 해당하는 물체가 있다면 hitResult 배열에 저장
            IsGround = cc2d.Cast(Vector2.down, cf2d, g_hitResult, g_checkRange) > 0;
            //같은 원리로 좌우를 통해 벽을, 윗 방향으로 천장을 확인 가능
            IsCeiling = cc2d.Cast(Vector2.up, cf2d, c_hitResult, c_checkRange) > 0;
            IsWall = cc2d.Cast(PlayerDirection, cf2d, w_hitResult, w_checkRange) > 0;

            GetComponent<Animator>().SetBool(AnimationString.isground, (IsGround || IsWall || IsCeiling));
        }
        #endregion

        #region Custom Methods
        void InitArray() {
            for (int i = 0; i < g_hitResult.Length; i++) {
                g_hitResult[i] = new RaycastHit2D();
            }
            for (int i = 0; i < c_hitResult.Length; i++) {
                c_hitResult[i] = new RaycastHit2D();
            }
            for (int i = 0; i < w_hitResult.Length; i++) {
                w_hitResult[i] = new RaycastHit2D();
            }
        }
        #endregion
    }
}


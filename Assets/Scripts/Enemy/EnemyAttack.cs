using UnityEngine;

namespace FlatformerTest {
    public class EnemyAttack : MonoBehaviour {
        #region Variables
        //공격력
        [SerializeField] private float attackPower = 10f;
        //공격 재사용 대기 시간
        static public float attackCooldown = 1.5f;

        //공격한 상대를 뒤로 밀어내는 힘
        [SerializeField] private Vector2 knockbackForce = new Vector2(3f, 2.5f);
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter2D(Collider2D collision) {
            PlayerGetHit ps = collision.GetComponent<PlayerGetHit>();
            //공격자가 바라보는 방향에 맞춰서 넉백 방향 조절
            if (transform.localScale.x < 0) {
                knockbackForce.x *= -1f;
            }
            //컴포넌트가 없으면 공격 불가
            if (ps == null) { Debug.Log("PlayerGetHit is null"); return; }
            //무적 상태면 공격은 하지만, 피해는 입지 않음 (무적시간 조정을 위해서 실행은 함)
            if (ps.IsInvincible) { 
                collision.gameObject.GetComponent<PlayerGetHit>().TakeDamage(0f, new Vector2(0f, 0f));
                return;

            }
            else collision.gameObject.GetComponent<PlayerGetHit>().TakeDamage(attackPower, knockbackForce);

            //피격당한 상대를 넉백
            
        }
        #endregion

        #region Custom Method

        #endregion
    }

}

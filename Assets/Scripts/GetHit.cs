using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FlatformerTest {


    public class PlayerGetHit : MonoBehaviour {
        #region Variables
        //최대 체력과 현재 체력
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth = 100f;
        //사망 판정
        bool isdead = false;
        //피격 무적 관련
        [SerializeField] private float invincibilityDuration = 1f;
        private float invincibilityTimer = 0f;

        //애니메이터
        [SerializeField] private Animator animator;

        //피격 넉백 이벤트
        public UnityAction<float, Vector2> HitAction;
        #endregion

        #region Property
        public float MaxHealth {
            get => maxHealth;
            private set => maxHealth = value;
        }
        public float CurrentHealth {
            get => currentHealth;
            private set => currentHealth = value;
        }
        public bool IsDead {
            get => currentHealth > 0;
            set => isdead = value;
        }
        public bool IsInvincible {
            get => invincibilityTimer > 0f;
        }

        public bool LockSpeed {
            get => animator.GetBool(AnimationString.lockspeed);
            set => animator.SetBool(AnimationString.lockspeed, value);
        }
        #endregion

        #region Unity Event Method
        private void Start() {
            CurrentHealth = MaxHealth;
        }
        private void Update() {
        }
        #endregion
        #region Custom Method
        //체력 감소 메소드
        public void TakeDamage(float damage, Vector2 knockback) {
            //무적이니까 피격 처리 안함
            if (IsInvincible || damage == 0) return;

            //피격 시 애니메이션 처리 - 플레이어와 적이 모두 같은 트리거명을 사용해서 상관 없음
            animator.SetTrigger(AnimationString.gethit);

            currentHealth -= damage;
            if(currentHealth <= 0 && !IsDead) {
                Die();
                IsDead = true;
            }
            //무적시간 조정 타이머 실행
            StartCoroutine(InvincilbilityTimer());

            //넉백처리 실행
            HitAction?.Invoke(damage, knockback);
        }
        //피격 시 무적 처리 메서드
        IEnumerator InvincilbilityTimer() {
            invincibilityTimer = invincibilityDuration;
            while (invincibilityTimer > 0f) {
                invincibilityTimer -= Time.deltaTime;
                yield return null;
            }
            invincibilityTimer = 0f;
            animator.ResetTrigger(AnimationString.gethit);
        }
        //사망 처리 메소드
        private void Die() {
            Destroy(gameObject);
            IsDead = true;
            animator.SetBool(AnimationString.isdead, true);
        }
        //체력 회복 메소드
        public void Heal(float healAmount) {
            currentHealth += healAmount;
            if (currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }
        }
        #endregion
    }
}

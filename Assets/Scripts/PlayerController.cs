using UnityEngine;
using UnityEngine.InputSystem;

namespace FlatformerTest {
    //플레이어 이동을 제어하는 클래스
    public class PlayerController : MonoBehaviour {
        #region Variables
        Rigidbody2D rb2d;

        //걷는 속도와 달리는 속도
        float walkSpeed = 5f;
        float runSpeed = 10f;

        //이동에 사용할 입력값
        Vector2 inputVector = new Vector2(0, 0);
        #endregion

        private void Start() {
            rb2d = GetComponent<Rigidbody2D>();
        }

        public void OnMove(InputAction.CallbackContext context) {
            //입력값을 받아옴
            inputVector = context.ReadValue<Vector2>();
        }

        private void FixedUpdate() {
            rb2d.linearVelocity = new Vector2(inputVector.x * walkSpeed, 0f);
        }
    }
}


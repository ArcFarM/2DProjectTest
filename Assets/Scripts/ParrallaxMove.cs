using UnityEngine;

namespace FlatformerTest {
    public class ParrallaxMove : MonoBehaviour {
        #region variables
        //플레이어와 카메라의 위치
        [SerializeField] Transform playerPos;
        [SerializeField] Camera cam;

        //배경의 처음 위치와 처음 z값
        Vector2 startPos;
        float startZ;
        #endregion

        #region Property
        //카메라의 이동 거리
        Vector2 camMoveDist => startPos - (Vector2)cam.transform.position;
        //플레이어와의 거리
        float zDist => transform.position.z - playerPos.position.z;
        //배경 위치에 따른 플레이어와의 거리
        float clippingPlane => cam.transform.position.z + ( (zDist > 0) ? cam.farClipPlane : cam.nearClipPlane);

        //플레이어 이동에 따른 배경 이동 거리 비율
        float parallaxRatio => Mathf.Abs(zDist / clippingPlane);
        #endregion

        #region Unity Event Method
        private void Start() {
            startZ = transform.position.z;
            startPos = new Vector2(transform.position.x, transform.position.y);
        }

        private void Update() {
            Vector2 newPos = startPos + camMoveDist * parallaxRatio;
            transform.position = new Vector3(newPos.x, newPos.y, startZ);
        }
        #endregion
    }

}

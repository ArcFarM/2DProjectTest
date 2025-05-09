using UnityEngine;
using System.Collections.Generic;
namespace FlatformerTest {
    public class Detector : MonoBehaviour
    {
        #region Variables
        //감지기와 접촉한 모든 대상
        public List<Collider2D> contactedList = new List<Collider2D>();
        //감지 대상 레이어
        [SerializeField] LayerMask targetLayer;
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter2D(Collider2D collision) {
            if(!contactedList.Contains(collision)) contactedList.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (contactedList.Contains(collision)) contactedList.Remove(collision);
        }
        #endregion
    }
}

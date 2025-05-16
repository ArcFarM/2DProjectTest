using Unity.Collections;
using UnityEngine;

namespace FlatformerTest {

    public class AnimationString {
        //이동 관련
        public static string runinput = "RunInput";
        public static string walkinput = "WalkInput";
        public static string jumpinput = "JumpTrigger";
        //Y축 이동 속도
        public static string yvelocity = "YVelocity";
        //이동 판정 관련
        public static string isground = "IsGround";
        public static string isceiling = "IsCeiling";
        public static string iswall = "IsWall";
        public static string cannotmove = "CannotMove";
        //공격 관련
        public static string groundattack = "GroundAttack";
        //플레이어 피격
        public static string gethit = "GetHit";
        public static string playerhitdelay = "HitCoolDown";
        public static string lockspeed = "LockSpeed";
        //플레이어 사망
        public static string isdead = "IsDead";

        #region Enemy
        //적이 플레이어 발견
        public static string findplayer = "FindPlayer";
        //적이 공격
        public static string enemyattack = "EnemyAttack";
        public static string enemyatkcd = "AttackCoolDown";
        #endregion
    }
}

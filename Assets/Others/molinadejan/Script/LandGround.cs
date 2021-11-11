using UnityEngine;

namespace molinadejan
{
    public class LandGround : StateMachineBehaviour
    {
        public float transition;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime > transition)
            {
                animator.SetBool("isLand", false);
                animator.SetBool("isJump", false);
            }
        }
    }
}

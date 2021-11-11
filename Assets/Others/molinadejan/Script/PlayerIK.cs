using UnityEngine;

namespace molinadejan
{
    public class PlayerIK : MonoBehaviour
    {
        [Range(0, 1)]
        public float posWeight = 1;

        //[Range(0, 1)]
        //public float rotWeigth = 1;

        public Transform leftHandTarget;
        public Transform rightHandTarget;

        public GameObject testGunObj;

        private int mode = 0;
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            testGunObj.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                testGunObj.SetActive(false);
                mode = 0;
                anim.SetLayerWeight(1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                testGunObj.SetActive(true);
                mode = 1;
                anim.SetLayerWeight(1, 1);
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            switch (mode)
            {
                case 0:
                    break;

                case 1:
                    SetRifleGrip();
                    break;
            }
        }
        
        private void SetRifleGrip()
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, posWeight);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, posWeight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
        }
    }
}

using K_Space;
using UnityEngine;

namespace molinadejan
{
    public class PlayerLookIK : MonoBehaviour
    {
        [Range(0, 1)]
        public float headWeight = 1.0f;

        [Range(0, 1)]
        public float bodyWeight = 0.5f;

        [Range(0, 1)]
        public float altHeadWeight = 0.8f;

        [Range(0, 1)]
        public float altBodyWeight = 0.0f;

        public Transform target;

        private Player_Camera pCam;
        private Animator anim;

        private float currentWeight;

        private float initHeadWeight;
        private float initBodyWeight;

        private bool isCamRotByMouse = false;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            pCam = GetComponent<Player_Camera>();
        }

        private void Start()
        {
            initHeadWeight = headWeight;
            initBodyWeight = bodyWeight;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.C))
                isCamRotByMouse = true;
            else
                isCamRotByMouse = false;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (!isCamRotByMouse)
                SetLookAt();
            else
                ResetLookAt();
        }

        private void SetLookAt()
        {
            bodyWeight = Mathf.Lerp(bodyWeight, initBodyWeight, 0.1f);
            headWeight = Mathf.Lerp(headWeight, initHeadWeight, 0.1f);
            
            anim.SetLookAtWeight(1, bodyWeight, headWeight);
            anim.SetLookAtPosition(target.position);
        }

        private void ResetLookAt()
        {
            bodyWeight = Mathf.Lerp(bodyWeight, altBodyWeight, 0.1f);
            headWeight = Mathf.Lerp(headWeight, altHeadWeight, 0.1f);

            anim.SetLookAtWeight(1, bodyWeight, headWeight);
            anim.SetLookAtPosition(target.position);
        }
    }
}

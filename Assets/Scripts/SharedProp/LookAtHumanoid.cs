using UnityEngine;

public class LookAtHumanoid : MonoBehaviour
{
    [Tooltip("If true, it will look specifically at the head bone of the humanoid avatar.")]
    public bool lookAtHead = true;
     
    [Tooltip("How fast to rotate towards the target. Set to 0 for instant snap look.")]
    public float rotationSpeed = 1f;

    private Transform _targetTransform;

    void Update()
    {
        if (_targetTransform == null)
        {
            FindTarget();
        }

        if (_targetTransform != null)
        {
            if (rotationSpeed > 0)
            {
                Vector3 direction = _targetTransform.position - transform.position;
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
            }
            else
            {
                transform.LookAt(_targetTransform);
            }
        }
    }

    void FindTarget()
    {
        // Try not to spam FindObjectsOfType every single frame if no humanoid exists.
        if (Time.frameCount % 30 != 0) return;

        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (Animator anim in animators)
        {
            if (anim.isHuman)
            {
                if (lookAtHead)
                {
                    Transform head = anim.GetBoneTransform(HumanBodyBones.Head);
                    if (head != null)
                    {
                        _targetTransform = head;
                        return;
                    }
                }
                
                // Fallback to to the animator's root transform
                _targetTransform = anim.transform;
                return;
            }
        }
    }
}

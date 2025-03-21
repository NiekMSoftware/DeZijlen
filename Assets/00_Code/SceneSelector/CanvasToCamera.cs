using UnityEngine;

namespace DeZijlen
{
    public class CanvasToCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float distanceFromPlayer = 2.0f;
        [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);

        // Makes sure the target isn't null and makes the target follow the targetPosition.
        private void LateUpdate()
        {
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("UI_Target").transform;
            }
            else
            {
                Vector3 targetPosition = target.position + target.forward * distanceFromPlayer + offset;
                transform.position = targetPosition;

                Vector3 lookDirection = transform.position - target.position;
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}

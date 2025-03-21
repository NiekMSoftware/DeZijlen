using UnityEngine;

namespace DeZijlen
{
    public class CanvasToCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void LateUpdate()
        {
            if (target != null)
                transform.position = target.position;

            if (target == null)
            {
                transform.position = GameObject.FindGameObjectWithTag("UI_Target").transform.position;
            }
        }
    }
}

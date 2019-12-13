using UnityEngine;

namespace SlimeCamera
{
    public class CameraFollow : MonoBehaviour
    {
        public Vector2 Offset = new Vector2(0, 3);
        private Vector3 _offset;

        public float speed = 3.5f;

        // Dolly work
        public Transform[] DollyPoints;

        private int currentDollyIndex;

        private Slime.SlimeManager _slimeManager;

        private Transform mainSlime
        {
            get
            {
                return _slimeManager.GetActiveSlime().transform;
            }
        }

        private void Start()
        {
            _offset = new Vector3(Offset.x, Offset.y, transform.position.z);

            _slimeManager = FindObjectOfType<Slime.SlimeManager>();
        }

        private void Update()
        {
            var targetPos = transform.position;
            var activeSlime = mainSlime.position;

            if (DollyPoints == null || DollyPoints.Length <= 0)
            {
                // Follow player
                targetPos = activeSlime + _offset;
            }
            else
            {
                // Follow Dolly
                for (int i = 0; i < DollyPoints.Length; i++)
                {
                    if (Vector2.Distance(activeSlime, DollyPoints[i].position) < Vector2.Distance(activeSlime, DollyPoints[currentDollyIndex].position))
                    {
                        // Set new index and target pos.
                        currentDollyIndex = i;
                    }
                }

                targetPos = (Vector3)DollyPoints[currentDollyIndex].position + _offset;
            }

            transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            // Draw the dolly if we have one
            if (DollyPoints == null || DollyPoints.Length <= 0) return;

            Gizmos.color = Color.green;
            foreach (var pointObject in DollyPoints)
            {
                Gizmos.DrawWireCube(pointObject.position, Vector3.one * 0.35f);
            }
        }
    }
}
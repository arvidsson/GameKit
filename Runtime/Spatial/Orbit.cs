using UnityEngine;

namespace GameKit.Spatial
{
    [System.Serializable]
    public class Orbit
    {
        [SerializeField] private Transform center;
        [SerializeField] private float radiusX;
        [SerializeField] private float radiusY;
        [SerializeField] private float period;
        [SerializeField] private float progress;
        private float speed => 1f / period;

        public Orbit() { }

        public Orbit(Transform center, float radiusX, float radiusY, float period)
        {
            this.center = center;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
            this.period = period;
            progress = 0f;
        }

        public Orbit(Transform center, float radius, float period) : this(center, radius, radius, period) { }

        public void SetRandomProgress()
        {
            progress = Random.Range(0f, 1f);
        }

        public void Update()
        {
            progress += Time.deltaTime * speed;
            progress %= 1f;
        }

        public Vector3 GetPosition()
        {
            return Evaluate(progress) + center.position;
        }

        public Vector3[] GetPositions(int segments)
        {
            Vector3[] positions = new Vector3[segments + 1];

            for (int i = 0; i < segments; i++)
            {
                float progress = (float)i / segments;
                positions[i] = Evaluate(progress);
            }

            positions[segments] = positions[0];

            return positions;
        }

        private Vector3 Evaluate(float progress)
        {
            float angle = Mathf.Deg2Rad * 360 * progress;
            float x = Mathf.Sin(angle) * radiusX;
            float y = Mathf.Cos(angle) * radiusY;
            return new Vector3(x, 0f, y);
        }
    }
}
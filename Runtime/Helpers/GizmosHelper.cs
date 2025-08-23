using UnityEngine;

namespace GameKit
{
    /// <summary>
    /// Helper class for drawing useful gizmos.
    /// </summary>
    public static class GizmosHelper
    {
        /// <summary>
        /// Draws a gizmo circle in 2D.
        /// </summary>
        /// <param name="center">Center of the circle.</param>
        /// <param name="radius">Radius of the circle.</param>
        /// <param name="segments">Number of line segments (higher = smoother circle).</param>
        /// <param name="normal">Normal vector of the circle's plane (defaults to Vector3.forward).</param>
        public static void DrawGizmoCircle(Vector3 center, float radius, int segments = 64, Vector3? normal = null)
        {
            if (segments < 3) segments = 3; // need at least a triangle
            Vector3 n = normal ?? Vector3.forward;

            // Create orthogonal vectors in the circle's plane
            Vector3 v1 = Vector3.Cross(n, Vector3.up);
            if (v1.sqrMagnitude < 0.001f) v1 = Vector3.Cross(n, Vector3.right);
            v1.Normalize();
            Vector3 v2 = Vector3.Cross(n, v1);

            float angleStep = 2f * Mathf.PI / segments;

            Vector3 prevPoint = center + v1 * radius;
            for (int i = 1; i <= segments; i++)
            {
                float angle = i * angleStep;
                Vector3 nextPoint = center + (Mathf.Cos(angle) * v1 + Mathf.Sin(angle) * v2) * radius;
                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
        }

        /// <summary>
        /// Draws a gizmo sphere.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="radius"></param>
        public static void DrawGizmoSphere(Vector3 pos, float radius)
        {
            Quaternion rot = Quaternion.Euler(-90.0f, 0.0f, 0.0f);

            int alphaSteps = 8;
            int betaSteps = 16;

            float deltaAlpha = Mathf.PI / alphaSteps;
            float deltaBeta = 2.0f * Mathf.PI / betaSteps;

            for (int a = 0; a < alphaSteps; a++)
            {
                for (int b = 0; b < betaSteps; b++)
                {
                    float alpha = a * deltaAlpha;
                    float beta = b * deltaBeta;

                    Vector3 p1 = pos + rot * MathHelper.GetSphericalPoint(alpha, beta, radius);
                    Vector3 p2 = pos + rot * MathHelper.GetSphericalPoint(alpha + deltaAlpha, beta, radius);
                    Vector3 p3 = pos + rot * MathHelper.GetSphericalPoint(alpha + deltaAlpha, beta - deltaBeta, radius);

                    UnityEngine.Gizmos.DrawLine(p1, p2);
                    UnityEngine.Gizmos.DrawLine(p2, p3);
                }
            }
        }
    }
}
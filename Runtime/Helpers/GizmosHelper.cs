using UnityEngine;

namespace GameKit
{
    /// <summary>
    /// Helper class for drawing useful gizmos.
    /// </summary>
    public static class GizmosHelper
    {
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
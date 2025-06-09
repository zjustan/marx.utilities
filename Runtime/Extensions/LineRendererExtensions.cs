using System;
using UnityEngine;

namespace Marx.Utilities {
    
    public static class LineRendererExtensions {

        /// <summary>
        /// Retrieves the position of a specific segment in the LineRenderer based on the provided index.
        /// </summary>
        /// <param name="lineRenderer">The LineRenderer instance from which to retrieve the position.</param>
        /// <param name="index">The index of the position to retrieve. This supports advanced indexing features such as negative indexing.</param>
        /// <returns>The position of the specified segment in the LineRenderer as a Vector3.</returns>
        public static Vector3 GetPosition(this LineRenderer lineRenderer, Index index) {
            return lineRenderer.GetPosition(index.GetOffset(lineRenderer.positionCount));
        }
    }
}
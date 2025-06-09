using UnityEngine;

namespace Marx.Utilities
{
    public static class RectExtensions
    {
        /// <summary>
        /// Sets the x-coordinate of the Rect to the specified value and returns the modified Rect.
        /// </summary>
        /// <param name="rect">The original Rect to modify.</param>
        /// <param name="x">The new x-coordinate value.</param>
        /// <returns>A new Rect instance with the modified x-coordinate.</returns>
        public static Rect SetX(this Rect rect, float x) {
            Rect result = new(rect) {
                x = x
            };
            return result;
        }

        /// <summary>
        /// Returns a new Rect with its y-coordinate set to the specified value.
        /// </summary>
        /// <param name="rect">The original Rect to modify.</param>
        /// <param name="y">The new y-coordinate value.</param>
        /// <returns>A new Rect with the modified y-coordinate.</returns>
        public static Rect SetY(this Rect rect, float y) {
            Rect result = new(rect) {
                y = y
            };
            return result;
        }

        /// <summary>
        /// Sets the width of the Rect to the specified value and returns the modified Rect.
        /// </summary>
        /// <param name="rect">The original Rect to modify.</param>
        /// <param name="width">The new width value.</param>
        /// <returns>A new Rect instance with the modified width.</returns>
        public static Rect SetWidth(this Rect rect, float width) {
            Rect result = new(rect) {
                width = width
            };
            return result;
        }

        /// <summary>
        /// Sets the height of the Rect to the specified value and returns the modified Rect.
        /// </summary>
        /// <param name="rect">The original Rect to modify.</param>
        /// <param name="height">The new height value.</param>
        /// <returns>A new Rect instance with the modified height.</returns>
        public static Rect SetHeight(this Rect rect, float height) {
            Rect result = new(rect) {
                height = height
            };
            return result;
        }

        /// <summary>
        /// Shifts the x-coordinate of the Rect by the specified amount and returns the modified Rect.
        /// </summary>
        /// <param name="rect">The original Rect to modify.</param>
        /// <param name="amountX">The amount to shift the x-coordinate.</param>
        /// <returns>A new Rect instance with the shifted x-coordinate.</returns>
        public static Rect ShiftX(this Rect rect, float amountX) {
            Rect result = new(rect) {
                x = rect.x + amountX
            };
            return result;
        }

        /// <summary>
        /// Adjusts the x-coordinate of the Rect by a specified amount and reduces its width by the same amount, returning the modified Rect.
        /// </summary>
        /// <param name="rect">The original Rect to modify.</param>
        /// <param name="amountX">The amount by which to shift the x-coordinate and reduce the width.</param>
        /// <returns>A new Rect instance with the adjusted x-coordinate and reduced width.</returns>
        public static Rect ShiftAndResizeX(this Rect rect, float amountX) {
            Rect result = new(rect) {
                x = rect.x + amountX,
                width = rect.width - amountX,
            };
            return result;
        }

        /// <summary>
        /// Shifts the y-coordinate of the Rect by the specified amount and returns the modified Rect.
        /// </summary>
        /// <param name="rect">The original Rect to modify.</param>
        /// <param name="amountY">The amount to add to the current y-coordinate.</param>
        /// <returns>A new Rect instance with the shifted y-coordinate.</returns>
        public static Rect ShiftY(this Rect rect, float amountY) {
            Rect result = new(rect) {
                y = rect.y + amountY
            };
            return result;
        }

        /// <summary>
        /// Shifts the y-coordinate of the Rect by the specified amount and adjusts its height accordingly, returning the modified Rect.
        /// </summary>
        /// <param name="rect">The original Rect to modify.</param>
        /// <param name="amountY">The amount to shift the y-coordinate. A positive value shifts the Rect down, while a negative value shifts it up.</param>
        /// <returns>A new Rect instance with the modified y-coordinate and adjusted height.</returns>
        public static Rect ShiftAndResizeY(this Rect rect, float amountY) {
            Rect result = new(rect) {
                y = rect.y + amountY,
                height = rect.height - amountY,
            };
            return result;
        }
    }
}
using UnityEngine;

namespace Marx.Utilities
{
    public static class RectExtensions
    {
        public static Rect SetX(this Rect rect, float x)
        {
            Rect result = new(rect)
            {
                x = x
            };
            return result;
        }

        public static Rect SetY(this Rect rect, float y)
        {
            Rect result = new(rect)
            {
                y = y
            };
            return result;
        }

        public static Rect SetWidth(this Rect rect, float width)
        {
            Rect result = new(rect)
            {
                width = width
            };
            return result;
        }

        public static Rect SetHeight(this Rect rect, float height)
        {
            Rect result = new(rect)
            {
                height = height
            };
            return result;
        }

        public static Rect ShiftX(this Rect rect, float amountX)
        {
            Rect result = new(rect)
            {
                x = rect.x + amountX
            };
            return result;
        }

        public static Rect ShiftAndResizeX(this Rect rect, float amountX)
        {
            Rect result = new(rect)
            {
                x = rect.x + amountX,
                width = rect.width - amountX,
            };
            return result;
        }

        public static Rect ShiftY(this Rect rect, float amountY)
        {
            Rect result = new(rect)
            {
                y = rect.y + amountY
            };
            return result;
        }

        public static Rect ShiftAndResizeY(this Rect rect, float amountY)
        {
            Rect result = new(rect)
            {
                y = rect.y + amountY,
                height = rect.height - amountY,
            };
            return result;
        }
    }
}
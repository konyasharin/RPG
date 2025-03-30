using System;
using Core.Enums;
using UnityEngine;

namespace Core.Utils
{
    public static class AxisUtils
    {
        public static float GetValueFromVectorByAxis(Vector2 vector, Axis axis)
        {
            return axis switch
            {
                Axis.Horizontal => vector.x,
                Axis.Vertical => vector.y,
                _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
            };
        }

        public static Axis GetOppositeAxis(Axis axis)
        {
            return axis switch
            {
                Axis.Horizontal => Axis.Vertical,
                Axis.Vertical => Axis.Horizontal,
                _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
            };
        }
    }
}
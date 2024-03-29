using Property.Origins;
using System;
using UnityEngine;

namespace Property.ObservableRangeProperty
{
    [Serializable]
    public class IntObsRangeProperty : OriginObservableRangeProperty<int>
    {
        protected override int ClampValue(int value, int min, int max)
        {
            return Mathf.Clamp(value, min, max);
        }
    }

    [Serializable]
    public class FloatObsRangeProperty : OriginObservableRangeProperty<float>
    {
        protected override float ClampValue(float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }
    }
}

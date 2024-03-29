using System;
using Property.Origins;
using UnityEngine;

namespace Property.ObservableProperty
{
    [Serializable]
    public class IntObsProperty : ObservableProperty<int> { }

    [Serializable]
    public class FloatObsProperty : ObservableProperty<float> { }

    [Serializable]
    public class BoolObsProperty : ObservableProperty<bool> { }

    [Serializable]
    public class StringObsProperty : ObservableProperty<string> { }

    [Serializable]
    public class Vector2ObsProperty : ObservableProperty<Vector2> { }

    [Serializable]
    public class Vector2IntObsProperty : ObservableProperty<Vector2Int> { }
}

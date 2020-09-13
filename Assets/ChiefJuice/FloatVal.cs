using UnityEngine;

namespace ChiefJuice
{
    [CreateAssetMenu]
    public class FloatVal : ScriptableObject
    {
        [SerializeField] private float value;

        public static implicit operator float(FloatVal val)
        {
            return val.value;
        }
    }
}

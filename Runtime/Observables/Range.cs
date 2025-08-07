using System;

namespace GameKit.Observables
{

    public class Range<T> where T : IComparable<T>, IConvertible
    {
        T min;
        T max;

        public Action<T, T> OnValueChanged = delegate { };

        public T Min
        {
            get => min;
            set
            {
                if (!Equals(min, value))
                {
                    min = value;
                    OnValueChanged.Invoke(min, max);
                }
            }
        }

        public T Max
        {
            get => max;
            set
            {
                if (!Equals(max, value))
                {
                    max = value;
                    OnValueChanged.Invoke(min, max);
                }
            }
        }

        public Range(T initialMinValue = default, T initialMaxValue = default)
        {
            min = initialMinValue;
            max = initialMaxValue;
        }

        public bool IsWithinRange(T value)
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        public float GetPercentage(T value)
        {
            float min = Convert.ToSingle(Min);
            float max = Convert.ToSingle(Max);
            float current = Convert.ToSingle(value);
            if (max == min) return 0;
            return ((current - min) / (max - min)) * 100;
        }

        public T GetRandomValue()
        {
            return (T)(object)UnityEngine.Random.Range(Convert.ToSingle(min), Convert.ToSingle(max));
        }
    }

}
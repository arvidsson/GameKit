using System;

namespace GameKit.Observables
{

    public class Value<T>
    {
        private T current;

        public Action<T> OnValueChanged { get; set; } = delegate { };

        public T Current
        {
            get => current;
            set
            {
                if (!Equals(current, value))
                {
                    current = value;
                    OnValueChanged.Invoke(current);
                }
            }
        }

        public Value(T initialValue)
        {
            current = initialValue;
        }
    }

}
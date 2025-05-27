using System;

namespace karin
{
    [Serializable]
    public class ChangingValue<T> where T : IComparable
    {
        public T Value 
        {
            get => _value;
            set
            {
                if (_value.Equals(value)) return;

                _value = value;
                OnValueChanged?.Invoke();
            }
        }
        private T _value;
        public event Action OnValueChanged;

        public void Update() => OnValueChanged?.Invoke();
    }
}
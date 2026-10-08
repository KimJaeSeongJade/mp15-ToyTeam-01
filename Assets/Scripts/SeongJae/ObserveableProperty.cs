using System;
public class ObserveableProperty<T>
{
    private T _value;
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }

    public event Action<T> OnValueChanged;
}

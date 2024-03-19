using Code.Scripts.Audio;

namespace Code.Scripts.Utils
{
    public sealed class Box<T> : IReadOnlyBox<T>
    {
        public T Value { get; set; }

        public Box(T value)
        {
            Value = value;
        }
    }
    public static class Box
    {
        public static Box<T> From<T>(T value)
        {
            return new Box<T>(value);
        }
    }
}
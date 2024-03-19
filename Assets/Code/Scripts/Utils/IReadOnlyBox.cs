namespace Code.Scripts.Audio
{
    public interface IReadOnlyBox<out T>
    {
        public T Value { get; }
    }
}
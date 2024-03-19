namespace Code.Scripts.Utils
{
    public interface IReadOnlyBox<out T>
    {
        public T Value { get; }
    }
}
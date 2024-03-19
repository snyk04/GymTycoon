namespace Code.Scripts.Save.Models
{
    public struct SerializableVector3
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public SerializableVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
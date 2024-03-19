using UnityEngine;

namespace Code.Scripts.Utils
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

        public static implicit operator Vector3(SerializableVector3 vector3)
        {
            return new Vector3(vector3.X, vector3.Y, vector3.Z);
        }
        
        public static implicit operator SerializableVector3(Vector3 vector3)
        {
            return new SerializableVector3(vector3.x, vector3.y, vector3.z);
        }
    }
}
using Code.Scripts.Save.Models;
using UnityEngine;

namespace Code.Scripts.Utils
{
    public static class Vector3Extensions
    {
        public static SerializableVector3 ToSerializable(this Vector3 vector3)
        {
            return new SerializableVector3(vector3.x, vector3.y, vector3.z);
        }
        
        public static Vector3 ToVector3(this SerializableVector3 vector3)
        {
            return new Vector3(vector3.X, vector3.Y, vector3.Z);
        }
    }
}
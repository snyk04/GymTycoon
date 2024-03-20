using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Scripts.Utils
{
    public static class Lerper
    {
        public static async Task To(Color startValue, Action<Color> setter, Color goal, float duration, CancellationToken cancellationToken)
        {
            await To(value => setter(Color.Lerp(startValue, goal, value)), duration, cancellationToken);
        }
        
        public static async Task To(Vector3 startValue, Action<Vector3> setter, Vector3 goal, float duration, CancellationToken cancellationToken)
        {
            await To(value => setter(Vector3.Lerp(startValue, goal, value)), duration, cancellationToken);
        }
        
        public static async Task To(Action<float> setter, float duration, CancellationToken cancellationToken)
        {
            var startTime = Time.time;
            float progress = 0;
            while (progress < 1 && !cancellationToken.IsCancellationRequested)
            {
                progress = Mathf.InverseLerp(startTime, startTime + duration, Time.time);
                setter(progress);
                await Task.Yield();
            }
        }
    }
}
using UnityEngine;

namespace Finark.Utils
{
    public class Utils
    {

        public static Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

    }
}

using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public class Utils
    {
        public static IEnumerator DoDelayed(float delayTime, Action action)
        {
            yield return delayTime != 0f ? new WaitForSeconds(delayTime) : null;

            if (action != null) action();
        }
    }
}
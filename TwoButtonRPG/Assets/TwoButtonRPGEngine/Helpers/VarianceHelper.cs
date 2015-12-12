using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.Helpers
{
    class VarianceHelper
    {
        public static int GetResult(int baseNumber, float variance)
        {
            return Mathf.RoundToInt(Random.Range(baseNumber - baseNumber*variance, baseNumber + baseNumber*variance));
        }
    }
}

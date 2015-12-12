using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.TwoButtonRPGEngine.DamageSystem
{
    public class DamageResult
    {
        public int DamageTaken;

        public DamageResult(int damageTaken)
        {
            DamageTaken = damageTaken;
        }
    }
}

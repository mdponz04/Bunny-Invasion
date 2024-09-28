using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthNamespace
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Transform bar;
        private HealthSystem healthSystem;
        private float baseLocalScale = 3;

        public void Setup(HealthSystem healthSystem)
        {
            this.healthSystem = healthSystem;
            healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        }

        private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
        {
            bar.localScale = new Vector3(baseLocalScale * healthSystem.GetHealthPercent(), .4f, 1f);
        }
    }
}


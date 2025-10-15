using Source.Gadgeteers.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Gadgeteers.UI
{
    public class StatusPanel : MonoBehaviour
    {
        [SerializeField]
        private Image _healthGauge;
        [SerializeField]
        private Image _energyGauge;
        [SerializeField]
        private StatController _source;

        private void Update()
        {
            _healthGauge.fillAmount = 0.16f + _source.SafeGet("health") / _source.SafeGet("max_health") * 0.675f;
            _energyGauge.fillAmount = 0.16f + _source.SafeGet("energy") / _source.SafeGet("max_energy") * 0.675f;
        }
    }
}
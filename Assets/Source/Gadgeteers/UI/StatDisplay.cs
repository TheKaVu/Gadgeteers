using Source.Gadgeteers.Game;
using TMPro;
using UnityEngine;

namespace Source.Gadgeteers.UI
{
    public class StatDisplay : MonoBehaviour
    {
        [SerializeField]
        private StatController _statCtrl;
        
        [SerializeField]
        private TMP_Text _text;

        private void Update()
        {
            _text.text = "<style=name>Statistics:</style>";
            foreach (var stat in _statCtrl.GetStats())
            {
                if(stat.IsDynamic) continue;
                var final = _statCtrl.Compute(stat);
                var raw = _statCtrl.Compute(stat, true);
                _text.text += $"\n<style={stat.Key}></style><style=hl>{final}</style>" +
                              $"<style=normal> ({raw} base + {final - raw} bonus)</style>";
            }
        }
    }
}
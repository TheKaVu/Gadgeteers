using Source.Gadgeteers.Game;
using Source.Gadgeteers.Game.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Source.Gadgeteers.UI
{
    [Prefab("UI/","EffectUnitView")]
    public class EffectUnitView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image _effectIcon;
        [SerializeField]
        private Image _borderChart;
        [SerializeField]
        private TMP_Text _stack;

        private EffectUnit _unit;

        public void Redraw(EffectUnit unit)
        {
            gameObject.SetActive(unit.IsVisible);
            _stack.enabled = unit.IsStackVisible;
            _stack.text = unit.Stack.ToString();
            _effectIcon.sprite = unit.Icon;
        }

        private void Update()
        {
            _borderChart.fillAmount = _unit.Score.Duration >= 0 ? _unit.Score.Progress : 0f;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Tooltip.Instance.ShowFor(_unit);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tooltip.Instance.SetVisible(false);
        }

        public void AssignTo(EffectUnit unit)
        {
            _unit = unit;
        }
    }
}
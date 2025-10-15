using System;
using TMPro;
using UnityEngine;

namespace Source.Gadgeteers.UI
{
    public class Tooltip : PopUp
    {
        public static Tooltip Instance { get; private set; }

        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private Vector2 _offset;
        
        private RectTransform _rectTransform;
        private ITooltipContext _context;

        public ITooltipContext Context
        {
            get => _context;
            set
            {
                _context = value;
                _text.SetText(_context != null ? _context.GetContent() : "");
                _text.ForceMeshUpdate();
            }
        }

        private void Awake()
        {
            if(Instance == null) Instance = this;
            else Destroy(this);
            _rectTransform = GetComponent<RectTransform>();
            SetVisible(false);
        }
        
        private void Update()
        {
            try
            {
                transform.position = Input.mousePosition + new Vector3(_offset.x, _offset.y);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void OnEnable()
        {
            _text.ForceMeshUpdate();
            var size = _text.GetRenderedValues(false);
            var m = _text.margin;
            _rectTransform.sizeDelta = new Vector2(size.x + m.x + m.z, size.y + m.y + m.w);
            Update();
        }

        public void ShowFor(ITooltipContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            SetVisible(true);
        }
    }
}
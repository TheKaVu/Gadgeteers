using UnityEngine;

namespace Source.Gadgeteers.UI
{
    public class PopUp : MonoBehaviour
    {
        private void Awake()
        {
            IsVisible = false;
            IsVisible = gameObject.activeSelf;
        }
        
        public bool IsVisible { get; private set; }
        
        public void SetVisible(bool visible)
        {
            IsVisible = visible;
            gameObject.SetActive(visible);
        }

        public void SwitchVisible()
        {
            SetVisible(!IsVisible);
        }
    }
}
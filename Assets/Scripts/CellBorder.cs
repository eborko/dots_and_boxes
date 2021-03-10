using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate void CellBorderActivated(CellBorder sender);
    public class CellBorder : MonoBehaviour
    {
        public Cell FirstCoowner { get; private set; }
        public Cell SeccondCoowner { get; private set; }

        private bool _isVisible;
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                if (_isVisible)
                {
                    if (_sprite != null)
                    {
                        _sprite.color = new Color(ColorPalete.UpColor.r, ColorPalete.UpColor.g, ColorPalete.UpColor.b, 1);
                    }
                    else
                    {
                        _sprite.color = new Color(ColorPalete.UpColor.r, ColorPalete.UpColor.g, ColorPalete.UpColor.b, 0);
                    }
                }
            }
        }

        public void AddCoowner(Cell coowner)
        {
            if (FirstCoowner == null)
            {
                FirstCoowner = coowner;
            }
            else
            {
                SeccondCoowner = coowner;
            }
        }

        public event CellBorderActivated OnActivated;

        public ColorPalete ColorPalete { get; set; }

        private bool _isActivated = false;
        public bool IsActivated
        {
            get
            {
                return _isActivated;
            }
            set
            {
                _isActivated = value;
                if (_isActivated)
                {
                    if (_sprite != null)
                    {
                        _sprite.color = new Color(ColorPalete.ActiveColor.r, ColorPalete.ActiveColor.g, ColorPalete.ActiveColor.b, 1);
                    }
                }
            }
        }

        private SpriteRenderer _sprite;

        public void Start()
        {
            _sprite = this.GetComponent<SpriteRenderer>();

            if (IsActivated)
            {
                _sprite.color = new Color(ColorPalete.ActiveColor.r, ColorPalete.ActiveColor.g, ColorPalete.ActiveColor.b, 1);
            }
            else
            {
                _sprite.color = new Color(ColorPalete.UpColor.r, ColorPalete.UpColor.g, ColorPalete.UpColor.b, 1);
            }
        }

        private void OnMouseOver()
        {
            if (IsActivated) return;

            _sprite.color = new Color(ColorPalete.OverColor.r, ColorPalete.OverColor.g, ColorPalete.OverColor.b, 1);
        }

        public void OnMouseExit()
        {
            if (IsActivated) return;

            _sprite.color = new Color(ColorPalete.UpColor.r, ColorPalete.UpColor.g, ColorPalete.UpColor.b, 1);
        }

        public void OnMouseDown()
        {
            if (IsActivated) return;
            IsActivated = true;
            OnActivated?.Invoke(this);
        }
    }
}

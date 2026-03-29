using System.Collections.Generic;
using UnityEngine;



namespace Rune.UI
{
    public class FocusGroup : FocusGroupItem
    {
        public FocusGroup()
        {
            _focusedItemIndex.OnChange.AddListener(OnChangeFocusedItemIndex);
            _focusedItemIndex.Refresh();
        }

        

        public override void Focus()
        {
            base.Focus();

            GoTo(-1);
        }

        public override void Defocus()
        {
            base.Defocus();

            GoTo(-1);
        }


        public void Prepend(FocusGroupItem item)
        {
            _items.Insert(0, item);
        }

        public void Prepend(IUIFocusable focusable)
        {
            Prepend(new FocusGroupElement { Focusable = focusable });
        }

        public void Append(FocusGroupItem item)
        {
            _items.Add(item);
        }

        public void Append(IUIFocusable focusable)
        {
            Append(new FocusGroupElement { Focusable = focusable });
        }

        public void Clear()
        {
            _items.Clear();
        }
        

        public void Move(int direction)
        {
            if (Utils.Math.IsWithin(_focusedItemIndex.Value, _items.Count))
            {
                if (EdgeMode == EdgeModeType.Clamped)
                {
                    GoTo(Mathf.Clamp(_focusedItemIndex.Value + direction, 0, _items.Count));
                }
                else if (EdgeMode == EdgeModeType.Repeated)
                {
                    GoTo(Utils.Math.PositiveModulo(_focusedItemIndex.Value + direction, _items.Count));
                }
            }
            else
            {
                GoTo(0);
            }
        }

        public void MoveRow(int direction)
        {
            if (Utils.Math.IsWithin(_focusedItemIndex.Value, _items.Count))
            {
                if (EdgeMode == EdgeModeType.Clamped)
                {
                    GoRow(Mathf.Clamp(_focusedItemIndex.Value + direction, 0, _items.Count));
                }
                else if (EdgeMode == EdgeModeType.Repeated)
                {
                    GoRow(Utils.Math.PositiveModulo(_focusedItemIndex.Value + direction, _items.Count));
                }
            }
            else
            {
                GoRow(0);
            }
        }


        public void GoTo(int index)
        {
            _focusedItemIndex.Value = index;
        }

        public void GoTo(int i0, int i1)
        {
            _focusedItemIndex.Value = i0;

            if (CurrentGroup == null) return;

            CurrentGroup.GoTo(i1);
        }

        public void GoTo(Vector2Int position)
        {
            GoTo(position.x, position.y);
        }

        public void GoRow(int row)
        {
            if (CurrentGroup != null)
            {
                int prevCol = CurrentGroup.FocusedItemIndex;

                GoTo(row);

                if (CurrentGroup != null)
                {
                    int currCol = Mathf.Min(prevCol, CurrentGroup.Items.Count - 1);

                    CurrentGroup.GoTo(currCol);
                }
            }
            else
            {
                GoTo(row);
            }
        }

        public void GoPrev()
        {
            GoTo(Utils.Math.PositiveModulo(_focusedItemIndex.Value - 1, _items.Count));
        }

        public void GoNext()
        {
            GoTo(Utils.Math.PositiveModulo(_focusedItemIndex.Value + 1, _items.Count));
        }

        public void GoPrevRow()
        {
            GoRow(Utils.Math.PositiveModulo(_focusedItemIndex.Value - 1, _items.Count));
        }

        public void GoNextRow()
        {
            GoRow(Utils.Math.PositiveModulo(_focusedItemIndex.Value + 1, _items.Count));
        }



        public override IUIFocusable CurrentFocusable
        {
            get => Utils.Math.IsWithin(_focusedItemIndex.Value, _items.Count) ? _items[_focusedItemIndex.Value].CurrentFocusable : null;
        }

        public int ItemCount
        {
            get => Items.Count;
        }



        public EdgeModeType EdgeMode { get; set; } = EdgeModeType.Repeated;



        public int FocusedItemIndex
        {
            get => _focusedItemIndex.Value;
        }

        public List<FocusGroupItem> Items
        {
            get => _items;
        }

        public FocusGroupItem CurrentItem
        {
            get => Utils.Math.IsWithin(_focusedItemIndex.Value, _items.Count) ? _items[_focusedItemIndex.Value] : null;
        }

        public FocusGroup CurrentGroup
        {
            get => CurrentItem as FocusGroup;
        }



        private void OnChangeFocusedItemIndex(int value)
        {
            foreach (var item in _items) item.Defocus();

            if (Utils.Math.IsWithin(value, _items.Count)) _items[value].Focus();
        }



        private readonly Attribute<int> _focusedItemIndex = new(-1);

        private readonly List<FocusGroupItem> _items = new();



        public enum EdgeModeType
        {
            Clamped,
            Repeated,
        }
    }
}
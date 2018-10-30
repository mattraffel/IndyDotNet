using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace IndyDotNetCLI.Views
{
    public abstract class BaseDialog : Window
    {
        protected static int PADDING = 1;
        protected int _selectedButton = 0;
        protected List<Button> _buttons = new List<Button>();

        public BaseDialog(string title, int width = 60, int height = 40) : base(title, PADDING)
        {
            X = Pos.Center();
            Y = Pos.Center();
            Width = width;
            Height = height;
            ColorScheme = Colors.Dialog;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            const int borderWidth = 2;
            int buttonSpace = 0;
            int maxHeight = 0;

            foreach (var b in _buttons)
            {
                buttonSpace += b.Frame.Width + 1;
                maxHeight = Math.Max(maxHeight, b.Frame.Height);
            }

            var start = (Frame.Width - borderWidth - buttonSpace) / 2;

            var y = Frame.Height - borderWidth - maxHeight - 1 - PADDING;
            foreach (var b in _buttons)
            {
                var bf = b.Frame;
                b.Frame = new Rect(start, y, bf.Width, bf.Height);
                start += bf.Width + 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttonId"></param>
        /// <param name="buttonText"></param>
        /// <param name="clickEvent">delegate for the click event, if null, default is used which will close the dialog</param>
        protected void CreateButton(int buttonId, string buttonText, Action clickEvent = null)
        {
            Action clickAction = clickEvent;
            if (null == clickAction)
            {
                clickAction = delegate
                {
                    _selectedButton = buttonId;
                    this.Running = false;
                };
            }

            var btn = new Button(buttonText);
            btn.Clicked += clickAction;
            AddButton(btn);
        }

        protected void AddButton(Button button)
        {
            _buttons.Add(button);
            Add(button);
        }

        protected int ComputeLeftOfCenter(int widthOfWindow, int displayWidth)
        {
            int center = (widthOfWindow / 2);
            int halfOfDisplayWidth = (displayWidth / 2);
            var leftOfCenter = center - halfOfDisplayWidth;
            if (0 >= leftOfCenter) leftOfCenter = 5;

            // widthOfWindow: 60 - halfOfDisplayWidth:6 - leftOfCenter:(
            System.Diagnostics.Debug.WriteLine($"widthOfWindow: {widthOfWindow} - halfOfDisplayWidth:{halfOfDisplayWidth} - leftOfCenter:{leftOfCenter}");

            return leftOfCenter;
        }
    }
}

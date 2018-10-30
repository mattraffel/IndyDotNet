using System;
using System.Collections.Generic;
using Terminal.Gui;

namespace IndyDotNetCLI.Views
{
    public class ListDialog : Window
    {
        private List<Button> _buttons = new List<Button>();
        private static int PADDING = 1;
        private int _selectedButton = 0;

        public ListDialog(string title, string message, System.Collections.IList source, int width = 60, int height = 40) : base(title, PADDING)
        {
            X = Pos.Center();
            Y = Pos.Center();
            Width = width;
            Height = height;
            ColorScheme = Colors.Dialog;

            var ok = new Button(DialogConstants.ButtonTexts[DialogConstants.OK]);
            ok.Clicked += delegate
            {
                _selectedButton = DialogConstants.OK;
                this.Running = false;
            };

            _buttons.Add(ok);
            Add(ok);

            var textLabel = new Label(ComputeLeftOfCenter(width, message.Length), 1, $"{message}");
            Add(textLabel);

            int listViewWidth = width - 8;
            var listViewRect = new Terminal.Gui.Rect()
            {
                X = ComputeLeftOfCenter(width, listViewWidth),
                Y = textLabel.Frame.Y + textLabel.Frame.Height + 1,
                Height = 10,
                Width = listViewWidth
            };

            var listView = new ListView(listViewRect, source)
            {

            };
            Add(listView);

        }

        /// <summary>
        /// Show the edit field dialog
        /// </summary>
        /// <returns>int, the button chosen.  1 for OK, 2 for cancel.  string, user inputted data.</returns>
        public (int, string) Show()
        {
            Application.Run(this);
            return (_selectedButton, string.Empty);
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

        private int ComputeLeftOfCenter(int widthOfWindow, int displayWidth)
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

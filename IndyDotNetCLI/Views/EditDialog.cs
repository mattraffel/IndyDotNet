using System;
using System.Collections.Generic;
using Terminal.Gui;

namespace IndyDotNetCLI.Views
{
    /// <summary>
    /// A generic, single edit field dialog box with OK, cancel buttons.
    /// </summary>
    public class EditDialog : Window
    {
        private static int PADDING = 1;

        private List<Button> _buttons = new List<Button>();
        private TextField _textInput = null;
        private int _selectedButton = 0;
        private string _data = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">Title of window</param>
        /// <param name="message">message appearing in dialog box above the edit field</param>
        /// <param name="width">width of dialog box, default is 60</param>
        /// <param name="height">height of dialog box, default is 10</param>
        public EditDialog(string title, string message, int width = 60, int height = 10) : base(title, PADDING)
        {

            X = Pos.Center();
            Y = Pos.Center();
            Width = width;
            Height = height;
            ColorScheme = Colors.Dialog;

            var ok = new Button("Ok");
            ok.Clicked += delegate
                {
                    _selectedButton = DialogConstants.OK;
                    this.Running = false;
                };

            var cancel = new Button("Cancel");
            cancel.Clicked += delegate
                {
                    _selectedButton = DialogConstants.CANCEL;
                    this.Running = false;
                };

            _buttons.Add(ok);
            _buttons.Add(cancel);

            this.Add(ok);
            this.Add(cancel);

            var textLabel = new Label(ComputeLeftOfCenter(width, message.Length), 1, $"{message}");
            this.Add(textLabel);

            const int EDIT_BOX_WIDTH = 40;
            var editX = ComputeLeftOfCenter(width, EDIT_BOX_WIDTH + 10);
            _textInput = new TextField("")
                {
                    X = editX,
                    Y = Pos.Top(textLabel) + 1,
                    Width = EDIT_BOX_WIDTH
                };

            this.Add(_textInput);

            var browse = new Button("Browse");
            browse.X = editX + EDIT_BOX_WIDTH + 1;
            browse.Y = _textInput.Y;
            browse.Clicked += delegate 
                {
                    FileDialog fileDialog = new FileDialog("Browse", "Pool configuration file", "", "");
                    Application.Run(fileDialog);
                    _textInput.Text = fileDialog.FilePath;
                };

            this.Add(browse);
        }

        /// <summary>
        /// Show the edit field dialog
        /// </summary>
        /// <returns>int, the button chosen.  1 for OK, 2 for cancel.  string, user inputted data.</returns>
        public (int, string) Show() 
        {
            Application.Run(this);
            return (_selectedButton, _textInput.Text.ToString());
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

        public override bool ProcessKey(KeyEvent kb)
        {
            switch (kb.Key)
            {
                case Key.Esc:
                    Running = false;
                    return true;
            }
            return base.ProcessKey(kb);
        }

        private int ComputeLeftOfCenter(int widthOfWindow, int lengthOfText)
        {
            int leftOfCenter = (widthOfWindow / 2) - (lengthOfText / 2);
            if (0 >= leftOfCenter) leftOfCenter = 5;

            System.Diagnostics.Debug.WriteLine($"widthOfWindow: {widthOfWindow} - lengthOfText:{lengthOfText} - leftOfCenter:{leftOfCenter}");

            return leftOfCenter;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using Terminal.Gui;

namespace IndyDotNetCLI.Views
{
    public class ListDialog : BaseDialog
    {
        public ListDialog(string title, string message, System.Collections.IList source, int width = 60, int height = 40) : base(title)
        {
            X = Pos.Center();
            Y = Pos.Center();
            Width = width;
            Height = height;
            ColorScheme = Colors.Dialog;

            CreateButton(DialogConstants.OK, DialogConstants.ButtonTexts[DialogConstants.OK]);
            
            var textLabel = new Label(ComputeLeftOfCenter(width, message.Length), 1, $"{message}");
            Add(textLabel);

            int listViewWidth = width - 8;
            int x = ComputeLeftOfCenter(width, listViewWidth);
            var listViewRect = new Terminal.Gui.Rect()
            {
                X = x,
                Y = textLabel.Frame.Y + textLabel.Frame.Height + 1,
                Height = 10,
                Width = listViewWidth - x
            };

            var listView = new ListView(listViewRect, source)
            {
                //ColorScheme = new ColorScheme()
                //{
                //    Normal = new Terminal.Gui.Attribute(205),
                //    Focus = new Terminal.Gui.Attribute(205),
                //    HotFocus = new Terminal.Gui.Attribute(205),
                //    HotNormal = new Terminal.Gui.Attribute(205)
                //}

            };
            Add(listView);

        }

        /// <summary>
        /// Show the edit field dialog
        /// </summary>
        /// <returns>int, the button chosen.</returns>
        public int Show()
        {
            Application.Run(this);
            return _selectedButton;
        }       
    }
}

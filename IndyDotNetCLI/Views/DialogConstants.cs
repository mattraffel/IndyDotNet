using System;
using System.Collections.Generic;
using System.Text;

namespace IndyDotNetCLI.Views
{
    public static class DialogConstants
    {
        public static int OK = 1;
        public static int CANCEL = 2;

        public static List<string> ButtonTexts;

        static DialogConstants()
        {
            ButtonTexts = new List<string>();
            ButtonTexts.Add("");
            ButtonTexts.Add("OK");
            ButtonTexts.Add("Cancel");
        }
    }
}

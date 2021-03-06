﻿using CoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace X.Extensions.UI
{
    public class ToolbarPositionToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var toolbarPosition = (ExtensionInToolbarPositions)value;

            var positions = "";
            
            if (toolbarPosition.Equals(ExtensionInToolbarPositions.Left)) positions += "left";
            if (toolbarPosition.Equals(ExtensionInToolbarPositions.Top)) positions += "top";
            if (toolbarPosition.Equals(ExtensionInToolbarPositions.Right)) positions += "right";
            if (toolbarPosition.Equals(ExtensionInToolbarPositions.Bottom)) positions += "bottom";
            if (toolbarPosition.Equals(ExtensionInToolbarPositions.BottomFull)) positions += "bottom-full";
            
            if (positions.Length == 0) return "ms-appx:///X.SharedLibs/AppX/X.Extensions.UI/tb-none.png";
            else return "ms-appx:///X.SharedLibs/AppX/X.Extensions.UI/tb-" + positions + ".png";

        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

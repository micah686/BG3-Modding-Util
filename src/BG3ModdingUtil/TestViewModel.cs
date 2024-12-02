using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG3ModdingUtil
{
    public partial class TestViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _textValue = string.Empty;

        [RelayCommand]
        private void SetText()
        {
            TextValue = "HelloWorld";
        }
    }
}

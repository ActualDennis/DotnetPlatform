using Microsoft.Win32;
using NUnitGen.Helpers;
using System.Collections.Generic;
using System.Windows.Input;

namespace NUnitGen {

    /// <summary>
    /// ViewModel for storing information about current chat and messages in chat
    /// </summary>
    public class ApplicationViewModel : BaseViewModel {

        public ApplicationViewModel()
        {
            
        }

        public string MaxFilesToWrite { get; set; }

        public string MaxTasksExecuted { get; set; }

        public string MaxFilesToLoad { get; set; }

        private List<string> LoadedClasses { get; set; }

        public ICommand ExecuteCommand => new RelayCommand(() => Execute(null), null);

        public ICommand AddClassCommand => new RelayCommand(() => AddClass(null), null);


        private void AddClass(object p)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "C# class file(*.cs)|*.cs";
            var result = dialog.ShowDialog();

            if (result == null || result == false)
                return;

            LoadedClasses.Add(dialog.FileName);
        }

        private void Execute(object p)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "C# NUnit test file(*.cs)|*.cs";
            var result = dialog.ShowDialog();

            if (result == null || result == false)
                return;

            var destination = dialog.FileName;

        }

    }






}

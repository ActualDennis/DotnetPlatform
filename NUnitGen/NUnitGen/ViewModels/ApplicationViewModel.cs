using Microsoft.Win32;
using NUnitGen.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace NUnitGen {

    /// <summary>
    /// ViewModel for storing information about current chat and messages in chat
    /// </summary>
    public class ApplicationViewModel : BaseViewModel {

        public ApplicationViewModel()
        {
            pipeLine = new PipeLine();
            LoadedClasses = new List<string>();
            LoadedClassesUI = new ObservableCollection<string>();
        }

        public string MaxFilesToWrite { get; set; }

        public string MaxTasksExecuted { get; set; }

        public string MaxFilesToLoad { get; set; }

        public List<string> LoadedClasses { get; set; }

        public ObservableCollection<string> LoadedClassesUI { get; set; }

        public ICommand ExecuteCommand => new RelayCommand(() => Execute(null), null);

        public ICommand AddClassCommand => new RelayCommand(() => AddClass(null), null);

        private PipeLine pipeLine { get; set; }

        private void AddClass(object p)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "C# class file(*.cs)|*.cs";
            var result = dialog.ShowDialog();

            if (result == null || result == false)
                return;

            LoadedClasses.Add(dialog.FileName);
            LoadedClassesUI.Add(dialog.FileName);
        }

        private void Execute(object p)
        {
            try
            {
                var dialog = new FolderBrowserDialog();
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    return;

                pipeLine.maxFilesToLoad = int.Parse(MaxFilesToLoad) == 0 ? 1 : int.Parse(MaxFilesToLoad);
                pipeLine.maxFilesToWrite = int.Parse(MaxFilesToWrite) == 0 ? 1 : int.Parse(MaxFilesToWrite);
                pipeLine.maxTasksExecuted = int.Parse(MaxTasksExecuted) == 0 ? 1 : int.Parse(MaxTasksExecuted);
                pipeLine.outputFile = dialog.SelectedPath;

                pipeLine.Start(new Stack<string>(LoadedClasses));

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadedClasses = new List<string>();
                LoadedClassesUI = new ObservableCollection<string>();
            }
        }

    }






}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPFMVVM.Models.Decanat;

namespace WPFMVVM.Views.Windows
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CollectionViewSource_Filter(object sender, System.Windows.Data.FilterEventArgs e)
        {
            if (!(e.Item is Group group)) return;
            if (group.Name is null) return;

            var filter_text = GroupFilterTextBox.Text;

            if(filter_text.Length == 0) return;

            if (group.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (group.Description != null && group.Description.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }

        private void GroupFilterTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var collectionViewSource = (CollectionViewSource)textBox.FindResource("GroupsCollection");
            collectionViewSource.View.Refresh();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Autodesk.Revit.Exceptions;
using ArgumentException = System.ArgumentException;
using Button = System.Windows.Forms.Button;
using ListView = System.Windows.Forms.ListView;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Library
{
    public static class NotificationUtils
    {
        public static void ShowMessageBox(this string str)
        {
            MessageBox.Show(str);
        }
        public static void ShowMessageBox(this List<string> str,string title="Test")
        {
            StringBuilder st = new StringBuilder();
            str.ForEach(delegate(string stm) { st.AppendLine(stm);});
            MessageBox.Show(st.ToString(),title);
        }

        public static void ShowMessageBox(this  string[] str)
        {
            ShowMessageBox(str.ToList());
        }

        public static void ShowMessageBox(this object obj, string title=null)
        {
            if (obj == null) throw new ArgumentException("object null");
            List<DataShow> dataShows = new List<DataShow>();
            Type type = obj.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string name = propertyInfo.Name;
                object value = GetValue(propertyInfo, obj);
                dataShows.Add(new DataShow() { Name = name, Value = value.ToString() });

            }
            List<DataShow> shows = dataShows.OrderBy(x => x.Name).ToList();
            Window window = new Window();
            window.Title = title==null?obj.GetType().FullName : title;
            window.Width = 400;
            window.Height = 600;
            window.SizeToContent = SizeToContent.Height;
            window.ResizeMode = ResizeMode.NoResize;
            Grid grid = new Grid();
            RowDefinitionCollection rowDefinitions = grid.RowDefinitions;
            rowDefinitions.Add(new RowDefinition());
            RowDefinition definition = new RowDefinition();
            definition.Height = new GridLength(30);
            rowDefinitions.Add(definition);
            System.Windows.Controls.Button btnClose = new System.Windows.Controls.Button();
            btnClose.IsCancel = true;
            btnClose.Content = "Close";
            btnClose.Height = 29;
            btnClose.Click += new RoutedEventHandler((sender, args) => window.Close());
            StackPanel stackPanel = new StackPanel();
            Grid.SetRow(stackPanel, 1);
            stackPanel.Children.Add(btnClose);
            grid.Children.Add(stackPanel);
            System.Windows.Controls.ListView listView = new System.Windows.Controls.ListView();
            GridViewColumn col1 = new GridViewColumn();
            // col1.Width = ;
            col1.Header = "Name";
            col1.DisplayMemberBinding = new System.Windows.Data.Binding("Name");
            GridViewColumn col2 = new GridViewColumn();
            //col2.Width = 100;
            col2.Header = "Value";
            col2.DisplayMemberBinding = new System.Windows.Data.Binding("Value");
            GridView gridView = new GridView();
            gridView.Columns.Add(col1);
            gridView.Columns.Add(col2);
            listView.View = gridView;
            listView.ItemsSource = shows;
            grid.Children.Add(listView);
            window.Content = grid;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();


        }
        private static object GetValue(PropertyInfo prop, object obj)
        {
            object propValue = null;
            try
            {
                propValue = prop.GetValue(obj, null);
            }
            catch
            {
            }

            if ((propValue != null))
                return propValue;
            return string.Empty;
        }
    }
    public class DataShow
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

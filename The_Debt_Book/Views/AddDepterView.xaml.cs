﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace The_Debt_Book.Views
{
	/// <summary>
	/// Interaction logic for HomeView.xaml
	/// </summary>
	public partial class AddDepterView : UserControl
	{
		public AddDepterView()
		{
			InitializeComponent();
		}

		private void CancelBtn(object sender, RoutedEventArgs e)
		{
		
			MainWindow mainWindow = new MainWindow();
			this.Visibility = Visibility.Hidden;
			mainWindow.Show();
		}
	}
}
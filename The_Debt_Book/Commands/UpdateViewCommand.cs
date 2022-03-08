﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using The_Debt_Book.ViewModels;

namespace The_Debt_Book.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "AddDepter")
            {
                viewModel.SelectedViewModel = new AddDepterViewModel();
            }
            else if (parameter.ToString() == "AddDept")
            {
                viewModel.SelectedViewModel = new AddDeptViewModel();
            }
           
        }
    }
}

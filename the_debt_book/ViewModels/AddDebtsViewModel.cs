using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using the_debt_book.Models;

namespace the_debt_book.ViewModels
{

    public class AddDebtsViewModel : INotifyPropertyChanged
    {
        DebtsModel debtsModel = new DebtsModel();
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
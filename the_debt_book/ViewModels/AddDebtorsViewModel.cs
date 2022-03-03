using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using the_debt_book.Models;

namespace the_debt_book.ViewModels
{
    internal class AddDebtorsViewModel : INotifyPropertyChanged
    {


        DebtorsModel debtorsModel = new DebtorsModel();

        public string FullName {
            get { return debtorsModel.FullName; }
            set {
                if (value != debtorsModel.FullName)
                {
                    debtorsModel.FullName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Debts
        {
            get { return debtorsModel.CalculateDebts(); }
            set {}
        }


        public event PropertyChangedEventHandler? PropertyChanged;


        private void NotifyPropertyChanged([CallerMemberName] string propertyName= null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }
}

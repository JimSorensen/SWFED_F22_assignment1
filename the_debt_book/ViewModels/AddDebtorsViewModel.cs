using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using the_debt_book.Models;

namespace the_debt_book.ViewModels
{

    // BindableBase in Prism implements the InotifyPropertyChanged interfacein a type-safe manner.
    // https://prismlibrary.com/docs/wpf/legacy/Implementing-MVVM.html
    internal class AddDebtorsViewModel : BindableBase
    {


        ObservableCollection<DebtorsModel> debtors = new ObservableCollection<DebtorsModel>();
        DebtsModel debtsModel = new DebtsModel();
        DebtorsModel debtorsModel = new DebtorsModel();



        DelegateCommand _addDebtorCommand;


        public AddDebtorsViewModel()
        {
            List<DebtsModel> debtsList1 = new List<DebtsModel>();
            debtsList1.Add(new DebtsModel()
            {
                DebtsValue = 42,
                LogTime = DateTime.UtcNow
            });

            List<DebtsModel> debtsList2 = new List<DebtsModel>();
            debtsList2.Add(new DebtsModel()
            {
                DebtsValue = 43,
                LogTime = DateTime.UtcNow
            });

            debtors.Add(new DebtorsModel() {
                FullName = "User1",
                Debts = debtsList1

            });

            debtors.Add(new DebtorsModel()
            {
                FullName = "User2",
                Debts = debtsList2

            });


        }


        public DelegateCommand AddDebtorCommand
        {
            get
            {
                return _addDebtorCommand ?? (_addDebtorCommand = new
          DelegateCommand(AddDebtor, AddDebtorCanExecute));
            }
        }


        /// <summary>
        /// the logic. It adds a debtor, to the debtor list. This includes adding a debts lists, 
        /// and adding the first element to that list as the initial value.
        /// </summary>
        public void AddDebtor()
        {
            // creating debts consisting of initial value
            List<DebtsModel> debtsList = new List<DebtsModel>();
            debtsList.Add(new DebtsModel()
            {
                LogTime = DateTime.UtcNow,
                DebtsValue = 42
            });

            DebtorsModel debtor = new DebtorsModel()
            {
                FullName = "Peter",
                Debts = new List<DebtsModel>()
            };


            debtors.Add(debtor);
            //NotifyPropertyChanged("Debtors");
            


        }

        /// <summary>
        /// Should only excute if entered fullname and initial value is not null or not 0.
        /// Also should not exectute if the debtor already exists.
        /// </summary>
        /// <returns> true if the command should exectue, false if not </returns>
        private bool AddDebtorCanExecute()
        {
            //// if Fullname is null or initial value is 0.
            //if (FullName == null || InitialValue == 0){
            //    return false; 
            //}
            //// of if debtor already exists, assuming name is unique...
            //foreach (DebtorsModel debtors in debtors)
            //{
            //    if (debtors.FullName.Equals(FullName))
            //    {
            //        return false;
            //    }
            //}
            return true;
        }


        public int InitialValue {
            get { return debtsModel.DebtsValue; }
            set
            {
                if (value != 0)
                {
                    debtsModel.DebtsValue = value;
                    //NotifyPropertyChanged("InitialValue");
                }
            }
        }

        public string FullName
        {
            get { return debtorsModel.FullName; }
            set
            {
                if (value != null)
                {
                    debtorsModel.FullName = value;
                    //NotifyPropertyChanged("FullName");
                }
            }
        }


        public string SumOfDebts
        {
            get { return debtorsModel.SumOfDebts.ToString(); }
           
        }




        public ObservableCollection<DebtorsModel> Debtors
        {
            get { return debtors; }
            set
            {
                SetProperty(ref debtors, value);
            }
        }


        //public int Debts
        //{
        //    get { return debtorsModel.CalculateDebts(); }
        //    set { }
        //}






        //public event PropertyChangedEventHandler? PropertyChanged;
        //private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}



    }
}

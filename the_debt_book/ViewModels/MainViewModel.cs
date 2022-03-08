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
using System.Windows;
using System.Windows.Input;
using the_debt_book.Models;
using the_debt_book.Views;

namespace the_debt_book.ViewModels
{

    // BindableBase in Prism implements the InotifyPropertyChanged interfacein a type-safe manner.
    // https://prismlibrary.com/docs/wpf/legacy/Implementing-MVVM.html
    internal class MainViewModel : BindableBase
    {
        ObservableCollection<DebtorsModel> debtors = new ObservableCollection<DebtorsModel>();
        DebtsModel debtsModel = new DebtsModel();
        ObservableCollection<DebtsModel> debts = new ObservableCollection<DebtsModel>();
        DebtorsModel debtorsModel = new DebtorsModel();        

        // Constructor Initialize components => might go into Data-repository
        public MainViewModel()
        {
            ObservableCollection<DebtsModel> debtsList1 = new ObservableCollection<DebtsModel>();
            debtsList1.Add(new DebtsModel()
            {
                DebtsValue = 42,
                LogTime = DateTime.UtcNow.ToShortTimeString()
            });

            ObservableCollection<DebtsModel> debtsList2 = new ObservableCollection<DebtsModel>();
            debtsList2.Add(new DebtsModel()
            {
                DebtsValue = 43,
                LogTime = DateTime.UtcNow.ToShortTimeString()
            });

            debtsList2.Add(new DebtsModel()
            {
                DebtsValue = -100,
                LogTime = DateTime.UtcNow.ToShortTimeString()
            });

            ObservableCollection<DebtsModel> debtsList3 = new ObservableCollection<DebtsModel>();
            debtsList3.Add(new DebtsModel()
            {
                DebtsValue = -404,
                LogTime = DateTime.UtcNow.ToShortTimeString()
            });


            debtors.Add(new DebtorsModel()
            {
                FullName = "Adam Vergara",
                Debts = debtsList1

            });

            debtors.Add(new DebtorsModel()
            {
                FullName = "Jim",
                Debts = debtsList2
            });

            debtors.Add(new DebtorsModel()
            {
                FullName = "Maagisha",
                Debts = debtsList3
            });
        }

        public DebtsModel Debt
        {
            get { return debtsModel; }
            set
            {
                SetProperty(ref debtsModel, value);
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

        public DebtorsModel Debtor
        {
            get { return debtorsModel; }
            set { SetProperty(ref debtorsModel, value); }
        }

        int debtorIndex = -1;

        public int DebtorIndex
        {
            get { return debtorIndex; }
            set { SetProperty(ref debtorIndex, value); }
        }


        public ObservableCollection<DebtsModel> Debts
        {
            get { return debtorsModel.Debts; }
            set
            {
                SetProperty(ref debts, value);
            }
        }

        #region Commands
        Window window;
        DelegateCommand _saveDebtorCommand;
        DelegateCommand _addDebtorCommand;
        DelegateCommand _updateDebtsCommand;
        DelegateCommand _closeCommand;

        DelegateCommand _AddNewDebtToExistingUserCommand;

        /// <summary>
        /// this method saves a Debtor.
        /// </summary>
        public DelegateCommand SaveDebtorCommand
        {
            get
            {
                return _saveDebtorCommand ?? (_saveDebtorCommand = new DelegateCommand(SaveDebtor, SaveDebtorCanExecute)); // can always execute. Maybe if the list is somewhat full?!
            }
        }

        public DelegateCommand AddNewDebtToExistingUserCommand
        {
            get
            {
                return _saveDebtorCommand ?? (_saveDebtorCommand = new DelegateCommand(AddNewDebt, () => true)); // can always execute. Maybe if the list is somewhat full?!
            }
        }

        /// <summary>
        /// the logic. It adds a debtor, to the debtor list. This includes adding a debts lists, 
        /// and adding the first element to that list as the initial value.
        /// </summary>
        public void SaveDebtor()
        {
            // creating debts consisting of initial value
            ObservableCollection<DebtsModel> debtsList = new ObservableCollection<DebtsModel>();
            debtsList.Add(new DebtsModel()
            {
                LogTime = DateTime.UtcNow.ToShortTimeString(),
                DebtsValue = debtsModel.DebtsValue
            });

            DebtorsModel debtor = new DebtorsModel()
            {
                FullName = debtorsModel.FullName,
                Debts = debtsList
            };
            DebtorIndex = Debtors.Count - 1;
            MessageBox.Show("New Debtor added" + "\n" + "Name: " + debtor.FullName + "\n" + "Debtsvalue: " + debtsModel.DebtsValue);
            window.Close();
        }


        public void AddNewDebt()
        {
            var NewDebt = new DebtsModel() {
                LogTime = DateTime.UtcNow.ToShortTimeString(),
                DebtsValue = debtsModel.DebtsValue
            };

            Debtors.Where(x => x.FullName == Debtor.FullName).SingleOrDefault().Debts.Add(NewDebt);

            MessageBox.Show("New Debt added" + "\n" + "Name: " + debtorsModel.FullName + "\n" + "Debtsvalue: " + debtsModel.DebtsValue);
            window.Close();
        }

        /// <summary>
        /// Should only excute if entered fullname and initial value is not null or not 0.
        /// Also should not exectute if the debtor already exists.
        /// </summary>
        /// <returns> true if the command should exectue, false if not </returns>
        private bool SaveDebtorCanExecute()
        {
            //if Fullname is null or initial value is 0.
            //if (debtsModel.DebtsValue == 0 || debtorsModel.FullName == null)
            //{
            //    return false;
            //}
            //of if debtor already exists, assuming name is unique...
            foreach (DebtorsModel debtors in debtors)
            {
                if (debtors.FullName.Equals(debtorsModel.FullName))
                {
                    return false;
                }
            }
            return true;
        }

        public DelegateCommand AddDebtorCommand
        {
            get
            {
                return _addDebtorCommand ?? (_addDebtorCommand = new
          DelegateCommand(AddDebtor));
            }
        }


        /// <summary>
        ///  Creates a windwod and add the viewmodes as its datacontext.
        /// </summary>
        public void AddDebtor()
        {

            window = new AddDebtorsWin();
            window.DataContext = this;
            window.Show();
        }


        public DelegateCommand UpdateDebtsCommand
        {
            get
            {
                return _updateDebtsCommand ?? (_updateDebtsCommand = new
          DelegateCommand(UpdateDebts));
            }
        }


        public void UpdateDebts()
        {
            window = new AddDebtsWin();
            window.DataContext = this;
            window.Show();

        }



        /// <summary>
        /// This method closes the window currently open.
        /// </summary>
        public DelegateCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new
          DelegateCommand(Close));
            }
        }

        public void Close()
        {

            window.Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion


    }
}

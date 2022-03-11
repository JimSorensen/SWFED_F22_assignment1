using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
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



            DebtorsModel debtor1 = new DebtorsModel("Adam Vergara", debtsList1);
            DebtorsModel debtor2 = new DebtorsModel("Jim", debtsList2);
            DebtorsModel debtor3 = new DebtorsModel("Maagisha", debtsList3);


            debtors.Add(debtor1);
            debtors.Add(debtor2);
            debtors.Add(debtor3);

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



        public ObservableCollection<DebtsModel> Debts
        {
            get { return debtorsModel.Debts; }
            set
            {
                SetProperty(ref debts, value);
            }
        }

        string newDebtValue = "";
        public string NewDebtValue
        {
            get { return newDebtValue; }
            set
            {
                SetProperty(ref newDebtValue, value);
            }
        }


        string debtsValue = "";

        public string DebtsValue
        {
            get { return debtsValue; }
            set {
                SetProperty(ref debtsValue, value); }
        }

        string fullName = "";
        public string FullName
        {
            get { return fullName; }

            set
            { SetProperty(ref fullName, value); }
        }

        void UpdateSumOfDebts()
        {
            debtorsModel.SumOfDebts = debtorsModel.CalculateDebts();
        }




        #region Commands

        Window window;
        DelegateCommand _saveDebtorCommand;
        DelegateCommand _addDebtorCommand;

        DelegateCommand _updateDebtsCommand;
        DelegateCommand _addDebtsCommand;

        DelegateCommand _closeCommand;
        DelegateCommand? _closeAppCommand;
        DelegateCommand? _saveAppCommand;

        /// <summary>
        /// this method saves a Debtor.
        /// </summary>
        public DelegateCommand SaveDebtorCommand
        {
            get
            {
                return _saveDebtorCommand ?? ( _saveDebtorCommand = new DelegateCommand(SaveDebtor, SaveDebtorCanExecute).ObservesProperty(() => FullName).ObservesProperty(() => DebtsValue)); 
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
                DebtsValue = Int32.Parse(debtsValue)
            });

            DebtorsModel debtor = new DebtorsModel(fullName, debtsList);

            debtors.Add(debtor);
            MessageBox.Show("New Debtor added" + "\n" + "Name: " + debtor.FullName + "\n" + "Debtsvalue: " + debtsValue);
            window.Close();
        }


        /// <summary>
        /// Should only excute if entered fullname and initial value is not null or not 0.
        /// Also should not exectute if the debtor already exists.
        /// Also not execute if the debtsValue is not of type int.
        /// </summary>
        /// <returns> true if the command should exectue, false if not </returns>
        private bool SaveDebtorCanExecute()
        {
            bool isStringIntegerType = int.TryParse(debtsValue, out _);
            if (!isStringIntegerType) { return false; }

            //if Fullname is null or initial value is 0.
            if (debtsValue.Equals("0") || fullName.Equals(""))
            {
                return false;
            }
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

        public DelegateCommand AddDebtsCommand
        {
            get
            {
                return _addDebtsCommand ?? (_addDebtsCommand = new DelegateCommand(AddNewDebt, CanAddNewDebt)).ObservesProperty(() => NewDebtValue);
            }
        }

        public void AddNewDebt()
        {
            var NewDebt = new DebtsModel()
            {
                LogTime = DateTime.UtcNow.ToShortTimeString(),
                DebtsValue = Int32.Parse(newDebtValue)
            };

            Debtors.Where(x => x.FullName == Debtor.FullName).SingleOrDefault().Debts.Add(NewDebt);
            UpdateSumOfDebts();
            MessageBox.Show("New Debt added" + "\n" + "Name: " + debtorsModel.FullName + "\n" + "Debtsvalue: " + newDebtValue);
            window.Close();
        }

        /// <summary>
        /// Can only add new debts if it is an integer
        /// </summary>
        /// <returns></returns>
        private bool CanAddNewDebt()
        {
            bool isStringIntegerType = int.TryParse(newDebtValue, out _);
            if (!isStringIntegerType) { return false; }
            return true;
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


        public DelegateCommand CloseAppCommand =>
            _closeAppCommand ?? (_closeAppCommand = new DelegateCommand(ExecuteCloseAppCommand));

        void ExecuteCloseAppCommand()
        {
            Application.Current.MainWindow.Close();
        }

        public DelegateCommand SaveAppCommand =>
        _saveAppCommand ?? (_saveAppCommand = new DelegateCommand(ExecuteSaveAppCommand));


        // save it as a JSON.
        // inspired by: https://www.youtube.com/watch?v=2fy6csZb9I0
        private void ExecuteSaveAppCommand()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text File(*.text)|*.text";
            if(saveDialog.ShowDialog() == true)
            {
                var content = Newtonsoft.Json.JsonConvert.SerializeObject(this);
                File.WriteAllText(saveDialog.FileName, content);
            }
        }


        #endregion


    }
}

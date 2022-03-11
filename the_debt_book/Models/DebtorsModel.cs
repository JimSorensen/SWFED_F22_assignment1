using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace the_debt_book.Models
{
    public class DebtorsModel : BindableBase
    {

        public String? FullName { get; set; }
        public ObservableCollection<DebtsModel>? Debts { get; set; }

        int sumOfDebts;


        public DebtorsModel() { }
        public DebtorsModel(string fullName, ObservableCollection<DebtsModel> debts)
        {
            this.FullName = fullName;
            this.Debts = debts;
            SumOfDebts = CalculateDebts();
        }


        public int SumOfDebts
        {
            get { return sumOfDebts; }
            set { SetProperty(ref sumOfDebts, value); }

        }

        public int CalculateDebts()
        {
            int totalAccumulatedDebt = 0;
            if (Debts == null) { return 0; };
            foreach(DebtsModel debt in Debts)
            {
                totalAccumulatedDebt += debt.DebtsValue;
            }
            return totalAccumulatedDebt;
        }
    }
}

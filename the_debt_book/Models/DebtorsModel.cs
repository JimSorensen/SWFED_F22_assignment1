using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_debt_book.Models
{
    public class DebtorsModel
    {

        public String FullName { get; set; }
        public ObservableCollection<DebtsModel> Debts { get; set; }

        public int SumOfDebts
        {
            get { return CalculateDebts(); }
        }

        public int CalculateDebts()
        {
            int totalAccumulatedDebt = 0;

            foreach(DebtsModel debt in Debts)
            {
                totalAccumulatedDebt += debt.DebtsValue;
            }
            return totalAccumulatedDebt;
        }
    }
}

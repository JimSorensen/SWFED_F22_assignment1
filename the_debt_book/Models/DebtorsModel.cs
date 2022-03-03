using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_debt_book.Models
{
    public class DebtorsModel
    {

        public String FullName { get; set; }
        public List<DebtsModel> Debts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Debt_Book.Models;

namespace The_Debt_Book.ViewModels
{
	internal class DepterViewModel
	{
		private Depter? itemSelected;

		public ObservableCollection<Depter> Depters { get; set; }

		public DepterViewModel()
		{
			this.Depters = new ObservableCollection<Depter>();
			PopulateStaticData();
		}
	}
}

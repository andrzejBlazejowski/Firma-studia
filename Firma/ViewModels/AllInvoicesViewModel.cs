using Firma.Helpers;
using Firma.Models;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class AllInvoicesViewModel : AllViewModel<invoice>
    {

        #region Konstruktor
        public AllInvoicesViewModel()
            : base("faktury")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<invoice>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from invoice in ZaliczenieEntities.invoices //dla kazdego...
                  where invoice.is_active == true
                  select invoice //wy...
                );
        }
        #endregion
    }
}

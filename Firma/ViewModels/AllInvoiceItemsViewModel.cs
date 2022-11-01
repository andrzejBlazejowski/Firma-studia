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
    public class AllInvoiceItemsViewModel : AllViewModel<invoice_item>
    {

        #region Konstruktor
        public AllInvoiceItemsViewModel()
            : base("pozycje faktury")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<invoice_item>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from invoice_item in ZaliczenieEntities.invoice_item //dla kazdego...
                  where invoice_item.is_active == true
                  select invoice_item //wy...
                );
        }
        #endregion
    }
}

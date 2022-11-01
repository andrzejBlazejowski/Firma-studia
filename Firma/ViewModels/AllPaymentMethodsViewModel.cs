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
    public class AllPaymentMethodsViewModel : AllViewModel<payment_method>
    {

        #region Konstruktor
        public AllPaymentMethodsViewModel()
            : base("metody płatności")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<payment_method>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from payment_method in ZaliczenieEntities.payment_method //dla kazdego...
                  where payment_method.is_active == true
                  select payment_method //wy...
                );
        }
        #endregion
    }
}

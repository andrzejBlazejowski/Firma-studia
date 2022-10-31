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
    public class AllDeliveryItemsViewModel : AllViewModel<delivery_item>
    {

        #region Konstruktor
        public AllDeliveryItemsViewModel()
            : base("pozycje dostawy")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<delivery_item>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from delivery_item in ZaliczenieEntities.delivery_item //dla kazdego...
                  where delivery_item.is_active == true
                  select delivery_item //wy...
                );
        }
        #endregion
    }
}

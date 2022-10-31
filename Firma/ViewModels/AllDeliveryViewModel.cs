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
    public class AllDeliveryViewModel : AllViewModel<delivery>
    {

        #region Konstruktor
        public AllDeliveryViewModel()
            : base("dostawy")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<delivery>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from delivery in ZaliczenieEntities.deliveries //dla kazdego...
                  where delivery.is_active == true
                  select delivery //wy...
                );
        }
        #endregion
    }
}

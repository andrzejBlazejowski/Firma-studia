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
    public class AllWarehousesViewModel : AllViewModel<warehouse>
    {

        #region Konstruktor
        public AllWarehousesViewModel()
            : base("magazyny")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<warehouse>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from warehouse in ZaliczenieEntities.warehouses //dla kazdego...
                  where warehouse.is_active == true
                  select warehouse //wy...
                );
        }
        #endregion
    }
}

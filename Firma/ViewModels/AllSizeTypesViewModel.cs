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
    public class AllSizeTypesViewModel : AllViewModel<size_type>
    {

        #region Konstruktor
        public AllSizeTypesViewModel()
            : base("dostępne rozmiary")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<size_type>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from size_type in ZaliczenieEntities.size_type //dla kazdego...
                  where size_type.is_active == true
                  select size_type //wy...
                );
        }
        #endregion
    }
}

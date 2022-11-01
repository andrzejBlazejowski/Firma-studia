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
    public class AllCurenciesViewModel : AllViewModel<curency>
    {

        #region Konstruktor
        public AllCurenciesViewModel()
            : base("waluty")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<curency>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from curency in ZaliczenieEntities.curencies //dla kazdego...
                  where curency.is_active == true
                  select curency //wy...
                );
        }
        #endregion
    }
}

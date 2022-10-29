using Firma.Helpers;
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
    public class WszystkieTowaryViewModel: WszystkieViewModel<Towar>
    {
        
        #region Konstruktor
        public WszystkieTowaryViewModel()
            :base("Towary")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Towar>
                (
                //to jest zapytanie linq (obiektowa wersja SQL)
                  from towar in FakturyEntities.Towar //dla kazdego...
                  where towar.CzyAktywny==true
                  select towar //wy...
                );
        }
        #endregion
    }
}

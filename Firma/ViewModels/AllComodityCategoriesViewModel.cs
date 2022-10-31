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
    public class AllComodityCategoriesViewModel : AllViewModel<comodity_category>
    {

        #region Konstruktor
        public AllComodityCategoriesViewModel()
            : base("kategorie towarow")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<comodity_category>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from comodity_category in ZaliczenieEntities.comodity_category //dla kazdego...
                  where comodity_category.is_active == true
                  select comodity_category //wy...
                );
        }
        #endregion
    }
}

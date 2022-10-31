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
    public class AllBrandsViewModel : AllViewModel<brand>
    {

        #region Konstruktor
        public AllBrandsViewModel()
            : base("brands")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<brand>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from brand in ZaliczenieEntities.brands //dla kazdego...
                  where brand.is_active == true
                  select brand //wy...
                );
        }
        #endregion
    }
}

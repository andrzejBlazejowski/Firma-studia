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
    public class AllComoditiesViewModel: AllViewModel<comodity>
    {
        
        #region Konstruktor
        public AllComoditiesViewModel()
            :base("comodities")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<comodity>
                (
                //to jest zapytanie linq (obiektowa wersja SQL)
                  from comodity in ZaliczenieEntities.comodities //dla kazdego...
                  where comodity.is_active==true
                  select comodity //wy...
                );
        }
        #endregion
    }
}

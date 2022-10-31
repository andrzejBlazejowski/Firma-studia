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
    public class AllEmplyeeTypesViewModel : AllViewModel<employ_type>
    {

        #region Konstruktor
        public AllEmplyeeTypesViewModel()
            : base("typy pracowników")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<employ_type>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from employ_type in ZaliczenieEntities.employ_type //dla kazdego...
                  where employ_type.is_active == true
                  select employ_type //wy...
                );
        }
        #endregion
    }
}

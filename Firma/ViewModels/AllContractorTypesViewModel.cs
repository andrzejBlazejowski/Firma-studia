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
    public class AllContractorTypesViewModel : AllViewModel<contractor_type>
    {

        #region Konstruktor
        public AllContractorTypesViewModel()
            : base("typy kontrachentów")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<contractor_type>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from contractor_type in ZaliczenieEntities.contractor_type //dla kazdego...
                  where contractor_type.is_active == true
                  select contractor_type //wy...
                );
        }
        #endregion
    }
}

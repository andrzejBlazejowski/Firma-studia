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
    public class AllContractorsViewModel : AllViewModel<contractor>
    {

        #region Konstruktor
        public AllContractorsViewModel()
            : base("contractor")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<contractor>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from contractors in ZaliczenieEntities.contractors //dla kazdego...
                  where contractors.is_active == true
                  select contractors //wy...
                );
        }
        #endregion
    }
}

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
    public class AllStoragesViewModel : AllViewModel<storage>
    {

        #region Konstruktor
        public AllStoragesViewModel()
            : base("miejsca w magazynach")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<storage>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from storage in ZaliczenieEntities.storages //dla kazdego...
                  where storage.is_active == true
                  select storage //wy...
                );
        }
        #endregion
    }
}

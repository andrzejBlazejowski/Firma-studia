﻿using Firma.Helpers;
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
    public class AllDeliveryStatusesViewModel : AllViewModel<delivery_status>
    {

        #region Konstruktor
        public AllDeliveryStatusesViewModel()
            : base("pozycje dostawy")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<delivery_status>
                (
                  //to jest zapytanie linq (obiektowa wersja SQL)
                  from delivery_status in ZaliczenieEntities.delivery_status //dla kazdego...
                  where delivery_status.is_active == true
                  select delivery_status //wy...
                );
        }
        #endregion
    }
}

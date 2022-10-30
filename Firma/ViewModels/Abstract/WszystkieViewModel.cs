using Firma.Helpers;
using Firma.Models;
using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels.Abstract
{
    public abstract class WszystkieViewModel<T> :WorkspaceViewModel //bo wszystkie zakl....
    {
        //just test commit
        #region Fields
        //to jest obiekt, ktory...
        private readonly ZaliczenieEntities zaliczenieEntities;
        public ZaliczenieEntities ZaliczenieEntities
        { 
            get
            {
                return zaliczenieEntities;
            }
        }
        //to jest komenda do za ladowania towarow
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new BaseCommand(() => Load());//pusta wywoluje load
                }
                return _LoadCommand;
            }
        }
        //w tym obiekcie beda wszystkie towary
        private ObservableCollection<T> _List;
        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null) //jak lista jest pusta wy...
                    Load();
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }
        #endregion
        #region Konstruktor
        public WszystkieViewModel(string displayName)
        {
            base.DisplayName = displayName;
            this.zaliczenieEntities = new ZaliczenieEntities();
        }
        #endregion
        #region Helpers
        public abstract void Load();
        #endregion

    }
}

using Firma.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public  class MainWindowViewModel : BaseViewModel
    {
        //będzie zawierała kolekcje komend, które pojawiają się w menu lewym oraz kolekcje zakładek 
        #region Komendy menu i paska narzedzi
        public ICommand NowyTowarCommand //ta komenda zostanie podpieta pod menu i pasek narzedzi
        { 
            get
            {
                return new BaseCommand(()=>createView(new NowyTowarViewModel()));//to jest komenda, która wyw. funkcję createTowar
            }
        }
        public ICommand TowaryCommand
        {
            get
            {
                return new BaseCommand(showAllTowar);
            }
        }
        public ICommand NowaFakturaCommand 
        {
            get
            {
                return new BaseCommand(() => createView(new NowaFakturaViewModel()));
            }
        }
        public ICommand FakturyCommand
        {
            get
            {
                return new BaseCommand(showAllFaktury);
            }
        }
        #endregion

        #region Przyciski w menu z lewej strony
        private ReadOnlyCollection<CommandViewModel> _Commands;//to jest kolekcja komend w emnu lewym
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get 
            { 
                if(_Commands == null)//sprawdzam czy przyciski z lewej strony menu nie zostały zainicjalizowane
                {
                    List<CommandViewModel> cmds = this.CreateCommands();//tworzę listę przyciskow za pomocą funkcji CreateCommands
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);//tę listę przypisuje do ReadOnlyCollection (bo readOnlyCollection można tylko tworzyć, nie można do niej dodawać)
                }
                return _Commands; 
            }   
        }
        private List<CommandViewModel> CreateCommands()//tu decydujemy jakie przyciski są w lewym menu
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel("Towary",new BaseCommand(showAllTowar)), //to tworzy pierwszy przycisk o nazwie Towary, który pokaże zakładkę wszystkie towary
                new CommandViewModel("Producenci",new BaseCommand(showAllBrands)),
                new CommandViewModel("Kategorie Towarów",new BaseCommand(showAllComodityCategories)),
                new CommandViewModel("kontrachenci",new BaseCommand(showAllContractors)), 
                new CommandViewModel("typy kontrachentów",new BaseCommand(showAllContractorTypes)),
                new CommandViewModel("waluty",new BaseCommand(showAllCurencies)),
                new CommandViewModel("dostawy",new BaseCommand(showAllDeliveries)),
                new CommandViewModel("pozycje dostawy",new BaseCommand(showAllDeliveryItems)),
                new CommandViewModel("statusy dostawy",new BaseCommand(showAllDeliveryStatuses)),
                new CommandViewModel("pracownicy",new BaseCommand(showAllEmployees)),
                new CommandViewModel("typy pracowników",new BaseCommand(showAllEmployeeTypes)),
                new CommandViewModel("faktury",new BaseCommand(showAllInvoices)),
                new CommandViewModel("pozycje faktury",new BaseCommand(showAllInvoiceItems)),
                new CommandViewModel("metody płatności",new BaseCommand(showAllPaymentMethods)),
                new CommandViewModel("dostępne rozmiary",new BaseCommand(showAllSizeTypes)),
                new CommandViewModel("miejsca w magazynach",new BaseCommand(showAllStorages)),


                new CommandViewModel("Towar",new BaseCommand(()=>createView(new NowyTowarViewModel()))),
                new CommandViewModel("Katura",new BaseCommand(()=>createView(new NowaFakturaViewModel()))),
                new CommandViewModel("Faktury",new BaseCommand(showAllFaktury)),
            };
        }
        #endregion

        #region Zakładki
        private ObservableCollection<WorkspaceViewModel> _Workspaces; //to jest kolekcja zakładek
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get 
            {
                if(_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.onWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.onWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.onWorkspaceRequestClose;
        }
        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }
        #endregion
        #region Funkcje pomocnicze
        private void createView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.setActiveWorkspace(workspace);
        }
        private void showAllFaktury()
        {
            WszystkieFakturyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszystkieFakturyViewModel) as WszystkieFakturyViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieFakturyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        //to jest funkcja, która otwiera nową zakładke Towar
        //za każdym tworzy nową NOWĄ zakładkę do dodawania towaru
        //private void createTowar()
        //{
        //    //tworzy zakładke NowyTowar (VM)
        //    NowyTowarViewModel workspace=new NowyTowarViewModel();
        //    //dodajmy ją do kolkcji aktywnych zakładek
        //    this.Workspaces.Add(workspace);
        //    this.setActiveWorkspace(workspace);
        //}
        //to jest funkcja, która otwiera zakładke ze wszystkimi towarami
        //za każdym razem sprawdza czy zakladka z towarami jest juz otwarta, jak jest to ja aktywuje, ajk nie ma to tworzy 
        private void showAllTowar()
        {
            //sz....
            AllComoditiesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllComoditiesViewModel) as AllComoditiesViewModel;
            //jezeli ....
            if(workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace=new AllComoditiesViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllBrands()
        {
            //sz....
            AllBrandsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllBrandsViewModel) as AllBrandsViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllBrandsViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllContractors()
        {
            //sz....
            AllContractorsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllContractorsViewModel) as AllContractorsViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllContractorsViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllDeliveryItems()
        {
            //sz....
            AllDeliveryItemsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllDeliveryItemsViewModel) as AllDeliveryItemsViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllDeliveryItemsViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllDeliveryStatuses()
        {
            //sz....
            AllDeliveryStatusesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllDeliveryStatusesViewModel) as AllDeliveryStatusesViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllDeliveryStatusesViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllContractorTypes()
        {
            //sz....
            AllContractorTypesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllContractorTypesViewModel) as AllContractorTypesViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllContractorTypesViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllCurencies()
        {
            //sz....
            AllCurenciesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllCurenciesViewModel) as AllCurenciesViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllCurenciesViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllDeliveries()
        {
            //sz....
            AllDeliveryViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllDeliveryViewModel) as AllDeliveryViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllDeliveryViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllComodityCategories()
        {
            //sz....
            AllComodityCategoriesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllComodityCategoriesViewModel) as AllComodityCategoriesViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllComodityCategoriesViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllEmployees()
        {
            //sz....
            AllEmplyeesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllEmplyeesViewModel) as AllEmplyeesViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllEmplyeesViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllEmployeeTypes()
        {
            //sz....
            AllEmplyeeTypesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllEmplyeeTypesViewModel) as AllEmplyeeTypesViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllEmplyeeTypesViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllInvoices()
        {
            //sz....
            AllInvoicesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllInvoicesViewModel) as AllInvoicesViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllInvoicesViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllInvoiceItems()
        {
            //sz....
            AllInvoiceItemsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllInvoiceItemsViewModel) as AllInvoiceItemsViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllInvoiceItemsViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllPaymentMethods()
        {
            //sz....
            AllPaymentMethodsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllPaymentMethodsViewModel) as AllPaymentMethodsViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllPaymentMethodsViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllSizeTypes()
        {
            //sz....
            AllSizeTypesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllSizeTypesViewModel) as AllSizeTypesViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllSizeTypesViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }
        private void showAllStorages()
        {
            //sz....
            AllStoragesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AllStoragesViewModel) as AllStoragesViewModel;
            //jezeli ....
            if (workspace == null)
            {
                //tworzymy nowa zakladke Wszystkie towary
                workspace = new AllStoragesViewModel();
                //i dodajemy ja do kolekcji zakladek
                this.Workspaces.Add(workspace);
            }
            //aktywujemy zakladke
            this.setActiveWorkspace(workspace);
        }


        private void setActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }


        #endregion



    }
}

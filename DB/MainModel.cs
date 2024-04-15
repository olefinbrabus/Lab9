using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lab9.DB
{
    public class MainModel : BindableBase
    {
        private readonly DbContext db;


        public MainModel()
        {
            db = new DbContext();
            db.Database.EnsureCreated();
            db.Animals.Load();

            UpdateAnimals();
        }
        private void UpdateAnimals()
        {
            var items = db.Animals.Local.ToList();
            var result = items;
            if (IsSorted)
            {
                if (ByName)
                {
                    result = items.OrderBy(x => x.Name).ToList();
                }
                else if (ByDescription)
                {
                    result = items.OrderBy(x => x.Description).ToList();
                }
                else if (ByType)
                {
                    result = items.OrderBy(x => x.Type).ToList();
                }
            }
            else
            {
                result = items.OrderBy(x => x.Id).ToList();
            }

            Animals.Clear();
            result.ForEach(x =>
            {
                Animals.Add(x);
            });
        } 
        
         public ObservableCollection<Animal> Animals { get; set; } 
            = new ObservableCollection<Animal>();


        private bool _isSorted;


        public bool IsSorted
        {
            get => _isSorted;
            set
            {
                SetProperty(ref _isSorted, value);
                UpdateAnimals();
            }
        }


        private bool _byName = true;


        public bool ByName
        {
            get => _byName;
            set
            {
                SetProperty(ref _byName, value);
                UpdateAnimals();
            }
        }


        private bool _ByDescription;


        public bool ByDescription
        {
            get => _ByDescription;
            set
            {
                SetProperty(ref _ByDescription, value);
                UpdateAnimals();
            }
        }
        private bool _byType;
        public bool ByType
        {
            get => _byType;
            set
            {
                SetProperty(ref _byType, value);
                UpdateAnimals();
            }
        }
       

        private string _newName;
        public string NewName
        {
            get => _newName;
            set => SetProperty(ref _newName, value);
        }


        private string _newDescription;
        public string NewDescription
        {
            get => _newDescription;
            set => SetProperty(ref _newDescription, value);
        }


        private string _newType;
        public string NewType
        {
            get => _newType;
            set => SetProperty(ref _newType, value);
        }


        private string _newBiome;
        public string NewBiome
        {
            get => _newBiome;
            set => SetProperty(ref _newBiome, value);
        }

        public void SaveNewPlant()
        {
            db.Add(new Animal
            {
                Name=NewName,
                Description=NewDescription,
                Type=NewType,
                Biome=NewBiome,

            });
            db.SaveChanges();
            UpdateAnimals();
            ClearNewAnimal();
        }


        public void ClearNewAnimal()
        {
            NewName = "";
            NewDescription = "";
            NewType = "";
            NewBiome = "";
        }

    }
}
    


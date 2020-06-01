using AnthemBuilderLibrary;
using Caliburn.Micro;
using DBApp.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApp.ViewModels
{
    /// <summary>
    /// Class managing CreateEditBuildView
    /// </summary>
    public class CreateEditBuildViewModel : Screen
    {
        private IEventAggregator events;
        private int userId;
        private int buildId;
        private bool newBuild;
        private bool startingValuesSet = false;
        private bool alreadySaved = false;

        private string _selectedName;
        private string _selectedAdditionalNotes;
        private Class _selectedClass;
        private Component _selectedComponent1; //dla każdego osobny insert
        private Component _selectedComponent2;
        private Component _selectedComponent3;
        private Component _selectedComponent4;
        private Component _selectedComponent5;
        private Component _selectedComponent6;
        private Weapon _selectedWeapon1; //dla każdego osobny insert
        private Weapon _selectedWeapon2;
        private string _itemDetailsRarity;
        private string _itemDetailsDamage;
        private string _itemDetailsNormalEffect;
        private string _itemDetailsSpecialEffect;
        private AssaultSystem _selectedAssaultSystem;
        private SupportSystem _selectedSupportSystem;
        private StrikeSystem _selectedStrikeSystem;
        private string _message;



        public BindableCollection<Class> Classes { get; set; }
        public BindableCollection<Rarity> Rarities { get; set; }
        public BindableCollection<AssaultSystem> AssaultSystems { get; set; }
        public BindableCollection<StrikeSystem> StrikeSystems { get; set; }
        public BindableCollection<SupportSystem> SupportSystems { get; set; }
        public BindableCollection<Component> Components { get; set; }
        public BindableCollection<Weapon> Weapons { get; set; }


        private Build sourceBuild;
        private Build createdBuild;
        public Build CreatedBuild { get => createdBuild; set => createdBuild = value; }
        private List<Component> selectedComponents = new List<Component>();
        private List<Weapon> selectedWeapons = new List<Weapon>();

        /// <summary>
        /// Picks default values displayed to the user depending on if user is editing an existing build or creating a new one
        /// </summary>
        /// <param name="_events"></param>
        /// <param name="_newBuild"></param>
        /// <param name="_userId"></param>
        /// <param name="_buildId"></param>
        public CreateEditBuildViewModel(IEventAggregator _events, bool _newBuild, int _userId, int _buildId = -1)
        {
            events = _events;
            newBuild = _newBuild;
            userId = _userId;
            CreatedBuild = new Build();
            DataAccess db = new DataAccess();
            Classes = new BindableCollection<Class>(db.GetClassesWithoutUniversal());
            Rarities = new BindableCollection<Rarity>(db.GetRarities());

            CreatedBuild.BuildId = -1;
            CreatedBuild.AuthorId = userId;
            CreatedBuild.Rating = 0;
            ItemDetailsDamage = "-";
            ItemDetailsRarity = "-";
            ItemDetailsNormalEffect = "-";
            ItemDetailsSpecialEffect = "-";
            if (_buildId != -1)
            {
                buildId = _buildId;   
                sourceBuild = db.GetBuild(buildId);
                CreatedBuild.BuildId = buildId;

                AssaultSystems = new BindableCollection<AssaultSystem>(db.GetAssaultSystems(sourceBuild.ClassId));
                StrikeSystems = new BindableCollection<StrikeSystem>(db.GetStrikeSystems(sourceBuild.ClassId));
                SupportSystems = new BindableCollection<SupportSystem>(db.GetSupportSystems(sourceBuild.ClassId));
                Components = new BindableCollection<Component>(db.GetComponents(sourceBuild.ClassId));
                Weapons = new BindableCollection<Weapon>(db.GetWeapons());
                CreatedBuild.Name = sourceBuild.Name;
                SelectedName = sourceBuild.Name;

                CreatedBuild.AdditionalNotes = sourceBuild.AdditionalNotes;
                SelectedAdditionalNotes = sourceBuild.AdditionalNotes;

                CreatedBuild.AuthorId = sourceBuild.AuthorId;
                CreatedBuild.Rating = sourceBuild.Rating;


                CreatedBuild.ClassId = sourceBuild.ClassId;
                Class _tmpc = db.GetClass(buildId);
                for (int i = 0; i < Classes.Count; i++)
                {
                    if (Classes[i].ClassId == _tmpc.ClassId)
                    {
                        SelectedClass = Classes[i];
                    }
                }

                CreatedBuild.StrikeSystemId = sourceBuild.StrikeSystemId;
                StrikeSystem _tmp = db.GetStrikeSystem(buildId);
                for(int i=0; i<StrikeSystems.Count; i++)
                {
                    if (StrikeSystems[i].StrikeSystemId == _tmp.StrikeSystemId)
                    {
                        SelectedStrikeSystem = StrikeSystems[i];
                    }
                }
                

                CreatedBuild.AssaultSystemId = sourceBuild.AssaultSystemId;
                AssaultSystem _tmpa = db.GetAssaultSystem(buildId);
                for (int i = 0; i < AssaultSystems.Count; i++)
                {
                    if (AssaultSystems[i].AssaultSystemId == _tmpa.AssaultSystemId)
                    {
                        SelectedAssaultSystem = AssaultSystems[i];
                    }
                }

                CreatedBuild.SupportSystemId = sourceBuild.SupportSystemId;
                SupportSystem _tmps = db.GetSupportSystem(buildId);
                for (int i = 0; i < SupportSystems.Count; i++)
                {
                    if (SupportSystems[i].SupportSystemId == _tmps.SupportSystemId)
                    {
                        SelectedSupportSystem = SupportSystems[i];
                    }
                }

                selectedComponents = db.GetBuildsComponents(buildId);
                Components = new BindableCollection<Component>(db.GetComponents(sourceBuild.ClassId));
                int[] selectedComponentsindeks = { -1, -1, -1, -1, -1, -1 };
                int tmp = 0;
                for (int i = 0; i < Components.Count; i++)
                {
                    for(int j=0; j<selectedComponents.Count; j++)
                    {
                        if (Components[i].ComponentId == selectedComponents[j].ComponentId)
                        {
                            selectedComponentsindeks[tmp] = i;
                            tmp++;
                        }
                    }
                }
                SelectedComponent1 = Components[selectedComponentsindeks[0]];
                SelectedComponent2 = Components[selectedComponentsindeks[1]];
                SelectedComponent3 = Components[selectedComponentsindeks[2]];
                SelectedComponent4 = Components[selectedComponentsindeks[3]];
                SelectedComponent5 = Components[selectedComponentsindeks[4]];
                SelectedComponent6 = Components[selectedComponentsindeks[5]];

                selectedWeapons = db.GetBuildsWeapons(buildId);
                Weapons = new BindableCollection<Weapon>(db.GetWeapons());
                int[] selectedWeaponsindeks = { -1, -1 };
                tmp = 0;
                for (int i = 0; i < Weapons.Count; i++)
                {
                    for (int j = 0; j < selectedWeapons.Count; j++)
                    {
                        if (Weapons[i].WeaponId == selectedWeapons[j].WeaponId)
                        {
                            selectedWeaponsindeks[tmp] = i;
                            tmp++;
                        }
                    }
                }
                SelectedWeapon1 = Weapons[selectedWeaponsindeks[0]];
                SelectedWeapon2 = Weapons[selectedWeaponsindeks[1]];

                ItemDetailsDamage = "-";
                ItemDetailsRarity = "-";
                ItemDetailsNormalEffect = "-";
                ItemDetailsSpecialEffect = "-";
                startingValuesSet = true;
            }
        }

        public string SelectedName
        {
            get { return _selectedName; }
            set
            {
                _selectedName = value;
                CreatedBuild.Name = SelectedName;
                NotifyOfPropertyChange(() => SelectedName);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }
        public string SelectedAdditionalNotes
        {
            get { return _selectedAdditionalNotes; }
            set
            {
                _selectedAdditionalNotes = value;
                CreatedBuild.AdditionalNotes = SelectedAdditionalNotes;
                NotifyOfPropertyChange(() => SelectedAdditionalNotes);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public Component SelectedComponent1
        {
            get { return _selectedComponent1; }
            set
            {
                _selectedComponent1 = value;
                if(selectedComponents.Count > 0)
                {
                    selectedComponents[0] = SelectedComponent1;
                }
                else
                {
                    selectedComponents.Add(SelectedComponent1);
                }
                

                ItemDetailsDamage = "-";
                ItemDetailsRarity = Rarities[SelectedComponent1.RarityId].Name;
                ItemDetailsNormalEffect = SelectedComponent1.NormalEffect;
                ItemDetailsSpecialEffect = SelectedComponent1.SpecialEffect;

                NotifyOfPropertyChange(() => SelectedComponent1);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public Component SelectedComponent2
        {
            get { return _selectedComponent2; }
            set
            {
                _selectedComponent2 = value;
                if (selectedComponents.Count > 1)
                {
                    selectedComponents[1] = SelectedComponent2;
                }
                else
                {
                    selectedComponents.Add(SelectedComponent2);
                }

                ItemDetailsDamage = "-";
                ItemDetailsRarity = Rarities[SelectedComponent2.RarityId].Name;
                ItemDetailsNormalEffect = SelectedComponent2.NormalEffect;
                ItemDetailsSpecialEffect = SelectedComponent2.SpecialEffect;

                NotifyOfPropertyChange(() => SelectedComponent2);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public Component SelectedComponent3
        {
            get { return _selectedComponent3; }
            set
            {
                _selectedComponent3 = value;
                if (selectedComponents.Count > 2)
                {
                    selectedComponents[2] = SelectedComponent3;
                }
                else
                {
                    selectedComponents.Add(SelectedComponent3);
                }

                ItemDetailsDamage = "-";
                ItemDetailsRarity = Rarities[SelectedComponent3.RarityId].Name;
                ItemDetailsNormalEffect = SelectedComponent3.NormalEffect;
                ItemDetailsSpecialEffect = SelectedComponent3.SpecialEffect;

                NotifyOfPropertyChange(() => SelectedComponent3);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public Component SelectedComponent4
        {
            get { return _selectedComponent4; }
            set
            {
                _selectedComponent4 = value;
                if (selectedComponents.Count > 3)
                {
                    selectedComponents[3] = SelectedComponent4;
                }
                else
                {
                    selectedComponents.Add(SelectedComponent4);
                }

                ItemDetailsDamage = "-";
                ItemDetailsRarity = Rarities[SelectedComponent4.RarityId].Name;
                ItemDetailsNormalEffect = SelectedComponent4.NormalEffect;
                ItemDetailsSpecialEffect = SelectedComponent4.SpecialEffect;

                NotifyOfPropertyChange(() => SelectedComponent4);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public Component SelectedComponent5
        {
            get { return _selectedComponent5; }
            set
            {
                _selectedComponent5 = value;
                if (selectedComponents.Count > 4)
                {
                    selectedComponents[4] = SelectedComponent5;
                }
                else
                {
                    selectedComponents.Add(SelectedComponent5);
                }

                ItemDetailsDamage = "-";
                ItemDetailsRarity = Rarities[SelectedComponent5.RarityId].Name;
                ItemDetailsNormalEffect = SelectedComponent5.NormalEffect;
                ItemDetailsSpecialEffect = SelectedComponent5.SpecialEffect;

                NotifyOfPropertyChange(() => SelectedComponent5);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public Component SelectedComponent6
        {
            get { return _selectedComponent6; }
            set
            {
                _selectedComponent6 = value;
                if (selectedComponents.Count > 5)
                {
                    selectedComponents[5] = SelectedComponent6;
                }
                else
                {
                    selectedComponents.Add(SelectedComponent6);
                }

                ItemDetailsDamage = "-";
                ItemDetailsRarity = Rarities[SelectedComponent6.RarityId].Name;
                ItemDetailsNormalEffect = SelectedComponent6.NormalEffect;
                ItemDetailsSpecialEffect = SelectedComponent6.SpecialEffect;

                NotifyOfPropertyChange(() => SelectedComponent6);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public Class SelectedClass
        {
            get { return _selectedClass; }
            set
            {
                _selectedClass = value;
                CreatedBuild.ClassId = SelectedClass.ClassId;
                DataAccess db = new DataAccess();
                AssaultSystems = new BindableCollection<AssaultSystem>(db.GetAssaultSystems(SelectedClass.ClassId));
                StrikeSystems = new BindableCollection<StrikeSystem>(db.GetStrikeSystems(SelectedClass.ClassId));
                SupportSystems = new BindableCollection<SupportSystem>(db.GetSupportSystems(SelectedClass.ClassId));
                Components = new BindableCollection<Component>(db.GetComponents(SelectedClass.ClassId));
                Weapons = new BindableCollection<Weapon>(db.GetWeapons());
                CreatedBuild.StrikeSystemId = 0;
                CreatedBuild.SupportSystemId = 0;
                CreatedBuild.AssaultSystemId = 0;

                if(startingValuesSet)
                {
                    selectedWeapons[0].WeaponId = 0;
                    selectedWeapons[1].WeaponId = 0;
                    selectedComponents[0].ComponentId = 0;
                    selectedComponents[1].ComponentId = 0;
                    selectedComponents[2].ComponentId = 0;
                    selectedComponents[3].ComponentId = 0;
                    selectedComponents[4].ComponentId = 0;
                    selectedComponents[5].ComponentId = 0;                   
                }
                
                NotifyOfPropertyChange(() => SelectedClass);
                NotifyOfPropertyChange(() => AssaultSystems);
                NotifyOfPropertyChange(() => StrikeSystems);
                NotifyOfPropertyChange(() => SupportSystems);
                NotifyOfPropertyChange(() => Components);
                NotifyOfPropertyChange(() => Weapons);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public Weapon SelectedWeapon1
        {
            get { return _selectedWeapon1; }
            set
            {
                _selectedWeapon1 = value;
                if (selectedWeapons.Count > 0)
                {
                    selectedWeapons[0] = SelectedWeapon1;
                }
                else
                {
                    selectedWeapons.Add(SelectedWeapon1);
                }

                ItemDetailsDamage = SelectedWeapon1.Damage.ToString();
                ItemDetailsRarity = Rarities[SelectedWeapon1.RarityId].Name;
                ItemDetailsNormalEffect = "-";
                ItemDetailsSpecialEffect = SelectedWeapon1.SpecialEffect;

                NotifyOfPropertyChange(() => SelectedWeapon1);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }
        
        public Weapon SelectedWeapon2
        {
            get { return _selectedWeapon2; }
            set
            {
                _selectedWeapon2 = value;
                if (selectedWeapons.Count > 1)
                {
                    selectedWeapons[1] = SelectedWeapon2;
                }
                else
                {
                    selectedWeapons.Add(SelectedWeapon2);
                }

                ItemDetailsDamage = SelectedWeapon1.Damage.ToString();
                ItemDetailsRarity = Rarities[SelectedWeapon2.RarityId].Name;
                ItemDetailsNormalEffect = "-";
                ItemDetailsSpecialEffect = SelectedWeapon2.SpecialEffect;

                NotifyOfPropertyChange(() => SelectedWeapon2);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public string ItemDetailsRarity
        {
            get { return _itemDetailsRarity; }
            set
            {
                _itemDetailsRarity = value;
                NotifyOfPropertyChange(() => ItemDetailsRarity);
            }
        }
        public string ItemDetailsNormalEffect
        {
            get { return _itemDetailsNormalEffect; }
            set
            {
                _itemDetailsNormalEffect = value;
                NotifyOfPropertyChange(() => ItemDetailsNormalEffect);
            }
        }
        public string ItemDetailsSpecialEffect
        {
            get { return _itemDetailsSpecialEffect; }
            set
            {
                _itemDetailsSpecialEffect = value;
                NotifyOfPropertyChange(() => ItemDetailsSpecialEffect);
            }
        }
        public string ItemDetailsDamage
        {
            get { return _itemDetailsDamage; }
            set
            {
                _itemDetailsDamage = value;
                NotifyOfPropertyChange(() => ItemDetailsDamage);
            }
        }

        public AssaultSystem SelectedAssaultSystem
        {
            get { return _selectedAssaultSystem; }
            set
            {
                _selectedAssaultSystem = value;
                CreatedBuild.AssaultSystemId = SelectedAssaultSystem.AssaultSystemId;

                ItemDetailsDamage = SelectedAssaultSystem.Damage.ToString();
                ItemDetailsRarity = Rarities[SelectedAssaultSystem.RarityId].Name;
                ItemDetailsNormalEffect = "-";
                ItemDetailsSpecialEffect = SelectedAssaultSystem.SpecialEffect;

                NotifyOfPropertyChange(() => SelectedAssaultSystem);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public SupportSystem SelectedSupportSystem
        {
            get { return _selectedSupportSystem; }
            set
            {
                _selectedSupportSystem = value;
                CreatedBuild.SupportSystemId = SelectedSupportSystem.SupportSystemId;

                ItemDetailsDamage = "-";
                ItemDetailsRarity = Rarities[SelectedSupportSystem.RarityId].Name;
                ItemDetailsNormalEffect = "-";
                ItemDetailsSpecialEffect = "-";

                NotifyOfPropertyChange(() => SelectedSupportSystem);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public StrikeSystem SelectedStrikeSystem
        {
            get { return _selectedStrikeSystem; }
            set
            {
                _selectedStrikeSystem = value;
                CreatedBuild.StrikeSystemId = SelectedStrikeSystem.StrikeSystemId;

                ItemDetailsDamage = SelectedStrikeSystem.Damage.ToString();
                ItemDetailsRarity = Rarities[SelectedStrikeSystem.RarityId].Name;
                ItemDetailsNormalEffect = "-";
                ItemDetailsSpecialEffect = SelectedStrikeSystem.SpecialEffect;

                NotifyOfPropertyChange(() => SelectedStrikeSystem);
                NotifyOfPropertyChange(() => CanSaveBuild);
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        /// <summary>
        /// Handles the "Menu" button
        /// </summary>
        public void ReturnToMenu()
        {
            events.PublishOnUIThread(new ReturnToMenuEvent());
        }

        /// <summary>
        /// Enables/disables the "Save" button
        /// </summary>
        public bool CanSaveBuild
        {
            get
            {
                if (CreatedBuild.BuildId != 0 && CreatedBuild.Name != null && CreatedBuild.Name != "" && CreatedBuild.AdditionalNotes != null && CreatedBuild.AdditionalNotes != "" && CreatedBuild.AuthorId != 0
                    && CreatedBuild.StrikeSystemId != 0 && CreatedBuild.AssaultSystemId != 0 && CreatedBuild.SupportSystemId != 0 && selectedComponents != null && selectedWeapons != null)  
                {
                    if (selectedComponents[0].ComponentId != 0 && selectedComponents[1].ComponentId != 0 && selectedComponents[2].ComponentId != 0 && selectedComponents[3].ComponentId != 0 && selectedComponents[4].ComponentId != 0 && selectedComponents[5].ComponentId != 0
                    && selectedWeapons[0].WeaponId != 0 && selectedWeapons[1].WeaponId != 0)
                    {
                        if(!alreadySaved)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }          
        }

        /// <summary>
        /// Handles the "Save" button
        /// </summary>
        async public void SaveBuild()
        {
            DataAccess db = new DataAccess();
            if (newBuild)
            {
                alreadySaved = true;
                NotifyOfPropertyChange(() => CanSaveBuild);
                Message = "Saving... wait";
               await Task.Factory.StartNew(() => db.InsertNewBuild(CreatedBuild, selectedComponents, selectedWeapons));
            }
            else
            {
                alreadySaved = true;
                NotifyOfPropertyChange(() => CanSaveBuild);
                Message = "Saving... wait";
                await Task.Factory.StartNew(() => db.UpdateBuild(sourceBuild.BuildId, CreatedBuild, selectedComponents, selectedWeapons));
            }
            events.PublishOnUIThread(new ReturnToMenuEvent());
        }
    }
}

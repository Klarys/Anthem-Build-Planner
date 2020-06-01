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
    /// Class managing BuildDetailsView
    /// </summary>
    public class BuildDetailsViewModel : Screen

    {
        public int _buildId;
        public int _userId;
        private bool Saved;
        private IEventAggregator _events;
        private bool wasRatedUp = false;
        private bool wasRatedDown = false;
        private bool wasSaved = false;
        private bool wasCopied = false;
        private string _itemDetailsName;
        private string _itemDetailsRarity;
        private string _itemDetailsDamage;
        private string _itemDetailsNormalEffect;
        private string _itemDetailsSpecialEffect;
        private List<Rarity> Rarities;
        public BindableCollection<Component> Components { get; set; }
        public BindableCollection<Weapon> Weapons { get; set; }

        private string _totalArmorAndShield;
        private Component _selectedComponent;
        private Weapon _selectedWeapon;
        private Build selectedBuild;
        private string _selectedBuildName;
        private string _selectedBuildAdditionalNotes;
        private string _summary;

        public string Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                NotifyOfPropertyChange(() => Summary);
            }
        }


        private bool userIsAuthor;
        public bool UserIsAuthor
        {
            get { return userIsAuthor; }
            set
            {
                userIsAuthor = value;
                NotifyOfPropertyChange(() => UserIsAuthor);
            }
        }

        /// <summary>
        /// Gets the required information from a database
        /// </summary>
        /// <param name="events"></param>
        /// <param name="buildId"></param>
        /// <param name="userId"></param>
        /// <param name="saved"></param>
        public BuildDetailsViewModel(IEventAggregator events, int buildId, int userId, bool saved = false)
        {
            _events = events;
            _buildId = buildId;
            _userId = userId;
            Saved = saved;
            DataAccess db = new DataAccess();
            Components = new BindableCollection<Component>(db.GetBuildsComponents(_buildId));
            Weapons = new BindableCollection<Weapon>(db.GetBuildsWeapons(_buildId));
            selectedBuild = db.GetBuild(_buildId);
            Rarities = db.GetRarities();

            SelectedBuildName = selectedBuild.Name;
            SelectedBuildAdditionalNotes = selectedBuild.AdditionalNotes;

            if(selectedBuild.AuthorId ==_userId)
            {
                UserIsAuthor = true;
            }

            int armor = 0;
            int shield = 0;
            foreach (Component cmpt in Components)
            {
                armor += cmpt.ArmorReinforcement;
                shield += cmpt.ShieldReinforcement;
                Summary += cmpt.NormalEffect + " ";
                Summary += cmpt.SpecialEffect + " ";
            }
            TotalArmorAndShield = "" + armor.ToString() + " / " + shield.ToString();

            AssaultSystem _tmpas = db.GetAssaultSystem(_buildId);
            StrikeSystem _tmpsts = db.GetStrikeSystem(_buildId);
            Summary += Weapons[0].SpecialEffect + " " + Weapons[1].SpecialEffect + " " + _tmpas.SpecialEffect + " " + _tmpsts.SpecialEffect;
        }
        public string SelectedBuildName
        {
            get { return _selectedBuildName; }
            set
            {
                _selectedBuildName = value;
                NotifyOfPropertyChange(() => SelectedBuildName);
            }
        }


        public string SelectedBuildAdditionalNotes
        {
            get { return _selectedBuildAdditionalNotes; }
            set
            {
                _selectedBuildAdditionalNotes = value;
                NotifyOfPropertyChange(() => SelectedBuildAdditionalNotes);
            }
        }

        /// <summary>
        /// Shows details about the Selected Components
        /// </summary>
        public Component SelectedComponent
        {
            get { return _selectedComponent; }
            set
            {
                _selectedComponent = value;
                ItemDetailsName = SelectedComponent.Name;
                ItemDetailsDamage = "-";
                ItemDetailsRarity = Rarities[SelectedComponent.RarityId].Name;
                ItemDetailsNormalEffect = SelectedComponent.NormalEffect;
                ItemDetailsSpecialEffect = SelectedComponent.SpecialEffect;
                NotifyOfPropertyChange(() => SelectedComponent);
            }
        }
        /// <summary>
        /// Shows details about the selected weapon
        /// </summary>
        public Weapon SelectedWeapon
        {
            get { return _selectedWeapon; }
            set
            {
                _selectedWeapon = value;
                ItemDetailsName = SelectedWeapon.Name;
                ItemDetailsDamage = SelectedWeapon.Damage + " (Ammo: " + SelectedWeapon.Ammo + ", Rpm:" + SelectedWeapon.Rpm + ")";
                ItemDetailsRarity = Rarities[SelectedWeapon.RarityId].Name;
                ItemDetailsNormalEffect = "-";
                ItemDetailsSpecialEffect = SelectedWeapon.SpecialEffect;
                NotifyOfPropertyChange(() => SelectedWeapon);
            }
        }

        public string TotalArmorAndShield
        {
            get { return _totalArmorAndShield; }
            set
            {
                _totalArmorAndShield = value;
                NotifyOfPropertyChange(() => TotalArmorAndShield);
            }
        }
        public string ItemDetailsName
        {
            get { return _itemDetailsName; }
            set
            {
                _itemDetailsName = value;
                NotifyOfPropertyChange(() => ItemDetailsName);
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

        public bool WasCopied
        {
            get { return wasCopied; }
            set
            {
                wasCopied = value;
                NotifyOfPropertyChange(() => WasCopied);
                NotifyOfPropertyChange(() => CanCopy);
            }
        }

        public bool WasSaved
        {
            get { return wasSaved; }
            set
            {
                wasSaved = value;
                NotifyOfPropertyChange(() => WasSaved);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        public bool WasRatedDown
        {
            get { return wasRatedDown; }
            set
            {
                wasRatedDown = value;
                NotifyOfPropertyChange(() => WasRatedDown);
                NotifyOfPropertyChange(() => CanVoteDown);
            }
        }

        public bool WasRatedUp
        {
            get { return wasRatedUp; }
            set
            {
                wasRatedUp = value;
                NotifyOfPropertyChange(() => WasRatedUp);
                NotifyOfPropertyChange(() => CanVoteUp);
            }
        }

        
        /// <summary>
        /// Enables/disables the "Vote up" button
        /// </summary>
        public bool CanVoteUp
        {
            get
            {
                if(WasRatedUp)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Enables/disables the "Vote down" button
        /// </summary>
        public bool CanVoteDown
        {
            get
            {
                if (WasRatedDown)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Enables/disables the "Edit" button
        /// </summary>
        public bool CanEdit
        {
            get
            {
                return UserIsAuthor;
            }
        }

        /// <summary>
        /// Enables/disables the "Save" button
        /// </summary>
        public bool CanSave
        {
            get
            {
                DataAccess db = new DataAccess();
                if (WasSaved == true || db.CanSave(_buildId, _userId) == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// Enables/disables the "Make copy" button
        /// </summary>
        public bool CanCopy
        {
            get
            {
                if(WasCopied == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Handles the "Vote up" button
        /// </summary>
        public void VoteUp()
        {
            WasRatedUp = true;
            DataAccess db = new DataAccess();
            if (WasRatedDown)
            {
                db.RateBuildUp(_buildId, wasRatedDown);
                WasRatedDown = false;
            }
            else
            {
                db.RateBuildUp(_buildId, wasRatedDown);
            }
                
            
        }

        /// <summary>
        /// Handles the "Vote down" button
        /// </summary>
        public void VoteDown()
        {
            WasRatedDown = true;
            DataAccess db = new DataAccess();
            if (WasRatedUp)
            {
                db.RateBuildDown(_buildId, wasRatedUp);
                WasRatedUp = false;    
            }
            else
            {
                db.RateBuildDown(_buildId, wasRatedUp);
            }
            
            
        }

        /// <summary>
        /// Handles the "Save" button
        /// </summary>
        async public void Save()
        {
            WasSaved = true;
            DataAccess db = new DataAccess();
            await Task.Factory.StartNew(() => db.Save(_buildId, _userId));
        }
        /// <summary>
        /// Handles the "Copy" button
        /// </summary>
        async public void Copy()
        {
            WasCopied = true;
            DataAccess db = new DataAccess();
            await Task.Factory.StartNew(() =>db.Copy(_buildId, _userId));            
        }

        /// <summary>
        /// Handles the "Edit" button
        /// </summary>
        public void Edit()
        {
            _events.PublishOnUIThread(new CreateEditBuildEventModel(false, _buildId));
        }

        /// <summary>
        /// Handles the "Back" button
        /// </summary>
        public void Back()
        {
            if(Saved)
            {
                _events.PublishOnUIThread(new BrowseBuildsEvent(true));
            }
            else
            {
                _events.PublishOnUIThread(new BrowseBuildsEvent());
            }
            
        }

        /// <summary>
        /// Handles the "SupportSystem" button
        /// </summary>
        public void SupportSystemDetails()
        {
            DataAccess db = new DataAccess();
            SupportSystem _tmp = db.GetSupportSystem(_buildId);
            ItemDetailsName = _tmp.Name;
            ItemDetailsDamage = "-";
            ItemDetailsRarity = Rarities[_tmp.RarityId].Name;
            ItemDetailsNormalEffect = "-";
            ItemDetailsSpecialEffect = "-";
        }
        /// <summary>
        /// Handles the "AssaultSystem" button
        /// </summary>
        public void AssaultSystemDetails()
        {
            DataAccess db = new DataAccess();
            AssaultSystem _tmp = db.GetAssaultSystem(_buildId);
            ItemDetailsName = _tmp.Name;
            ItemDetailsDamage = _tmp.Damage.ToString();
            ItemDetailsRarity = Rarities[_tmp.RarityId].Name;
            ItemDetailsNormalEffect = "-";
            ItemDetailsSpecialEffect = _tmp.SpecialEffect;
        }
        /// <summary>
        /// Handles the "Strike System" button
        /// </summary>
        public void StrikeSystemDetails()
        {
            DataAccess db = new DataAccess();
            StrikeSystem _tmp = db.GetStrikeSystem(_buildId);
            ItemDetailsName = _tmp.Name;
            ItemDetailsDamage = _tmp.Damage.ToString();
            ItemDetailsRarity = Rarities[_tmp.RarityId].Name;
            ItemDetailsNormalEffect = "-";
            ItemDetailsSpecialEffect = _tmp.SpecialEffect;
        }
    }
}

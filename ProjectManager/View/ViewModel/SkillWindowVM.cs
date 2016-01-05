using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using PMDataLayer;
using PMView.View.WrapperVM;
using Core;

namespace PMView
{
    public class SkillWindowVM : INotifyPropertyChanged, ILoadData
    {
        private ObservableCollection<SkillVM> _skillsCollection = new ObservableCollection<SkillVM>();
        private string _name;

        private bool _addButton;
        private bool _editButton;
        private bool _removeButton;
        private bool _saveButton;
        private SkillVM _selectedSkill;
        private bool _editing;
        private bool _somethingChanged = false;
        private ObservableCollection<SkillVM> _savedCollection;
        private bool _saveAllChangesButton;
        private bool _cancelAllChangesButton;
        private ILoadData _lastScreen;

        public SkillWindowVM(ILoadData lastScreen)
        {
            _lastScreen = lastScreen;
            AddButton = false;
            EditButton = false;
            RemoveButton = false;
            SaveButton = false;
            CancelAllChangesButton = false;
            SaveAllChangesButton = false;
        }



        public ObservableCollection<SkillVM> SkillsCollection
        {
            get
            {
                if (_savedCollection != null)
                    return _savedCollection;

                _skillsCollection.Clear();
                _savedCollection = new ObservableCollection<SkillVM>();
                foreach (var item in SkillVM.Skills)
                {
                    _skillsCollection.Add(item);
                    _savedCollection.Add(item.Clone());
                }

                return _skillsCollection;
            }
        }

        internal void AddButtonClick()
        {
            _savedCollection.Add(new SkillVM(new Skill() { Name = Name }));
            AddButton = false;
            Name = string.Empty;
            OnPropertyChanged("SkillsCollection");
            OnPropertyChanged("Name");
            SaveAllChangesButton = true;
            _somethingChanged = true;
        }

        internal void CancelAllChangesButtonClick()
        {
            Name = string.Empty;
            OnPropertyChanged("Name");
            _savedCollection = null;
            _somethingChanged = false;
            CancelAllChangesButton = false;
            SaveAllChangesButton = false;
            OnPropertyChanged("CancelAllChangesButton");
            OnPropertyChanged("SkillsCollection");

        }

        public bool AddButton
        {
            get { return _addButton; }
            set
            {
                _addButton = value;
                OnPropertyChanged("AddButton");
            }
        }


        public bool SaveAllChangesButton
        {
            get { return _saveAllChangesButton; }
            set
            {
                _saveAllChangesButton = value;
                CancelAllChangesButton = value;
                OnPropertyChanged("SaveAllChangesButton");
            }
        }

        internal void RemoveButtonClick()
        {
            SaveAllChangesButton = true;
            Editing = false;
            AddButton = false;
            EditButton = false;
            RemoveButton = false;
            SaveButton = false;
            _somethingChanged = true;
            if (SelectedSkill != null)
            {
                _savedCollection.Remove(_savedCollection.First(item=>item.Name == SelectedSkill.Name));
            }
            Name = string.Empty;
            SelectedSkill = null;
            OnPropertyChanged("SkillsCollection");
            OnPropertyChanged("Name");
        }

        internal void SaveButtonClick()
        {
            SaveAllChangesButton = true;
            SaveButton = false;
            getSkillByName(SelectedSkill.Name).Name = Name;
            OnPropertyChanged("SkillsCollection");
            Name = string.Empty;
            OnPropertyChanged("Name");
        }

        public bool CancelAllChangesButton
        {
            get { return _cancelAllChangesButton; }
            set
            {
                _cancelAllChangesButton = value;
                OnPropertyChanged("CancelAllChangesButton");
            }
        }

        public bool EditButton
        {
            get { return _editButton; }
            set
            {
                _editButton = value;
                OnPropertyChanged("EditButton");
            }
        }

        internal void SaveAllButtonClick()
        {
            foreach (var item in _savedCollection)
            {
                var find = Skill.Items.FirstOrDefault(skill => skill.Id == item.Skill.Id);
                if (find == null)
                {
                    Skill.Items.Add(item.Skill);
                }
                else
                {
                    find.Name = item.Name;
                }
            }
            List<Skill> toDelete = new List<Skill>();
            foreach (var item in Skill.Items)
            {
                var find = _savedCollection.FirstOrDefault(skill => skill.Skill.Id == item.Id);
                if (find == null)
                {
                    toDelete.Add(item);
                }
            }
            foreach (var item in toDelete)
            {
                User_Skill.Items.RemoveAll(skill => skill.Skill.Id == item.Id);
                Project_Skill.Items.RemoveAll(skill => skill.Skill.Id == item.Id);
                Skill.Items.RemoveAll(skill => skill.Id == item.Id);
            }
            _savedCollection = null;
            SaveAllChangesButton = false;
            LoadData();
        }

        public bool RemoveButton
        {
            get { return _removeButton; }
            set
            {
                _removeButton = value;
                OnPropertyChanged("RemoveButton");
            }
        }

        public bool SaveButton
        {
            get { return _saveButton; }
            set
            {
                _saveButton = value;
                OnPropertyChanged("SaveButton");
            }
        }

        public bool Editing
        {
            get { return _editing; }
            set
            {
                _editing = value;
                if (_editing)
                {
                    AddButton = false;
                    EditButton = false;
                    RemoveButton = false;
                    SaveButton = false;
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                if (string.IsNullOrEmpty(value))
                {
                    AddButton = false;
                    EditButton = false;
                    RemoveButton = false;
                    if (!_somethingChanged)
                        SaveButton = false;
                    Editing = false;
                    return;
                }

                if (!Editing)
                {
                    if (IsExist(value))
                    {
                        SelectedSkill = getSkillByName(value);
                        AddButton = false;
                        EditButton = true;
                        RemoveButton = true;
                        SaveButton = false;
                    }
                    else
                    {
                        RemoveButton = false;
                        AddButton = true;
                        EditButton = false;
                    }
                }
                else
                {
                    AddButton = false;
                    EditButton = false;
                    RemoveButton = false;
                    if (IsExist(value))
                    {
                        SaveButton = false;
                    }
                    else
                    {
                        SaveButton = true;
                    }
                }
                OnPropertyChanged("Name");
            }
        }

        public SkillVM SelectedSkill
        {
            get { return _selectedSkill; }
            set { _selectedSkill = value; }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private SkillVM getSkillByName(string skillName)
        {
            if (_somethingChanged)
            {
                var find = _savedCollection.Where(item => item.Name == skillName).FirstOrDefault();
                return find;
            }
            else
            {
                var find = _savedCollection.Where(item => item.Name == skillName).FirstOrDefault();
                return find;
            }
        }

        private bool IsExist(string skillName)
        {
            return getSkillByName(skillName) == null ? false : true;
        }

        public void LoadData()
        {
            OnPropertyChanged("SkillsCollection");
            _lastScreen.LoadData();
        }
    }
}
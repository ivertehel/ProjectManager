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
    public class SkillWindowVM : INotifyPropertyChanged
    {
        private ObservableCollection<SkillVM> _skillsCollection = new ObservableCollection<SkillVM>();
        private string _name;

        private bool _addButton;
        private bool _editButton;
        private bool _removeButton;
        private bool _saveButton;
        private SkillVM _selectedSkill;
        private bool _editing;

        public SkillWindowVM()
        {
            AddButton = false;
            EditButton = false;
            RemoveButton = false;
            SaveButton = false;
        }

        public ObservableCollection<SkillVM> SkillsCollection
        {
            get
            {
                _skillsCollection.Clear();
                foreach (var item in SkillVM.Skills)
                {
                    _skillsCollection.Add(item);
                }

                return _skillsCollection;
            }
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

        public bool EditButton
        {
            get { return _editButton; }
            set
            {
                _editButton = value;
                OnPropertyChanged("EditButton");
            }
        }

        internal void SaveButtonClick()
        {
            Editing = false;
            AddButton = false;
            EditButton = false;
            RemoveButton = false;
            SaveButton = false;
            if (SelectedSkill != null)
            {
                var skill = (from items in Skill.Items where items.Name == SelectedSkill.Name select items).FirstOrDefault();
                if (skill == null)
                    throw new Exception("Unknown error");

                skill.Name = Name;
            }
            Name = string.Empty;
            SelectedSkill = null;
            OnPropertyChanged("SkillsCollection");
            OnPropertyChanged("Name");
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
                    SaveButton = false;
                    Editing = false;
                    return;
                }

                if (!Editing)
                {
                    if (IsExist(value))
                    {
                        SelectedSkill = GetSkillByName(value);
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

        private SkillVM GetSkillByName(string skillName)
        {
            return SkillVM.Skills.Where(item => item.Name == skillName).FirstOrDefault();
        }

        private bool IsExist(string skillName)
        {
            return GetSkillByName(skillName) == null ? false : true;
        }
    }
}
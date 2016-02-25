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
    public class SkillWindowVM : EntitysAddRemoveEdit<SkillVM>, INotifyPropertyChanged, ILoadDataSender
    {
        public SkillWindowVM(ILoadDataSender lastScreen) : base(lastScreen)
        {
        }

        public override ObservableCollection<SkillVM> EntityCollection
        {
            get
            {
                if (_savedCollection != null)
                {
                    ObservableCollection<SkillVM> _newCol = new ObservableCollection<SkillVM>();
                    foreach (var item in _savedCollection)
                    {
                        _newCol.Add(item);
                    }

                    _savedCollection.Clear();
                    foreach (var item in _newCol)
                    {
                        _savedCollection.Add(item);
                    }

                    return _savedCollection;
                }

                _entityCollection.Clear();
                _savedCollection = new ObservableCollection<SkillVM>();
                foreach (var item in SkillVM.Skills)
                {
                    _entityCollection.Add(item);
                    _savedCollection.Add(item.Clone());
                }

                return _entityCollection;
            }
        }

        public override void AddButtonClick()
        {
            _savedCollection.Add(new SkillVM(new Skill() { Name = Name }));
            AddButton = false;
            Name = string.Empty;
            OnPropertyChanged("EntityCollection");
            OnPropertyChanged("Name");
            SaveAllChangesButton = true;
            _somethingChanged = true;
        }

        public override void RemoveButtonClick()
        {
            SaveAllChangesButton = true;
            Editing = false;
            AddButton = false;
            EditButton = false;
            RemoveButton = false;
            SaveButton = false;
            _somethingChanged = true;
            if (SelectedEntity != null)
            {
                _savedCollection.Remove(_savedCollection.First(item => item.Name == SelectedEntity.Name));
            }

            Name = string.Empty;
            SelectedEntity = null;
            OnPropertyChanged("EntityCollection");
            OnPropertyChanged("Name");
        }

        public override void SaveButtonClick()
        {
            SaveAllChangesButton = true;
            SaveButton = false;
            _somethingChanged = true;
            getEntityByName(SelectedEntity.Name).Name = Name;
            OnPropertyChanged("EntityCollection");
            Name = string.Empty;
            OnPropertyChanged("Name");
        }

        public override void SaveAllButtonClick()
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
                Projects_Skill.Items.RemoveAll(skill => skill.Skill.Id == item.Id);
                Skill.Items.RemoveAll(skill => skill.Id == item.Id);
            }

            _savedCollection = null;
            SaveAllChangesButton = false;
            LoadData(_lastScreen);
        }

        protected override SkillVM getEntityByName(string name)
        {
            if (_somethingChanged)
            {
                var find = _savedCollection.Where(item => item.Name == name).FirstOrDefault();
                return find;
            }
            else
            {
                var find = _savedCollection.Where(item => item.Name == name).FirstOrDefault();
                return find;
            }
        }
    }
}
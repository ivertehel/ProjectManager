using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMView
{
    public abstract class EntitysAddRemoveEdit<T> : INotifyPropertyChanged, ILoadDataSender
    {
        public EntitysAddRemoveEdit(ILoadDataSender lastScreen)
        {
            _lastScreen = lastScreen;
            AddButton = false;
            EditButton = false;
            RemoveButton = false;
            SaveButton = false;
            CancelAllChangesButton = false;
            SaveAllChangesButton = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual ObservableCollection<T> EntityCollection
        {
            get { return _entityCollection; }
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
                    if (!_somethingChanged)
                        SaveAllChangesButton = false;
                    Editing = false;
                    return;
                }

                if (!Editing)
                {
                    if (IsExist(value))
                    {
                        SelectedEntity = getEntityByName(value);
                        AddButton = false;
                        EditButton = true;
                        RemoveButton = true;
                        SaveButton = false;
                    }
                    else
                    {
                        RemoveButton = false;
                        if (value[0] != ' ')
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
                        if (value[0] != ' ')
                            SaveButton = true;
                    }
                }

                OnPropertyChanged("Name");
            }
        }

        public T SelectedEntity
        {
            get { return _selectedEntity; }
            set { _selectedEntity = value; }
        }

        protected string _name { get; set; }

        protected bool _addButton { get; set; }

        protected bool _editButton { get; set; }

        protected bool _removeButton { get; set; }

        protected bool _saveButton { get; set; }

        protected T _selectedEntity { get; set; }

        protected bool _editing { get; set; }

        protected bool _somethingChanged { get; set; } = false;

        protected ObservableCollection<T> _savedCollection { get; set; }

        protected bool _saveAllChangesButton { get; set; }

        protected bool _cancelAllChangesButton { get; set; }

        protected ILoadDataSender _lastScreen { get; set; }

        protected ObservableCollection<T> _entityCollection { get; set; } = new ObservableCollection<T>();

        public abstract void AddButtonClick();

        public abstract void RemoveButtonClick();

        public abstract void SaveButtonClick();

        public abstract void SaveAllButtonClick();

        public void CancelAllChangesButtonClick()
        {
            Name = string.Empty;
            OnPropertyChanged("Name");
            _savedCollection = null;
            _somethingChanged = false;
            CancelAllChangesButton = false;
            SaveAllChangesButton = false;
            OnPropertyChanged("CancelAllChangesButton");
            OnPropertyChanged("EntityCollection");
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadData(object sender)
        {
            OnPropertyChanged("EntityCollection");
            _lastScreen.LoadData(this);
        }

        protected abstract T getEntityByName(string name);

        private bool IsExist(string name)
        {
            return getEntityByName(name) == null ? false : true;
        }
    }
}

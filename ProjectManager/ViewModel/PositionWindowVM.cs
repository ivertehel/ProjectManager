using PMView.View.WrapperVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PMDataLayer;

namespace PMView
{
    public class PositionWindowVM : EntitysAddRemoveEdit<PositionVM>
    {
        public PositionWindowVM(ILoadDataSender lastScreen) : base(lastScreen)
        {

        }

        public override ObservableCollection<PositionVM> EntityCollection
        {
            get
            {
                if (_savedCollection != null)
                {
                    ObservableCollection<PositionVM> _newCol = new ObservableCollection<PositionVM>();
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
                _savedCollection = new ObservableCollection<PositionVM>();
                foreach (var item in PositionVM.Positions)
                {
                    _entityCollection.Add(item);
                    _savedCollection.Add(item.Clone());
                }

                return _entityCollection;
            }
        }

        public override void AddButtonClick()
        {
            _savedCollection.Add(new PositionVM(new Position() { Name = Name }));
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

        public override void SaveAllButtonClick()
        {
            foreach (var item in _savedCollection)
            {
                var find = Position.Items.FirstOrDefault(position => position.Id == item.Position.Id);
                if (find == null)
                {
                    Position.Items.Add(item.Position);
                }
                else
                {
                    find.Name = item.Name;
                }
            }
            List<Position> toDelete = new List<Position>();
            foreach (var item in Position.Items)
            {
                var find = _savedCollection.FirstOrDefault(position => position.Position.Id == item.Id);
                if (find == null)
                {
                    toDelete.Add(item);
                }
            }
            foreach (var item in toDelete)
            {
                User_Team.Items.RemoveAll(position => position.Position.Id == item.Id);
                User_Project.Items.RemoveAll(position => position.Position.Id == item.Id);
                Position.Items.RemoveAll(position => position.Id == item.Id);
            }
            _savedCollection = null;
            SaveAllChangesButton = false;
            LoadData(_lastScreen);
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

        protected override PositionVM getEntityByName(string name)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMView.View
{
    class AddTeamToTheProjectVM
    {
        private AddTeamToTheProject _addTeamToTheProject;
        private ILoadDataSender _lastScreen;
        private ProjectModuleEditVM _projectModuleEditVM;

        public AddTeamToTheProjectVM(ILoadDataSender lastScreen, ProjectModuleEditVM projectModuleEditVM, AddTeamToTheProject addTeamToTheProject)
        {
            _lastScreen = lastScreen;
            _projectModuleEditVM = projectModuleEditVM;
            _addTeamToTheProject = addTeamToTheProject;
        }
    }
}

namespace Frontend_EmployeeManagementSystem.ApplicationStates
{
    public class AllStates
    {
        public Action? Action { get; set; }
        public bool ShowGeneralDepartment { get; set; }

        public void GeneralDepartmentClicked()
        {
            ResetAllDepartments();
            ShowGeneralDepartment = true;
            Action?.Invoke();
        }

        public bool ShowDepartments { get; set; }
        public void DepartmentClicked() {

            ResetAllDepartments();
            ShowDepartments = true;
            Action?.Invoke();
        }

        public bool ShowBranch{ get; set; }
        public void BranchClicked()
        {

            ResetAllDepartments();
            ShowBranch = true;
            Action?.Invoke();
        }

        public bool ShowCountry{ get; set; }
        public void CountryClicked()
        {

            ResetAllDepartments();
            ShowCountry= true;
            Action?.Invoke();
        }

        public bool ShowCity { get; set; }
        public void CityClicked()
        {

            ResetAllDepartments();
            ShowCity= true;
            Action?.Invoke();
        }


        public bool ShowUser{ get; set; }
        public void UserClicked()
        {

            ResetAllDepartments();
            ShowUser = true;
            Action?.Invoke();
        }

        public bool ShowEmployee{ get; set; }
        public void EmployeeClicked()
        {

            ResetAllDepartments();
            ShowEmployee= true;
            Action?.Invoke();
        }

        private void ResetAllDepartments()
        {
            ShowGeneralDepartment = false;
            ShowDepartments = false;
            ShowBranch = false;
            ShowCountry = false;
            ShowCity = false;
            ShowUser = false;
            ShowEmployee = false;
        }
    }
}

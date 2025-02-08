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

        public bool ShowEmployee { get; set; } = true;
        public void EmployeeClicked()
        {

            ResetAllDepartments();
            ShowEmployee= true;
            Action?.Invoke();
        }

        public bool ShowOvertime { get; set; }
        public void OvertimeClicked()
        {
            ResetAllDepartments();
            ShowOvertime = true;
            Action?.Invoke();
        }

        public bool ShowOvertimeType { get; set; }
        public void OvertimeTypeClicked()
        {
            ResetAllDepartments();
            ShowOvertimeType = true;
            Action?.Invoke();
        }

        public bool ShowHeath { get; set; }
        public void HeathClicked()
        {
            ResetAllDepartments();
            ShowHeath = true;
            Action?.Invoke();
        }

       public bool ShowSanction { get; set; }
        public void SanctionClicked()
        {
            ResetAllDepartments();
            ShowSanction = true;
            Action?.Invoke();
        }


        public bool ShowSancationType { get; set; }
        public void SancationTypeClicked()
        {
            ResetAllDepartments();
            ShowSancationType = true;
            Action?.Invoke();
        }

        public bool ShowVacationType { get; set; }
        public void VacationTypeClicked()
        {
            ResetAllDepartments();
            ShowVacationType = true;
            Action?.Invoke();
        }
        public bool ShowVacation { get; set; }
        public void VacationClicked()
        {
            ResetAllDepartments();
            ShowVacation = true;
            Action?.Invoke();
        }

        public bool ShowUserProfile { get; set; }
        public void UserProfileClicked()
        {
            ResetAllDepartments();
            ShowUserProfile = true;
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
            ShowOvertime = false;
            ShowOvertimeType = false;
            ShowHeath = false;
            ShowSancationType = false;
            ShowSanction = false;
            ShowVacationType = false;
            ShowVacation = false;
            ShowUserProfile = false;
           
        }
    }
}

namespace Frontend_EmployeeManagementSystem.ApplicationStates
{
    public class DepartmentStates
    {
        public Action? GeneralDepartmentAction { get; set; }
        public bool ShowGeneralDepartment { get; set; }

        public void GeneralDepartmentClicked()
        {
            ResetAllDepartments();
            ShowGeneralDepartment = true;
            GeneralDepartmentAction?.Invoke();
        }

        private void ResetAllDepartments()
        {
            ShowGeneralDepartment = false;
        }
    }
}

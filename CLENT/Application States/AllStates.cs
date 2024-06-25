namespace CLENT.Application_States
{
    public class AllStates
    {
        public Action? Action { get; set; }
        public bool ShowGeneralDepartment {  get; set; }
        public void GeneralDepartmentClicked()
        {
            RestAllDepartment();
            ShowGeneralDepartment=true;
            Action?.Invoke();
        }
        public bool ShowDepartment {  get; set; }
        public void DepartmentClicked()
        {
            RestAllDepartment();
            ShowDepartment = true;
            Action?.Invoke();
        }
        public bool ShowCity { get; set; }
        public void CityClicked()
        {
            RestAllDepartment();
            ShowCity = true;
            Action?.Invoke();
        }
        public bool ShowCountry { get; set; }
        public void CountryClicked()
        {
            RestAllDepartment();
            ShowCountry = true;
            Action?.Invoke();
        }
        public bool ShowTwon { get; set; }
        public void TwonClicked()
        {
            RestAllDepartment();
            ShowTwon = true;
            Action?.Invoke();
        }
        public bool ShowBranch { get; set; }
        public void BranchClicked()
        {
            RestAllDepartment();
            ShowBranch = true;
            Action?.Invoke();
        }
        public bool ShowEmpolee { get; set; }
        public void EmpoleeClicked()
        {
            RestAllDepartment();
            ShowEmpolee = true;
            Action?.Invoke();
        }
        public bool ShowUser { get; set; }
        public void UserClicked()
        {
            RestAllDepartment();
            ShowUser = true;
            Action?.Invoke();
        }
        public void RestAllDepartment()
        {
            ShowGeneralDepartment = false;
            ShowDepartment = false;
            ShowCity = false;
            ShowBranch = false;
            ShowCountry = false;
            ShowTwon = false;
            ShowUser = false;
            ShowEmpolee=false;

        }
    }
}
